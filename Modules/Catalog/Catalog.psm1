<#
+----------------------------------------------------------------------------------------+
    .Description
    Catalog
	Created by: Gilmar Prust
	Filename:   Catalog.psm1
    Get-DevicesCatalog
+----------------------------------------------------------------------------------------+
#>

function Get-DevicesCatalog_Temp {
    <#
        .PARAMETER Links
        Especifique os links para download dos catalogos.

        .DESCRIPTION
        Obtem os catalogos, e cria um PSCustonObject.

        .OUTPUTS
        Returns list of objects @{ Manufacturer=""; Model=""; Types=""; Version=""; Link=""; Hash="" }
    #>
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $Links
    )
    Write-Host "Getting devices catalog... " -ForegroundColor DarkMagenta -NoNewline
    $devicesCatalog = New-Object System.Collections.ArrayList

    <#
        .DESCRIPTION
         DELL
    #>
    $CatalogDell = Get-XmlCatalog -Url $Links.Devices.Dell
    $softwareComponent = $CatalogDell.Manifest.SoftwareComponent | Where-Object { ($_.ComponentType.value -eq "BIOS") }


    foreach ($component in $softwareComponent) {
    
        foreach ($brand_item in $component.SupportedSystems.Brand) {
    
            foreach ($model_item in $brand_item.Model) {

                $modelnames = @($model_item.Display."#cdata-section" -split '/')
                foreach ($modelname in $modelnames) {
                    
                    $modelname = $modelname -replace "Latitude-","" -replace "Precision-","" -replace "OptiPlex-",""
                    $modelname = "$($brand_item.Display."#cdata-section") $($modelname)"
                    $lastitem = $devicesCatalog | Select-Object -Last 1
                    
                    if (($modelname -eq $lastitem.Model) -and ($component.hashMD5 -eq $lastitem.Hash)) {

                        $devicesCatalog | Select-Object -Last 1 | ForEach-Object {
                            $_.Types = $_.Types + "," + $model_item.systemID
                        }

                    } else {

                        $devicesCatalog += ([PScustomobject]@{  

                            Manufacturer = "Dell Inc.";
                            Model        = $modelname;
                            Types        = $($model_item.systemID);
                            Version      = $component.dellVersion;
                            Link         = "http://$($CatalogDell.Manifest.baseLocation)/$($component.path)";
                            Hash         = $component.hashMD5
                        })
                    }
                }
            }        
        }
    }

    <#
        .DESCRIPTION
         LENOVO
    #>
    $CatalogLenovo = Get-XmlCatalog -Url $Links.DriverPack.Lenovo
    $LenovoModels = $CatalogLenovo.ModelList | Select-Object -ExpandProperty Model

    $devicesCatalog += $LenovoModels | Select-Object -Property  @{
            Label="Manufacturer" ; Expression={ "Lenovo" }}, 
            @{Label="Model"      ; Expression={ ($_.name -split 'Type')[0].Trim() }},    
            @{Label="Types"      ; Expression={ ($_.Types.Type) -join ',' }},
            @{Label="Version"    ; Expression={ $_.BIOS.version }},
            @{Label="Link"       ; Expression={ $_.BIOS.'#text'}},
            @{Label="Hash"       ; Expression={ $_.BIOS.crc }
    }

    <#
        .DESCRIPTION
         HP
    #>

    return $devicesCatalog
}
function Get-DriverPackCatalog {
    <#
        .PARAMETER Links
        Especifique os links para download dos catalogos.

        .DESCRIPTION
        Obtem os catalogos, e cria um PSCustonObject.

        .OUTPUTS
        Returns list of objects @{ Manufacturer=""; Model=""; Types=""; Version=""; Link=""; Hash="" }
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory, ValueFromPipeline)]
        [ValidateNotNullOrEmpty()]
        $Links
    )

    $driverPackCatalog = New-Object -TypeName System.Collections.ArrayList

    <# 
        .DESCRIPTION
        DELL 
    #>
    $CatalogDell = Get-XmlCatalog -Url $Links.Dell
    #Filtered List
    $FilteredList = $CatalogDell.DriverPackManifest.DriverPackage | Where-Object {
        $_.SupportedOperatingSystems.OperatingSystem.majorVersion -eq "10" -and
        $_.SupportedSystems.Brand.prefix -match "(OP|LAT|XPSNOTEBOOK)" }

    $driverPackCatalog += [PScustomobject]$FilteredList | Select-Object -Property @{ Label="Manufacturer" ;Expression={ 'Dell Inc.' }},
        @{Label="Model"   ;Expression={ $_.SupportedSystems.Brand.Model.name | Select-Object -unique }},
        @{Label="Types"   ;Expression={ $_.SupportedSystems.Brand.Model.systemID -join ',' }},
        @{Label="OS"      ;Expression={ ($_.SupportedOperatingSystems.OperatingSystem.osCode | Select-Object -Unique) -replace "Windows", "Win" }},
        @{Label="Version" ;Expression={ $_.dellVersion }},
        @{Label="Link"    ;Expression={ "http://$($CatalogDell.DriverPackManifest.baseLocation)/$($_.path)"}},
        @{Label="Hash"    ;Expression={ ($_.Cryptography.Hash | Where-Object {$_.algorithm -eq 'SHA256'}).'#text' 
    }} | Sort-Object Model
    

    <# 
        .DESCRIPTION
        LENOVO 
    #>
    $CatalogLenovo = Get-XmlCatalog -Url $Links.Lenovo
    $LenovoModels = $CatalogLenovo.ModelList | Select-Object -ExpandProperty Model | Sort-Object -Property name
    $LenovoModels | ForEach-Object { $_.name = ($_.name -split ' Type')[0].Trim() }
        
    foreach ($model in $LenovoModels) {
    
        $sccm = $model.SCCM | Where-Object { $_.os -eq 'Win10' -or $_.os -eq 'Win11' } | Sort-Object -Property version | Select-Object -Last 1
        $driverPackCatalog += [PScustomobject]@{ 
    
            Manufacturer = "Lenovo";
            Model   = $model.name;
            Types   = $model.Types.Type -join ','; 
            OS      = (Get-Culture).TextInfo.ToTitleCase($sccm.os);
            Version = $sccm.version;
            Link    = $sccm.'#text';
            Hash    = $sccm.crc;
        }  
    }

    <# 
        .DESCRIPTION
        HP
    #>

    return $driverPackCatalog
}


<#
    .DESCRIPTION
    Get-AllDeviceCatalog
#>
class Device {

    [string]$Manufacturer
    [string]$Model
    [string[]]$Types = @()
    [string]$Version
    [string]$Link
    [string]$Hash
    
    Device([String]$Manufacturer, [String]$Model, [string[]]$Types, [String]$Version, [string]$Link, [string]$Hash) {
        $this.Manufacturer = $Manufacturer
        $this.Model        = $Model
        $this.Types        = $Types
        $this.Version      = $Version
        $this.Link         = $Link
        $this.Hash         = $Hash
    }
}
class DeviceList {

    static [System.Collections.Generic.List[[Device]]] $Devices

    static [bool] Initialize() {
        [DeviceList]::Devices = [System.Collections.Generic.List[Device]]::new()
        return $true
    }
    static [DeviceList] GetAll() {
        
        return [DeviceList]::Devices
    }
    static [bool] AddList([]) {
        
        [DeviceList]::Devices
        return $true
    }


    static [bool] Add([Device]$Device) {

        [DeviceList]::Devices.Add($Device)
        return $true
    }    
}
function Get-Catalog {
    <#
        .DESCRIPTION
        Obtem os catalogos, e cria um PSCustonObject.

        .OUTPUTS
        Return PSCustonObject of devices @{ Manufacturer=""; Model=""; Types="" }
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory, ValueFromPipeline, ValueFromPipelineByPropertyName)]
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
            $Content
        )
        $devicesCatalog = New-Object System.Collections.ArrayList
        
        $softwareComponent = $Content.Manifest.SoftwareComponent | Where-Object { ($_.ComponentType.value -eq "BIOS") }

        foreach ($component in $softwareComponent) {
        
            foreach ($brand_item in $component.SupportedSystems.Brand) {
        
                foreach ($model_item in $brand_item.Model) {
    
                    $modelnames = @($model_item.Display."#cdata-section" -split '/')
                    foreach ($modelname in $modelnames) {
                        
                        $modelname = $modelname -replace "Latitude-","" -replace "Precision-","" -replace "OptiPlex-",""
                        $modelname = "$($brand_item.Display."#cdata-section") $($modelname)"
                        $lastitem = $devicesCatalog | Select-Object -Last 1
                        
                        if (($modelname -eq $lastitem.Model) -and ($component.hashMD5 -eq $lastitem.Hash)) {
    
                            $devicesCatalog | Select-Object -Last 1 | ForEach-Object {
                                $_.Types = $_.Types + "," + $model_item.systemID
                            }
    
                        } else {

                            [DeviceList]::Devices::Add( [Device]::new(
                                $Manufacturer,
                                $modelname, 
                                $($model_item.systemID), 
                                $component.dellVersion, 
                                "http://$($Content.Manifest.baseLocation)/$($component.path)", 
                                $component.hashMD5)
                            )
                        }
                    }
                }        
            }
        }
    }
    <#
        .DESCRIPTION
        LENOVO
    #>
    function Lenovo {
        param ( 
            $Content
        )
        $devicesCatalog = New-Object System.Collections.ArrayList

        $LenovoModels = $Content.ModelList | Select-Object -ExpandProperty Model
        $devicesCatalog += $LenovoModels | Select-Object -Property  @{
                Label="Manufacturer" ; Expression={ "Lenovo" }}, 
                @{Label="Model"      ; Expression={ ($_.name -split 'Type')[0].Trim() }},    
                @{Label="Types"      ; Expression={ ($_.Types.Type) -join ',' }},
                @{Label="Version"    ; Expression={ $_.BIOS.version }},
                @{Label="Link"       ; Expression={ $_.BIOS.'#text'}},
                @{Label="Hash"       ; Expression={ $_.BIOS.crc }
        }
        return $devicesCatalog
    }

    switch -Wildcard ($Manufacturer) {
        "*Dell*" { Dell -Content $XmlContent }
        "*Lenovo*" { Lenovo -Content $XmlContent  }
    }
}
function Get-XmlContent {
    
    [CmdletBinding()]
    param (
        [Parameter(Mandatory, ValueFromPipeline, ValueFromPipelineByPropertyName)]
        [ValidateNotNullOrEmpty()]
        $Url
    )

    $name = [System.IO.Path]::GetFileNameWithoutExtension($Url)
    $filename = Split-Path -Path $Url -Leaf
    $extencion = [System.IO.Path]::GetExtension($Url)

    Download -Url $Url -Destination "$($env:TEMP)\$($filename)" -ErrorAction Stop
    
    # Extract xml file, if .Cab file
    if ($extencion -eq '.cab') {
        Invoke-Command -ScriptBlock { expand "$($env:TEMP)\$($name).cab" "$($env:TEMP)\$($name).xml" } -ErrorAction Stop | Out-Null
    }

    [xml]$XmlContent = Get-Content "$($env:TEMP)\$($name).xml" -ErrorAction Stop
    return $XmlContent
}

function Get-DevicesCatalog {
    <#
        .DESCRIPTION
        Obtem os catalogos, e cria um PSCustonObject.

        .OUTPUTS
        Returns list of objects @{ Manufacturer=""; Model=""; Types="" }
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $Settings
    )
    Write-Host "Getting all devices catalog... " -ForegroundColor DarkMagenta -NoNewline

    $Settings | ForEach-Object {

        [DeviceList]::AddSource((Get-Catalog $_.Manufacturer -XmlContent (Get-XmlContent -Url $_.Link)))
        
    }
    Write-Host "Done." -ForegroundColor Green
    return [DeviceList]::Devices
}

Export-ModuleMember -Function Get-DevicesCatalog


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