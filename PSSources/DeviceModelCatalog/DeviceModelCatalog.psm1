
<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    DeviceModelCatalog Module
    Created by: Gilmar Prust
    Filename:   DeviceModelCatalog.psm1
+----------------------------------------------------------------------------------------+
#>

class DeviceModel {

    [string]$Manufacturer
    [string]$Model
    [string[]]$Types = @()
    [string]$Version
    [string]$Firmware
    [string]$Hash
    
    DeviceModel([String]$Manufacturer, [String]$Model, [string[]]$Types, [String]$Version, [string]$Firmware, [string]$Hash) {
        $this.Manufacturer = $Manufacturer
        $this.Model        = $Model
        $this.Types        = $Types
        $this.Version      = $Version
        $this.Firmware     = $Firmware
        $this.Hash         = $Hash
    }
}

class DeviceModelCatalog {

    static [System.Collections.Generic.List[DeviceModel]] $DeviceModels

    static [void] Initialize() {
        
        [DeviceModelCatalog]::DeviceModels = [System.Collections.Generic.List[DeviceModel]]::new()
    }

    static [void] Add([DeviceModel]$Model) {
        
        [DeviceModelCatalog]::DeviceModels.Add($Model)
    } 

    static [void] AddRange($Models) {
        
        $Models | ForEach-Object {

            [DeviceModelCatalog]::DeviceModels.Add( 
                [DeviceModel]::new( 
                    $_.Manufacturer, 
                    $_.Model, 
                    $_.Types, 
                    $_.Version, 
                    $_.Firmware, 
                    $_.Hash
                )
            )
        }
    }
}

function Get-Catalog {
    <#
        .DESCRIPTION
        Obtem os catalogos de modelos de todos os fabricantes.
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
        <##>
        $softwareComponent = $Content.Manifest.SoftwareComponent | Where-Object { ($_.ComponentType.value -eq "BIOS") }

        foreach ($component in $softwareComponent) {
        
            foreach ($brand_item in $component.SupportedSystems.Brand) {
        
                foreach ($model_item in $brand_item.Model) {
    
                    $modelnames = @($model_item.Display."#cdata-section" -split '/')
                    foreach ($modelname in $modelnames) {
                        
                        $modelname = $modelname -replace "Latitude-","" -replace "Precision-","" -replace "OptiPlex-",""
                        $modelname = "$($brand_item.Display."#cdata-section") $($modelname)"

                        $lastitem = [DeviceModelCatalog]::DeviceModels[[DeviceModelCatalog]::DeviceModels.Count -1]
                        
                        if (($modelname -eq $lastitem.Model) -and ($component.hashMD5 -eq $lastitem.Hash)) {
    
                            [DeviceModelCatalog]::DeviceModels[[DeviceModelCatalog]::DeviceModels.Count -1].Types += $model_item.systemID
    
                        } else {

                            [DeviceModelCatalog]::Add( 
                                [DeviceModel]::new( 
                                    $Manufacturer, 
                                    $modelname, 
                                    $model_item.systemID, 
                                    $component.dellVersion, 
                                    "http://$($Content.Manifest.baseLocation)/$($component.path)", 
                                    $component.hashMD5
                                )
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
            $Content,
            $Manufacturer
        )
        <##>
        $devicesCatalog = @()
        $LenovoModels = $Content.ModelList | Select-Object -ExpandProperty Model
        $devicesCatalog = $LenovoModels | Select-Object -Property  @{
                Label="Manufacturer" ; Expression={ $Manufacturer }}, 
                @{Label="Model"      ; Expression={ ($_.name -split 'Type')[0].Trim() }},    
                @{Label="Types"      ; Expression={ ($_.Types.Type) -join ',' }},
                @{Label="Version"    ; Expression={ $_.BIOS.version }},
                @{Label="Firmware"   ; Expression={ $_.BIOS.'#text'}},
                @{Label="Hash"       ; Expression={ $_.BIOS.crc }
        }
        
        [DeviceModelCatalog]::AddRange($devicesCatalog)
    }

    <#
        .DESCRIPTION
        Outro Fabricante
    #>
    function HP {
        param ( 
            $Content,
            $Manufacturer
        )
        <##>
        $devicesCatalog = @()

        $devicesCatalog += @{ 
                Manufacturer = $Manufacturer;
                Model    = "Other";
                Types    = "Types";
                Version  = "Versiom";
                Firmware = "Link";
                Hash     = "Hash"
         }
    }

    switch -Wildcard ($Manufacturer) {
        "*Dell*"   { Dell -Content $XmlContent -Manufacturer $Manufacturer }
        "*Lenovo*" { Lenovo -Content $XmlContent -Manufacturer $Manufacturer }
        #"*HP*"    { HP -Content $XmlContent -Manufacturer $Manufacturer }
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

function Get-DeviceModelCatalog {
    <#
        .DESCRIPTION
        
    #>
    Install-PSResource Control -Repository DCM2_PSResources
    Install-PSResource Download -Repository DCM2_PSResources

    [DeviceModelCatalog]::Initialize()
        
    (Import-JsonSettings).Catalog.DeviceModels | ForEach-Object {
        <##>
        Download -Url $_.Link | Get-XmlContent | Get-Catalog -Manufacturer $_.Manufacturer
    }
    return [DeviceModelCatalog]::DeviceModels
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