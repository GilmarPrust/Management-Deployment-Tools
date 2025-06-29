<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    Drivers Module
	Created by: Gilmar Prust
	Filename: Drivers.psm1
    
    .NOTES
    Use to deploy drivers on offline or online system.
+----------------------------------------------------------------------------------------+
#>

function Expand-DriverPack {
    <#
        .DESCRIPTION
        Extract driverpack to C:\Drivers

        .NOTES

    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $FilePath,
        $Destination,
        $Manufacturer
    )

    Write-Host "Expanding drivers..." -ForegroundColor Yellow -NoNewline

    $file = Get-Item -Path $FilePath -ErrorAction Continue

    if (-not(Test-Path $FilePath -PathType Leaf)) {
        Write-Information "Driver Pack not found. [$($FilePath)]"
        continue
    }

    switch ($file.Extension) { 
        '.7z'  { 
            Start-Process -Wait -FilePath "$($DeployRoot)\Tools\7z\x64\7za.exe" -ArgumentList "x $($file.FullName)", "-o$($Destination) -y -xr!x86 -xr!amd -xr!nvidia" 
        }
        '.CAB' { 
            Start-Process -Wait -FilePath "$($DeployRoot)\Tools\7z\x64\7za.exe" -ArgumentList "x $($file.FullName)", "-o$($Destination) -y -xr!x86 -xr!amd -xr!nvidia" 
        }
        '.exe' {

            if($Manufacturer -eq "ASUS") {
                Start-Process -Wait -FilePath "$($DeployRoot)\Tools\MiniUnz\miniunz.exe" -ArgumentList "-x $($file.FullName)", "-d $($Destination)"
            }
            if($Manufacturer -eq "Lenovo") {
                Start-Process -Wait -FilePath "$($file.FullName)" -ArgumentList "/verysilent", "/DIR=$($Destination)"
            }
            if($Manufacturer -eq "HP") {
                Start-Process -Wait -FilePath "$($file.FullName)" -ArgumentList "/silent", "/DIR=$($Destination)"
            }
        }
        Default { Write-Host "Unconfigured file extension." -ForegroundColor Red }
    }

    ### Exclude x86 drivers
    Get-ChildItem $Destination -recurse | Where-Object { $_.PSIsContainer -eq $true -and $_.Name -match 'x86' -and $_.GetFiles().Count -eq 0 } | Remove-Item -Recurse -Verbose

    Write-Host " Done!" -ForegroundColor Green
}

function Add-DriversOffline {
    <#
        .DESCRIPTION
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        $DriveSystem,
        $DriverPath
    )

    Write-Host "Adding Drivers..." -ForegroundColor Yellow -NoNewline

    Start-Process -Wait -WindowStyle Maximized -FilePath "powershell" -ArgumentList "&{ Add-WindowsDriver -Path $($DriveSystem) -Driver $($DriverPath) -Recurse -Verbose -ErrorAction Continue | Out-Null }"
    #Add-WindowsDriver -Path $global:DriveSystem -Driver $DriverPath -Recurse -Verbose -ErrorAction Continue | Out-Null

    Write-Host " Done!" -ForegroundColor Green
}

function Add-DriversOnline {
    <#
        .DESCRIPTION
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        $DriverPath
    )

    Write-Host "Adding Drivers..." -ForegroundColor Yellow

    Start-Process -Wait -WindowStyle Maximized -FilePath "powershell" -ArgumentList "&{ Add-WindowsDriver -Path $($global:DriveSystem)\ -Driver $($DriverPath) -Recurse -Verbose -ErrorAction Continue | Out-Null }"
    #Add-WindowsDriver -Path $global:DriveSystem -Driver $DriverPath -Recurse -Verbose -ErrorAction Continue | Out-Null
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