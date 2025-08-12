
<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    DriverPackCatalog Module 
    Created by: Gilmar Prust
    Filename:   DriverPackCatalog.psm1
+----------------------------------------------------------------------------------------+
#>

class DriverPack {
    [string]$Manufacturer
    [string]$Model
    [string[]]$Types = @()
    [string]$OS
    [string]$Version
    [string]$Link
    [string]$Hash
    
    DriverPack([string]$Manufacturer, [String]$Model, [string[]]$Types, [String]$OS, [String]$Version, [string]$Link, [string]$Hash) {
        $this.Manufacturer = $Manufacturer
        $this.Model        = $Model
        $this.Types        = $Types
        $this.OS           = $OS
        $this.Version      = $Version
        $this.Link         = $Link
        $this.Hash         = $Hash
    }
}

class DriverPackCatalog {

    static [System.Collections.Generic.List[DriverPack]] $DriverPacks

    static [void] Initialize() {
        
        [DriverPackCatalog]::DriverPacks = [System.Collections.Generic.List[DriverPack]]::new()
    }

    static [void] Add([DriverPack]$Model) {
        
        [DriverPackCatalog]::DriverPack.Add($Model)
    } 

    static [void] AddRange($DriverPacks) {
        
        $DriverPacks | ForEach-Object {

            [DriverPackCatalog]::DriverPacks.Add( 

                [DriverPack]::new( 
                    $_.Manufacturer, 
                    $_.Model, 
                    $_.Types, 
                    $_.OS, 
                    $_.Version, 
                    $_.Link, 
                    $_.Hash
                )
            )
        }
    }
}

function Get-Catalog {
    <#
        .DESCRIPTION
        Obtem os catalogos de todos os fabricantes.

        .OUTPUTS
        Return [PSCustonObject]@{ Manufacturer=""; Model=""; Types=@(); OS=""; Version=""; Links=""; Hash="" }
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory, ValueFromPipeline)]
        [ValidateNotNullOrEmpty()]
        $XmlContent,
        $Manufacturer
    )
    
    <#
        .DESCRIPTION
         DELL
    #>
    function Dell {
        param ( 
            $Content,
            $Manufacturer
        )
        $driverPackCatalog = New-Object System.Collections.ArrayList
        
        ### Filtered List
        [PScustomobject]$FilteredList = $Content.DriverPackManifest.DriverPackage | Where-Object {
            $_.SupportedOperatingSystems.OperatingSystem.majorVersion -eq "10" -and
            $_.SupportedSystems.Brand.prefix -match "(OP|LAT|XPSNOTEBOOK)" }

        $driverPackCatalog += $FilteredList | Select-Object -Property @{ Label="Manufacturer" ;Expression={ $Manufacturer }},
            @{Label="Model"   ;Expression={ $_.SupportedSystems.Brand.Model.name | Select-Object -unique }},
            @{Label="Types"   ;Expression={ @($_.SupportedSystems.Brand.Model.systemID) }},
            @{Label="OS"      ;Expression={ ($_.SupportedOperatingSystems.OperatingSystem.osCode | Select-Object -Unique) -replace "Windows", "Win" }},
            @{Label="Version" ;Expression={ $_.dellVersion }},
            @{Label="Link"    ;Expression={ "http://$($Content.DriverPackManifest.baseLocation)/$($_.path)"}},
            @{Label="Hash"    ;Expression={ ($_.Cryptography.Hash | Where-Object {$_.algorithm -eq 'SHA256'}).'#text' 
        }} | Sort-Object Model

        [DriverPackCatalog]::AddRange($driverPackCatalog)

    }

    <#
        .DESCRIPTION
        LENOVO
    #>
    function Lenovo {
        param ( 
            $Content,
            $Manufacturer
        )
        <##>
        $LenovoModels = $Content.ModelList | Select-Object -ExpandProperty Model | Sort-Object -Property name
        $LenovoModels | ForEach-Object { $_.name = ($_.name -split ' Type')[0].Trim() }
            
        foreach ($model in $LenovoModels) {
        
            $sccm = $model.SCCM | Where-Object { $_.os -eq 'Win10' -or $_.os -eq 'Win11' } | Sort-Object -Property version | Select-Object -Last 1
            
            [DriverPackCatalog]::DriverPacks.Add( 

                [DriverPack]::new( 
                    $Manufacturer, 
                    $model.name, 
                    @($model.Types.Type), 
                    (Get-Culture).TextInfo.ToTitleCase($sccm.os), 
                    $sccm.version, 
                    $sccm.'#text', 
                    $sccm.crc
                )
            )
        }
    }

    <#
        .DESCRIPTION
        Outro Fabricante
    #>
    function Other {
        param ( 
            $Content,
            $Manufacturer
        )
        $devicesCatalog = New-Object System.Collections.ArrayList

        $devicesCatalog += @{ 
                Manufacturer = $Manufacturer;
                Model   = "Other";
                Types   = @("Types");
                OS      = "os";
                Version = "Versiom";
                Link    = "Link";
                Hash    = "Hash"
         }
    }

    switch -Wildcard ($Manufacturer) {
        "*Dell*"   { Dell -Content $XmlContent -Manufacturer $Manufacturer }
        "*Lenovo*" { Lenovo -Content $XmlContent -Manufacturer $Manufacturer }
        #"*Other*"  { Other -Content $XmlContent -Manufacturer $Manufacturer }
        ###
        ### Espaço para implementar outros fabricantes.
        ###
    }
}

function Get-XmlContent {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory, ValueFromPipeline)]
        [ValidateNotNullOrEmpty()]
        $FilePath
    )
    <##>
    $name = [System.IO.Path]::GetFileNameWithoutExtension($FilePath)
    $extencion = [System.IO.Path]::GetExtension($FilePath)

    ### Extract xml file, if .Cab file
    if ($extencion -eq '.cab') {
        Invoke-Command -ScriptBlock { expand "$($env:TEMP)\$($name).cab" "$($env:TEMP)\$($name).xml" } -ErrorAction Stop | Out-Null
    }

    [xml]$XmlContent = Get-Content "$($env:TEMP)\$($name).xml" -ErrorAction Stop
    return $XmlContent
}

function Get-DriverPackCatalog {
    <#
        .DESCRIPTION
        
    #>
    Install-PSResource Control -Repository DCM2_PSResources
    Install-PSResource Download -Repository DCM2_PSResources

    [DriverPackCatalog]::Initialize()
        
    (Import-Settings).Catalog.DriverPacks | ForEach-Object {
        <##>
        Download -Url $_.Link | Get-XmlContent | Get-Catalog -Manufacturer $_.Manufacturer
    }
    return [DriverPackCatalog]::DriverPacks
}



# SIG # Begin signature block
# MIIFlAYJKoZIhvcNAQcCoIIFhTCCBYECAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUYevSz1LXrWMEGmzNepreWUdB
# 5XCgggMiMIIDHjCCAgagAwIBAgIQONirzSpNl5FNggQlpn5yyDANBgkqhkiG9w0B
# AQsFADAnMSUwIwYDVQQDDBxQb3dlclNoZWxsIENvZGUgU2lnbmluZyBDZXJ0MB4X
# DTIzMDcxNDE1NTUxN1oXDTI0MDcxNDE2MTUxN1owJzElMCMGA1UEAwwcUG93ZXJT
# aGVsbCBDb2RlIFNpZ25pbmcgQ2VydDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCC
# AQoCggEBAL/rqq9BFlGt8bVmQ6EqY43B8W3d2b82C/QN8O37QTChQKUPws+osX1f
# tj9MPdL93WC/XEPqvUiwqPtvBS63JinOJGATsxXh/+fcHCbvJnCestdrZ36VnJJ+
# rvslUBvorv5ynfpBSZRbCuA9aK06rBXksGTbu1wLdGc3oezK7iKymqYwIilFxqz8
# FnliKSbiUWhQmpGxx9RTjZ4YP6YItETeeDMRFGBVp6wlW+qXt6fV7VfqwRkpFCNJ
# YBO9j1Cpz/WcIjoXIeorb9iY5UEsNlLzIcJV0eqJlsvvqqyK07ITKug1OK/OXsYk
# ziA9WWLmpsvv0jRQYHFwXpcx2pwmr7UCAwEAAaNGMEQwDgYDVR0PAQH/BAQDAgeA
# MBMGA1UdJQQMMAoGCCsGAQUFBwMDMB0GA1UdDgQWBBQ3wm3NMuMjb8eeEE5cDN2+
# 4M0FOzANBgkqhkiG9w0BAQsFAAOCAQEAEQsmQjGeMB04f0awcyggFRT988/9EN80
# bzdIWYdJFKJ3Nu2N/vKZOaSBF7AAMrNpYZGFVUgZ8wkfobw12RnH+Lz9cyt+rCSS
# TBfMyIZfoVT9sAu7NQV5cufqH82ObysqoZa3PbniXvDE+PaP1ceWje1ONmFdtr8X
# Ffkv0QFmKeH2x4cpzrJBBD58GI3XssUD2xxtHZntC98VOy0Vk4eQQZCXr42m2C3Q
# lMZby+vvm179rkn8icFkDMlQ81Im6DBSSKDP5AqWGfI5d+54ACFayHiztSS0lyl6
# OBo8+fK5kJXoTqSR2F7oh4hvFvfaBanGWmYHwcXo1yKRX2jD80/RgDGCAdwwggHY
# AgEBMDswJzElMCMGA1UEAwwcUG93ZXJTaGVsbCBDb2RlIFNpZ25pbmcgQ2VydAIQ
# ONirzSpNl5FNggQlpn5yyDAJBgUrDgMCGgUAoHgwGAYKKwYBBAGCNwIBDDEKMAig
# AoAAoQKAADAZBgkqhkiG9w0BCQMxDAYKKwYBBAGCNwIBBDAcBgorBgEEAYI3AgEL
# MQ4wDAYKKwYBBAGCNwIBFTAjBgkqhkiG9w0BCQQxFgQUoCKK/wgGQkU0G6YIqMv4
# kx09lmcwDQYJKoZIhvcNAQEBBQAEggEAHhFmSqNnGiUokoAqG+89HfYszpEI2c9i
# wT+uhN6vmVX953wg37r7MnGL3Dr5sTGwkjzYsCOSfYNzYP6cqWSeUzE3CyUGinr5
# 9zapN3dVvsaKyjzaz9yt4c7/loXygBm0Rx0ZPFoPxA3lIMPpHVXMVg7BZ0piyVzV
# PbYVw9leHo8xDcDnmmVfuLWNQNlzLxCbrIH5Hm3G3Lv/X8wf2b4/5iAJBXdw9WbO
# 30BsRLlBoWoPSxUto+fZy1I7Ja5oA2Pf27MGLcN37usO/qMly4ozwLm0yZ6Za7gl
# 8t2haaRV7gs2JVSWMDBvm4WyTUwmhybgowFQofaXiw402/E0ajQZyQ==
# SIG # End signature block