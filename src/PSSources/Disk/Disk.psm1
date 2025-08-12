<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    Disk Module
	Created by: Gilmar Prust
	Filename:   Disk.psm1
    
    .NOTES
    New-Partition -offset 4294967296=4gb
	100gb*1024 = 30.720mb * 1024 = 31.457.280kb *1024 = 107374182400 bytes
     30gb*1024 = 30.720mb * 1024 = 31.457.280kb *1024 = 32212254720 bytes
      2gb*1024 = 2.048mb  * 1024 = 2.097.152kb  *1024 = 2147483648 bytes

+----------------------------------------------------------------------------------------+
#>

function Format-Disk {
    <#
        .DESCRIPTION
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $DiskNumber,
        [ValidateSet("GPT", "MBR")]
        $PartitionStyle
    )

    Write-Host "  Creating partition..." -ForegroundColor Yellow

    ### Clear Disk
    if ($PartitionStyle -match "(GPT|MBR|Unknown)"){
        Clear-Disk -Number $DiskNumber -RemoveData -RemoveOEM -confirm:$false -ErrorAction Stop
    }
    
    ### Desativa Autoplay das unidades.
    New-Item -Path "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoplayHandlers\EventHandlersDefaultSelection\StorageOnArrival" -Force | Out-Null
    Set-ItemProperty -Path "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoplayHandlers\EventHandlersDefaultSelection\StorageOnArrival" -Name "(default)" -Value "MSTakeNoAction" -Force | Out-Null
    New-Item -Path "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoplayHandlers\UserChosenExecuteHandlers\StorageOnArrival" -Force | Out-Null
    Set-ItemProperty -Path "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoplayHandlers\UserChosenExecuteHandlers\StorageOnArrival" -Name "(default)" -Value "MSTakeNoAction" -Force | Out-Null

    ### Create partitions, Format.
    Switch ($PartitionStyle) {

        "GPT" {
            Initialize-Disk -Number $DiskNumber -PartitionStyle GPT
            New-Partition -DiskNumber $DiskNumber -Size 260MB -GptType "{c12a7328-f81f-11d2-ba4b-00a0c93ec93b}" -AssignDriveLetter | 
            Format-Volume -FileSystem FAT32 -Force -NewFileSystemLabel "Boot" | Out-Null
            New-Partition -DiskNumber $DiskNumber -Size 16MB -GptType "{e3c9e316-0b5c-4db8-817d-f92df00215ae}" | Out-Null
            New-Partition -DiskNumber $DiskNumber -UseMaximumSize -AssignDriveLetter | 
            Format-Volume -FileSystem NTFS -Force -NewFileSystemLabel "System" | Out-Null
        }
        "MBR" {
            Initialize-Disk -Number $DiskNumber -PartitionStyle MBR
            New-Partition -DiskNumber $DiskNumber -Size 512MB -AssignDriveLetter -IsActive | 
            Format-Volume -FileSystem FAT32 -Force -NewFileSystemLabel "Boot" | Out-Null
            New-Partition -DiskNumber $DiskNumber -UseMaximumSize -AssignDriveLetter | 
            Format-Volume -FileSystem NTFS -Force -NewFileSystemLabel "System" | Out-Null
        }
    }

    ### Check if disk is operational.
    $disk_ = Get-Disk -Number $DiskNumber | Get-Partition | Get-Volume
    $disk_ | ForEach-Object {

        if ($_.OperationalStatus -ne 'OK' -and $_.HealthStatus -ne 'Healthy') {
            Throw "Error, disk not operational."
        }
    }
    return $disk_
}

function Get-DeployDisk {
    <#
        .DESCRIPTION
        Return first disk.
    #>
    [CmdletBinding()]
    param(
        [Parameter()] 
        [switch]$Auto
    )

    ### Get first disk auto.
    if ($Auto.IsPresent) {
        return Get-Disk | Sort-Object Number | Select-Object -First 1
    }

    $disk = Get-Disk | Sort-Object Number | Select-Object -Property Number,FriendlyName,SerialNumber,OperationalStatus,HealthStatus,PartitionStyle,Size

    if ($null -eq $disk) { Throw "Disk is null." }

    return $disk
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