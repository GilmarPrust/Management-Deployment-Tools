<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
        Add-Firmware
        Created by: Gilmar Prust
        Filename: Add-Firmware.ps1

    .EXAMPLE
        $firmware = @{ 
            DeviceGuid = "";
            FileName   = "";
            Version    = "";
            Source     = "";
            Hash       = "";
            Enabled    = bool
        }
+----------------------------------------------------------------------------------------+
#>
[CmdletBinding()]
param (
    [Parameter(Mandatory = $false)]
    [ValidateCount(1, 1)]
    [Object[]]$DeviceModel,
    [Object[]]$ItemCatalog
)

### GLOBAL VARIABLES
New-Variable -Name DeployRoot -Value "$($PSScriptRoot)" -Scope Global -Force -Option ReadOnly
Set-Location $DeployRoot -Verbose

### IMPORT MODULES
Import-Module .\Modules\Firmware -Force -Global -ErrorAction Stop -Verbose

function Main {

    begin {
        <##>
        if ($null -eq $DeviceModel) {
            Import-Module .\Modules\DeviceModel -Force -ErrorAction Stop -Verbose
            #Filter
            $DeviceModel = Get-DeviceModels | Out-GridView -OutputMode Single -Title "Select Device"
            if ($null -eq $DeviceModel) { break }
        }

        if ($null -eq $ItemCatalog) {
            Import-Module .\Modules\DeviceModelCatalog -Force -ErrorAction Stop -Verbose
            #Filter
            $ItemCatalog = Get-DeviceModelCatalog | Where-Object { 
                $_.Manufacturer -eq $DeviceModel.Manufacturer -and 
                $_.Model -eq $DeviceModel.Model -and 
                $_.Types -match $DeviceModel.Type 
            }
        }

        if($ItemCatalog.Count -gt 1) {
            $ItemCatalog = $ItemCatalog | Out-GridView -OutputMode Single -Title "Select device model from catalog"
        }
        if ($null -eq $ItemCatalog) { break }

    }
    process {
        <##>
        $newfirmware = Add-Firmware -DeviceModelGuid $DeviceModel.Guid -InputObject $ItemCatalog       

    }
    end {
        <##>
        Write-Host "New firmware add for $($DeviceModel.Model)." -ForegroundColor Green
        return $newfirmware
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