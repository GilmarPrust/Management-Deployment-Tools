<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    Firmware Module
    Created by: Gilmar Prust
    Filename: Firmware.psm1
+----------------------------------------------------------------------------------------+
#>
function New-Firmware {
    <##>
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$DeviceModelGuid,
        [string]$FileName,
        [string]$Version,
        [string]$Source,
        [string]$Hash
    )
    
    return @{
        DeviceModelGuid = $DeviceModelGuid;
        FileName = $FileName;
        Version = $Version;
        Source = $Source;
        Hash = $Hash;
        Enabled = $true
    }
}


function Get-Firmware {
    <##>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $DeviceModelGuid
    )
    ###
    Install-PSResource Control -Repository DCM2_PSResources

    $firmware = Import-Firmwares | Where-Object { $_.DeviceModelGuid -eq $DeviceModelGuid }

    return $firmware
}


function Add-Firmware {
    <#
        .DESCRIPTION
        Adiciona um item em Firmware.json.
        Este item precisa estar vinculado a um dispositivo de DeviceModels.json.

        .PARAMETER DeviceGuid
        Especifique o modelo de dispositivo obtido de DeviceModels.json.

        .PARAMETER InputObject
        Especifique o item do catalogo obtido de Get-DeviceModelCatalog.
    #>
    [CmdletBinding()]
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory)]
        [string]$DeviceModelGuid,
        [Object]$InputObject
    )
    
    begin {
        <##>
        [FirmwareList]::Initialize()

        [FirmwareList]::AddRange(@(Import-Firmwares))

        <# Verifica se DeviceGuid já existe. #>
        if ([FirmwareList]::Exists($DeviceModelGuid)) {
            Throw "Firmware alread exist."
        }

        Install-PSResource Download -Repository DCM2_PSResources

        $filename = Split-Path -Path $InputObject.Firmware -Leaf
        $fullfilepath = "$($DeployRoot)\Firmwares\$($filename)"

    }
    process {
        <##>
        ### Download Firmware and check Hash.
        Download -Url $InputObject.Firmware -Destination $fullfilepath -Hash $InputObject.Hash | Out-Null

        [FirmwareList]::Add(
            [Firmware]::new( 
                $DeviceModelGuid, 
                $filename, 
                $InputObject.Version,
                "\Firmwares\$($filename)",
                $InputObject.Hash
            )
        )
    }
    end {
        <##> 
        ### Save JsonFirmware.
        Save-Firmwares -Content ([FirmwareList]::Firmwares)
        return [FirmwareList]::Firmwares | Select-Object -Last 1
    }
}

function Invoke-Updatefirmware {

	$file = Get-Item "C:\Deploy\Firmware\*" -Filter "*.exe" -Exclude "Flash64W.exe" -Verbose -ErrorAction SilentlyContinue
	$fwversion = Get-Content -Path "C:\Deploy\Firmware\*.txt" -ErrorAction SilentlyContinue
	
	if (![string]::IsNullOrEmpty($file) -and ![string]::IsNullOrEmpty($fwversion)) { 

		if ($fwversion -gt $BIOSVersion) {
			Write-Host "Updating Firmware..." -ForegroundColor Red
			switch ($Manufac) {
				'Dell Inc.' { 
					$arg = "/b=$($file.FullName) /s /l=C:\Temp\FlashBios.txt"
					Start-Process -Wait "C:\Deploy\Firmware\Flash64W.exe" -ArgumentList $arg -PassThru
				 }
				'LENOVO' { 
					$arg = "/SILENT /NORESTART /LOG=C:\Temp\FlashBios.txt"
					Start-Process -Wait "$($file.FullName)" -ArgumentList $arg -PassThru
				}
				'Hewlett-Packard' {  }
			}
		}
	}
}

function Copy-Firmware {
    <#
        .DESCRIPTION
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $Firmware,
        $Destination
    )
    Write-Host "Copying Firmware..." -ForegroundColor Yellow

    New-Item -Path $Destination -ItemType Directory -Force | Out-Null

    Copy-Item -Path "$($DeployRoot)$($Firmware.Source)" -Destination "$($Destination)" -Force

    New-Item -Path "$($Destination)" -Name "fwVersion.txt" -Value "$($Firmware.Version)" -ItemType File | Out-Null

    if ($global:deployInfo.DeviceModel.Manufacturer -eq 'Dell Inc.') {
        Copy-Item -Path "$($DeployRoot)\Firmwares\Dell\Flash64W.exe" -Destination "$($Destination)" -Force
    }
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