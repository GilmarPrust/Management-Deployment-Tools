<#
+----------------------------------------------------------------------------------------+
    .Description
    Add-Firmware
	Created by: Gilmar Prust
	Filename:   Add-Firmware.ps1
+----------------------------------------------------------------------------------------+
#>
<#
    .EXAMPLE
    $Device = @{ 
        Guid         = (New-Guid).Guid;
        Manufacturer = "Dell Inc."; 
        Model        = "OptiPlex 5000"; 
        Types        = @("0AC3","0AC4")
    }
#>
param (
    [Object[]]$Device,
    [Object[]]$DeviceCatalog
)

New-Variable -Name DeployRoot -Value "$($PSScriptRoot)" -Scope Global -Force -Option ReadOnly
Set-Location $DeployRoot -Verbose

#IMPORT MODULES
Import-Module .\Scripts\Modules\Download -Force -Global -ErrorAction Stop -Verbose
Import-Module .\Scripts\Modules\Firmware -Force -Global -ErrorAction Stop -Verbose
Import-Module .\Scripts\Modules\Catalog  -Force -Global -ErrorAction Stop -Verbose


#Load Json files.
try {
    $Script:JsonDeviceModels = Get-Content "$($DeployRoot)\Control\DeviceModels.json" -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
    $Script:JsonFirmwares    = Get-Content "$($DeployRoot)\Control\Firmwares.json" -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
    
}
catch {
    Write-Host $_.Exception.Message -BackgroundColor Red
}

function Main {

    begin {

        #Select device model.
        #$null -eq $Device ? ($SelectedDevice = $JsonDeviceModels | Out-GridView -OutputMode Single -Title "Select device model") : "$(($SelectedDevice = $Device))"
        if ($null -eq $Device) {
            $SelectedDevice = $JsonDeviceModels | Out-GridView -OutputMode Single -Title "Select device model"
        } else { 
            $SelectedDevice = $Device
        }
        if ($null -eq $SelectedDevice) { break }


        #Get Catalog.
        if ($null -eq $DeviceCatalog) {
            $DeviceCatalog = Get-DevicesCatalog -Links $JsonSettings.Catalog
            Write-Host "Total: $($DeviceCatalog.Count) models" -ForegroundColor DarkMagenta
        }

        #Filter catalog by Model and Type.
        #$DeviceCatalog.Count -gt 1 ? "$($ItemCatalog = $DeviceCatalog | Where-Object { $_.Model -eq $SelectedDevice.Model -and (($_.Types -split ',')[0] -in $SelectedDevice.Types)})" : "$($ItemCatalog = $DeviceCatalog)"
        if ($DeviceCatalog.Count -gt 1) {
            $ItemCatalog = $DeviceCatalog | Where-Object { $_.Model -eq $SelectedDevice.Model -and (($_.Types -split ',')[0] -in $SelectedDevice.Types)}
        } else {
            $ItemCatalog = $DeviceCatalog
        }

        if ($null -eq $ItemCatalog) {
            Write-Host "Nenhum item encontrado no catalogo." -ForegroundColor Red
            break
        }
    }
    process {

        $filename = Split-Path -Path $ItemCatalog.Link -Leaf
        $fullfilepath = "$($DeployRoot)\Firmwares\$($filename)"

        # Download Firmware.
        $fileIntegrity = Download -Url $ItemCatalog.Link -Destination $fullfilepath -Hash $ItemCatalog.Hash -ErrorAction SilentlyContinue
    
        # Check the file hash.
        if ($false -eq $fileIntegrity) {
            Write-host "Error downloading file or different hash." -ForegroundColor Red
            Remove-Item -Path $fullfilepath -Force -ErrorAction SilentlyContinue -Verbose
            break
        }


        if ($Script:JsonFirmwares.DeviceGuid -contains $SelectedDevice.Guid) {

            #Remove old file.
            $old = $Script:JsonFirmwares | Where-Object { $_.DeviceGuid -eq $SelectedDevice.Guid }
            if ($old.Version -ne $ItemCatalog.Version) {
                Remove-Item -Path "$($DeployRoot)$($old.Source)" -Force -ErrorAction SilentlyContinue -Verbose -Confirm
            }

            #Update firmware.
            $Script:JsonFirmwares | ForEach-Object {
                
                if ($_.DeviceGuid -eq $SelectedDevice.Guid) {

                    $_.FileName = $filename;
                    $_.Version  = $ItemCatalog.Version;
                    $_.Source   = "\Firmwares\$($filename)";
                    $_.Hash     = $ItemCatalog.Hash
                }
            }
        } else {

            $Script:JsonFirmwares += [ordered]@{ 

                DeviceGuid = "$($SelectedDevice.Guid)";
                FileName   = $filename;
                Version    = $ItemCatalog.Version;
                Source     = "\Firmwares\$($filename)";
                Hash       = $ItemCatalog.Hash;
                Enabled    = $true
            }
        }
    }
    end {
        #Update JsonFirmware.
        Write-Host "Add firmware for $($SelectedDevice.Model)." -ForegroundColor Green
        $Script:JsonFirmwares | ConvertTo-Json | Set-Content "$($DeployRoot)\Control\Firmwares.json"
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