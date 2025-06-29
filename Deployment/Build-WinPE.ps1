<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
        Add-Firmware
        Created by: Gilmar Prust
        Filename:   Add-Firmware.ps1

    .EXAMPLE
        $Device = @{ 
            Guid         = (New-Guid).Guid;
            Manufacturer = "Dell Inc."; 
            Model        = "OptiPlex 5000"; 
            Types        = @("0AC3","0AC4")
        }
+----------------------------------------------------------------------------------------+
#>
param (
    [Object[]]$DeviceModel,
    [Object[]]$DeviceCatalog
)

### GLOBAL VARIABLES
New-Variable -Name DeployRoot -Value "$($PSScriptRoot)" -Scope Global -Force -Option ReadOnly
Set-Location $DeployRoot -Verbose

### IMPORT MODULES
Import-Module .\Modules\Control -Force -Global -ErrorAction Stop -Verbose
Import-Module .\Modules\Download -Force -Global -ErrorAction Stop -Verbose
Import-Module .\Modules\Firmware -Force -Global -ErrorAction Stop -Verbose

function Main {

    begin {

        $JsonFirmwares = @(Get-JsonFirmwares)
        
        $null -eq $DeviceModel ? ($SelectedDeviceModel = Get-JsonDeviceModels | 
            Out-GridView -OutputMode Single -Title "Select device.") : ($SelectedDeviceModel = $DeviceModel)
            
        $null -eq $DeviceCatalog ? ($SelectedDeviceCatalog = .\Get-DeviceModelCatalog.ps1 | 
            Out-GridView -OutputMode Single -Title "Select device model.") : ($SelectedDeviceCatalog = $catalog)
        
        if ($null -eq $SelectedDeviceModel -or $null -eq $SelectedDeviceCatalog) { break }
        
    }
    process {

        $filename = Split-Path -Path $SelectedDeviceCatalog.Link -Leaf
        $fullfilepath = "$($DeployRoot)\Firmwares\$($filename)"


        ### verifica se item atual é mais antigo que do catalogo, se sim faz download e update
        ### se nao, New item
        $exist = Get-FirmwareItemByGuid -Guid $SelectedDeviceModel.Guid

        if ($null -ne $exist) {
            
        }
        

        

        ### Download Firmware.
        $fileIntegrity = Download -Url $SelectedDeviceCatalog.Link -Destination $fullfilepath -Hash $SelectedDeviceCatalog.Hash -ErrorAction SilentlyContinue

        ### Check the file hash.
        if ($false -eq $fileIntegrity) {
            Write-host "Error downloading file or different hash." -ForegroundColor Red
            Remove-Item -Path $fullfilepath -Force -ErrorAction SilentlyContinue -Verbose
            break
        }

        if ($JsonFirmwares.DeviceGuid -contains $SelectedDeviceModel.Guid) {

            ### Remove old file.
            $old = $JsonFirmwares | Where-Object { $_.DeviceGuid -eq $SelectedDeviceModel.Guid }
            if ($old.Version -ne $SelectedDeviceCatalog.Version) {
                Remove-Item -Path "$($DeployRoot)$($old.Source)" -Force -ErrorAction SilentlyContinue -Verbose -Confirm
            }

            ### Update firmware.
            $Firmware = [ordered]@{ 

                DeviceGuid = $SelectedDeviceModel.Guid;
                FileName   = $filename; 
                Version    = $SelectedDeviceCatalog.Version
                Source     = "\Firmwares\$($filename)"; 
                Hash       = $SelectedDeviceCatalog.Hash;
            }
            Update-Firmware -Firmware $Firmware
            Write-Host "Update firmware for $($SelectedDeviceModel.Model)." -ForegroundColor Green

        } else {

            $NewFirmwares = [ordered]@{ 

                DeviceGuid = "$($SelectedDeviceModel.Guid)";
                FileName   = $filename;
                Version    = $SelectedDeviceCatalog.Version;
                Source     = "\Firmwares\$($filename)";
                Hash       = $SelectedDeviceCatalog.Hash;
                Enabled    = $true
            }
            Add-NewFirmware -Firmware $NewFirmwares
            Write-Host "Add new firmware for $($SelectedDeviceModel.Model)." -ForegroundColor Green
        }
    }
    end {
        #Save JsonFirmware.
        Save-JsonFirmwares -Content $JsonFirmwares
    }
}
Main


#>
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