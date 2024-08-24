<#
+----------------------------------------------------------------------------------------+
    .Description
    Add DriverPack
	Created by: Gilmar Prust
	Filename:   Add-DriverPack.ps1
+----------------------------------------------------------------------------------------+
#>
param (
    [Object[]]$Device
)

New-Variable -Name DeployRoot -Value "$($PSScriptRoot)" -Scope Global -Force -Option ReadOnly
Set-Location $DeployRoot -Verbose

#IMPORT MODULES
Import-Module .\Scripts\Modules\Download -Force -Global -ErrorAction Stop -Verbose
Import-Module .\Scripts\Modules\Catalog  -Force -Global -ErrorAction Stop -Verbose

#Load Json files.
try {
    $Script:JsonDeviceModels = Get-Content "$($DeployRoot)\Control\DeviceModels.json" -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
    $Script:JsonDriverPacks  = Get-Content "$($DeployRoot)\Control\DriverPacks.json" -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
    $Global:JsonSettings     = Get-Content "$($DeployRoot)\Control\Settings.json" -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
}
catch {
    Write-Host $_.Exception.Message -BackgroundColor Red
}


function Main {    
    begin {
        
        #Select device model.
        if ($null -eq $Device) {
            $SelectedDevice = $JsonDeviceModels | Out-GridView -OutputMode Single -Title "Select device model"
        } else { 
            $SelectedDevice = $Device
        }
        if ($null -eq $SelectedDevice) { break }

        #Select item from Catalog.
        $DriverPackCatalog = Get-DriverPackCatalog -Links $JsonSettings.Catalog.DriverPack
        Write-Host "Total: $($DriverPackCatalog.Count) models" -ForegroundColor DarkMagenta
        
        $temp = $DriverPackCatalog | Out-GridView -OutputMode Single
        #Filter catalog by Model and Type.
        $DriverPackCatalog = $DriverPackCatalog | Where-Object { $_.Model -eq $SelectedDevice.Model -and (($_.Types -split ',')[0] -in $SelectedDevice.Types)}
        Write-Host "$($DriverPackCatalog.Count) 1"
        
        #Select Driverpack to download.
        #$driverpacks = $DriverPackCatalog | Where-Object { $_.Manufacturer -eq $SelectedDevice.Manufacturer -and $_.Model -eq $SelectedDevice.Model } 
        $selectedDriverPacks = $DriverPackCatalog | Out-GridView -Title "Select Driverpack to download" -OutputMode Multiple
        if ($null -eq $selectedDriverPacks) { break }
    }
    process {
        <#
            .NOTES
            Select device model in GridView and create new object Device.
        #>
        foreach ($itemDownload in $selectedDriverPacks) {

            $filename = Split-Path -Path $itemDownload.Link -Leaf
            $filepath = Join-Path -Path "$($DeployRoot)\Drivers" -ChildPath $filename
            $checkfilehash = $false
        
            # Verifica o arquivo se ja existe.
            if (Test-Path -Path $filepath -PathType Leaf) {
                Write-Host "File alread exists: $($filepath)" -ForegroundColor Cyan
                $checkfilehash = CheckFileHash -File $filepath -Hash $itemDownload.Hash -ErrorAction SilentlyContinue
                if ($false -eq $checkfilehash) { 
                    Remove-Item -Path $filepath -Force -ErrorAction SilentlyContinue -Verbose 
                }
            }
        
            # Download DriverPack.
            if ($false -eq $checkfilehash) {
                $checkfilehash = Download -Url $itemDownload.Link -Destination $filepath -Hash $itemDownload.Hash -ErrorAction SilentlyContinue
            }
        
            # Check the file hash.
            if ($false -eq $checkfilehash) {
                Write-host "Error downloading file or different hash." -ForegroundColor Red
                Remove-Item -Path $filepath -Force -ErrorAction SilentlyContinue -Verbose
                continue
            }
        
            # Remove old file.
            $oldfile = ($Script:JsonDriverPacks | Where-Object {($_.DeviceGuid -eq $itemDownload.DeviceGuid) -and ($_.OS -eq $itemDownload.OS)}).Source
            Remove-Item -Path "$($DeployRoot)$($oldfile)" -Force -ErrorAction SilentlyContinue -Verbose
        
            $Script:JsonDriverPacks += [ordered]@{ 
                #Add new driverPack in DriverPacks.json.
                DeviceGuid = $SelectedDevice.Guid; 
                Enabled = $true;
                FileName = $filename;
                OS = $itemDownload.OS; 
                Version = $itemDownload.Version; 
                Source = "\Drivers\$($filename)"; 
                Hash = $itemDownload.Hash
            }
        }
    }
    end {
        <#
            .NOTES
            Update JsonDriverPacks.
        #>
        #Update Json
        Write-Host "Updated driverpack for $($itemDownload.Model)." -ForegroundColor Green
        $Script:JsonDriverPacks | ConvertTo-Json | Set-Content "$($DeployRoot)\Control\DriverPacks.json"
    }
}
Main


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
