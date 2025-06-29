<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    Download Module
	Created by: Gilmar Prust
	Filename:   Download.psm1
+----------------------------------------------------------------------------------------+
#>

function Download {
    <#
    .SYNOPSIS
        Download
    .PARAMETER Url
        Url of file.
    .PARAMETER Destination
        Set destination directory
    .PARAMETER Hash
        Hash file
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$Url,
        [Parameter(Mandatory=$false)]
        [string]$Destination,
        [string]$Hash
    )
    Begin {

        Write-Host "Downloading..." -ForegroundColor Cyan

        ### IMPORT MODULES
        Install-PSResource Control -Repository DCM2_PSResources
        
        #$PSStyle.Progress.Style = "`e[95m"
        #Set variables
        $WebClient = New-Object -TypeName System.Net.WebClient
        $WebClient.AllowReadStreamBuffering = $true
        $WebClient.AllowWriteStreamBuffering = $true
        $Global:DownloadComplete = $false

        <# PROXY #>
        if ((Import-JsonSettings).Proxy.Enabled -eq $true) {

            <# $WebProxy  = New-Object -TypeName System.Net.WebProxy
            #Get credential current User
            $User = [System.Security.Principal.WindowsIdentity]::GetCurrent()

            if(($User.Name -match 'WEGNET')) {
                $WebProxy.Credentials = [System.Net.CredentialCache]::DefaultNetworkCredentials
            } else {
                $Credential = Get-Credential -Message "Please enter your user name and password." -UserName $User.Name -Verbose
                $WebProxy.Credentials = $Credential
            }
            $WebProxy.Address = "$($Global:JsonSettings.Proxy.Address):$($Global:JsonSettings.Proxy.Port)"
            $WebClient.Proxy = $WebProxy #>
        }
        
        #Unregister Events
        Get-EventSubscriber -Force | Unregister-Event -Force

        ### Set temporary folder if $Destination not specified.
        if ([string]::IsNullOrEmpty($Destination)) {
            $Destination = [System.IO.Path]::GetTempPath()
        }

        ### Check Destination exist.
        if(Test-Path -Path $Destination -PathType Container) {
            $Destination = Join-Path $Destination (split-path -leaf $Url)
        }

        ### Register events for download progress.
        Register-ObjectEvent $WebClient DownloadFileCompleted -SourceIdentifier WebClient.DownloadFileComplete -Action { $Global:DownloadComplete = $true } | Out-Null
        Register-ObjectEvent $WebClient DownloadProgressChanged -SourceIdentifier WebClient.DownloadProgressChanged -Action { $Global:DPCEventArgs = $EventArgs } | Out-Null
    }
    Process {

        ### Start download file.
        Write-Host $Url
        Write-Host $Destination

        $WebClient.DownloadFileAsync($Url, $Destination)

        ### Download progress.
        do {
            $PercentComplete = $Global:DPCEventArgs.ProgressPercentage
            $DownloadedBytes = $Global:DPCEventArgs.BytesReceived
            if ($null -ne $DownloadedBytes) {
                Write-Progress -Activity "Downloading" -Id 1 -Status "Downloaded bytes: $($DownloadedBytes)" -PercentComplete $PercentComplete
            }
        }
        until ($Global:DownloadComplete)
        Write-Progress -Activity "Downloading" -Id 1 -Status "Completed." -Completed

    }
    End {
        ### Dispose
        $WebClient.Dispose()
        ### Unregister Events
        Get-EventSubscriber | Unregister-Event -Force

        ### Check file hash
        if (-not [string]::IsNullOrEmpty($Hash) ) {
            CheckFileHash -File $Destination -Hash $Hash
        }

        return (Get-Item -Path $Destination -ErrorAction Stop).FullName
    }
}

function CheckFileHash {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [string]$File,
        [string]$Hash
    )
    <##>
    Write-Host "Checking file Hash... " -ForegroundColor Blue -NoNewline
    
    switch ($Hash.Length) {
        '32' { $fileHash = (Get-FileHash -Path $File -Algorithm MD5).Hash }
        '64' { $fileHash = (Get-FileHash -Path $File -Algorithm SHA256).Hash }
    }

    if ($fileHash -ne $Hash) { 
        Remove-Item -Path $File -Force -ErrorAction SilentlyContinue -Verbose
        Throw "Invalid file hash: $($fileHash)"
    }
    Write-Host "OK. " -ForegroundColor DarkGreen
}

Export-ModuleMember -Function Download


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