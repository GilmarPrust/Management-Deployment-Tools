<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    Control Module
    Created by: GilmarPrust
    Filename:   Control.psm1
    .NOTES
    Usado apenas para controlar leitura e gravação nos arquivos Json
    verificando hash do arquivo
+----------------------------------------------------------------------------------------+
#>

function CheckHash {
    <#
        .SYNOPSIS
        Checks file hash before writing.
    #>
    param (
        $Hash
    )
}

function Import-DeviceProfile {

    $jsonFile = "$($PSScriptRoot)\DeviceProfile.json"
    return Get-Content $jsonFile  -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
}
function Save-DeviceProfile {
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory, ValueFromPipeline)]
        $Content
    )
    $Content | ConvertTo-Json -ErrorAction Stop | Set-Content "$($PSScriptRoot)\DeviceProfile.json" -ErrorAction Stop
}
function Import-Profiles {

    $jsonFile = "$($PSScriptRoot)\Profiles.json"
    return Get-Content $jsonFile  -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
}
function Import-Images {

    $jsonFile = "$($PSScriptRoot)\Images.json"
    if (-not (Test-Path -Path $jsonFile)) { New-Item -Path $jsonFile -ItemType File -Value "[]" -Force | Out-Null }
    return Get-Content $jsonFile  -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
}
function Save-Images {
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory, ValueFromPipeline)]
        $Content
    )
    $Content | ConvertTo-Json -ErrorAction Stop | Set-Content "$($PSScriptRoot)\Images.json" -ErrorAction Stop
}

function Save-Settings {
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory, ValueFromPipeline)]
        $Content
    )
    $Content | ConvertTo-Json -ErrorAction Stop | Set-Content "$($PSScriptRoot)\Settings.json" -ErrorAction Stop
}
function Import-Firmwares {

    $jsonFile = "$($PSScriptRoot)\Firmwares.json"
    if (-not (Test-Path -Path $jsonFile)) { New-Item -Path $jsonFile -ItemType File -Value "[]" -Force | Out-Null }
    return Get-Content $jsonFile -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
}
function Save-Firmwares {
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory, ValueFromPipeline)]
        $Content
    )
    $Content | ConvertTo-Json -ErrorAction Stop | Set-Content "$($PSScriptRoot)\Firmwares.json" -ErrorAction Stop
}
function Import-DriverPacks {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          

    $jsonFile = "$($PSScriptRoot)\DriverPacks.json"
    if (-not (Test-Path -Path $jsonFile)) { New-Item -Path $jsonFile -ItemType File -Value "[]" -Force | Out-Null }
    return Get-Content $jsonFile -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
}
function Save-DriverPacks {
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory, ValueFromPipeline)]
        $Content
    )
    $Content | ConvertTo-Json -ErrorAction Stop | Set-Content "$($PSScriptRoot)\DriverPacks.json" -ErrorAction Stop
}
function Import-DeviceModels {

    $jsonFile = "$($PSScriptRoot)\DeviceModels.json"
    if (-not (Test-Path -Path $jsonFile)) { New-Item -Path $jsonFile -ItemType File -Value "[]" -Force | Out-Null }
    return Get-Content $jsonFile -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
}
function Save-DeviceModels {
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory, ValueFromPipeline)]
        $Content
    )
    $Content | ConvertTo-Json -ErrorAction Stop | Set-Content "$($PSScriptRoot)\DeviceModels.json" -ErrorAction Stop
}
function Import-Applications {

    $jsonFile = "$($PSScriptRoot)\Applications.json"
    if (-not (Test-Path -Path $jsonFile)) { New-Item -Path $jsonFile -ItemType File -Value "[]" -Force | Out-Null }
    return Get-Content $jsonFile -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
}
function Save-Applications {
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory, ValueFromPipeline)]
        $Content
    )
    $Content | ConvertTo-Json -ErrorAction Stop | Set-Content "$($PSScriptRoot)\Applications.json" -ErrorAction Stop
}
function Import-Devices {

    $jsonFile = "$($PSScriptRoot)\Devices.json"
    if (-not (Test-Path -Path $jsonFile)) { New-Item -Path $jsonFile -ItemType File -Value "[]" -Force | Out-Null }
    return Get-Content $jsonFile  -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
}
function Save-Devices {
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory, ValueFromPipeline)]
        $Content
    )
    $Content | ConvertTo-Json -ErrorAction Stop | Set-Content "$($PSScriptRoot)\Devices.json" -ErrorAction Stop
}
function Import-DeviceApplications {

    $jsonFile = "$($PSScriptRoot)\DeviceApplications.json"
    if (-not (Test-Path -Path $jsonFile)) { New-Item -Path $jsonFile -ItemType File -Value "[]" -Force | Out-Null }
    return Get-Content $jsonFile  -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
}
function Import-DeviceModelApplications {

    $jsonFile = "$($PSScriptRoot)\DeviceModelApplications.json"
    if (-not (Test-Path -Path $jsonFile)) { New-Item -Path $jsonFile -ItemType File -Value "[]" -Force | Out-Null }
    return Get-Content $jsonFile  -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
}
function Import-DeviceIDOffice {

    $jsonFile = "$($PSScriptRoot)\DeviceIDOffice.json"
    if (-not (Test-Path -Path $jsonFile)) { New-Item -Path $jsonFile -ItemType File -Value "[]" -Force | Out-Null }
    return Get-Content $jsonFile  -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
}
function Import-BlackListAppx {

    $jsonFile = "$($PSScriptRoot)\BlackListAppx.json"
    if (-not (Test-Path -Path $jsonFile)) { New-Item -Path $jsonFile -ItemType File -Value "[]" -Force | Out-Null }
    return Get-Content $jsonFile  -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
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