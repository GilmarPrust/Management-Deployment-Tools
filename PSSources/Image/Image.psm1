<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    Image Module
	Created by: Gilmar Prust
	Filename: Image.psm1

    .NOTES
    New-Partition -offset 2147483648
+----------------------------------------------------------------------------------------+
#>

function Invoke-ApplyImage {
    <#
        .DESCRIPTION
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $ImageFile,
        $Index,
        $ApplyPath

    )
    
    Write-Host "  Applying Windows Image..." -ForegroundColor DarkYellow

    try {
        #Expand-WindowsImage -ImagePath "$($global:DeployRoot)$($Image.Source)" -Index "$($Image.ImageIndex)" -ApplyPath "$($global:DriveSystem)\" -CheckIntegrity -ErrorAction Stop
        #Start-Process -Wait -FilePath wimlib-imagex.exe -WorkingDirectory "$($global:DeployRoot)\Tools\WinLib" -ArgumentList "apply $($global:DeployRoot)$($Image) $($Index) $($global:DriveSystem)"
        Invoke-Command -ScriptBlock { 

            Push-Location -Path "$($global:DeployRoot)\Tools\amd64\DISM"
            Get-Location
            Dism.exe /Apply-Image /ImageFile:"$($ImageFile)" /ApplyDir:"$($ApplyPath)\" /Index:"$($Index)" /CheckIntegrity
            Pop-Location
        }
        Write-Host $null
    }
    catch {
        throw $_.Exception.Message
    }
}

function New-BcdBootStore {
    <#
        .DESCRIPTION
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $DriveBoot,
        $DriveSystem

    )
    Write-Host "  Creating BCDboot store..." -ForegroundColor Yellow

    ### 
    $volume = Get-Volume -DriveLetter ($DriveSystem.TrimEnd(":"))
    $partition = Get-Partition -Volume $volume
    $firmwareType = (Get-Disk -Number $partition.DiskNumber).PartitionStyle

    Invoke-Command -ScriptBlock { 

        Push-Location -Path "C:\Windows\System32"
        Set-Location -Path "$($DriveSystem)\Windows\System32"
        Switch ($firmwareType){
            "GPT" { bcdboot.exe "$($DriveSystem)\Windows" /s $DriveBoot /f UEFI }
            "MBR" { bcdboot.exe "$($DriveSystem)\Windows" /s $DriveBoot /f BIOS }
        }
        Pop-Location
    }
    return $true
}

function Build-Image {
    <#
        .DESCRIPTION
        Build Windows Image.
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
        $Image
    ) 

}

function Get-Image {
    <#
        .DESCRIPTION
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $Guid
    )

    Install-PSResource Control -Repository DCM2_PSResources

    $image = Import-Images | Where-Object { $_.Guid -eq $Guid }

    if ($null -eq $image) { Throw "Image not found." }

    return $image
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