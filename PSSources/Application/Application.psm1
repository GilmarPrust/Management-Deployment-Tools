<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    Application Module
    Created by: Gilmar Prust
    Filename: Application.psm1

    .NOTES
    Use to get or add application on repository.
+----------------------------------------------------------------------------------------+
#>

function New-Application {
    <##>
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$NameID,
        [string]$DisplayName,
        [string]$Version,
        [string]$FileName,
        [string]$Hash,
        [string]$Argument,
        [string]$Source
    )
    
    return @{
        Guid = (New-Guid).Guid;
        Enabled = $True;
        NameID = $NameID;
        DisplayName = $DisplayName;
        Version = $Version;
        FileName = $FileName;
        Argument = $Argument;
        Source = $Source
    }
}



function Add-Application {
    <#
        .DESCRIPTION
        Adiciona um item em Firmware.json.
        Este item precisa estar vinculado a um dispositivo de DeviceModels.json.
        
        .PARAMETER NameID
        Especifique o modelo de dispositivo obtido de DeviceModels.json.

        .OUTPUTS
        Return Object last add.
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory=$false)]
        [ValidateNotNullOrEmpty()]
        [string]$NameID,
        [string]$DisplayName,
        [string]$Version,
        [string]$FileName,
        [string]$Argument,
        [string]$Source
    )

    begin {
        ###
        Install-PSResource Control -Repository DCM2_PSResources

        $applications = Import-Applications

        <# Verifica se o aplicativo já existe. #>
        <# if ([ApplicationList]::Exists($NameID, $FileName, $Version, $Source)) {
            Throw "Application alread exist."
        } #>
    }
    process {
        ###
        $applications =+ (New-Application -NameID $NameID -DisplayName $DisplayName -Version $Version -FileName $FileName -Argument $Argument -Source $Source)

    }
    end {
        ###
        Save-Applications -InputObject $applications

        return $applications | Select-Object -Last 1
    }
}


function Copy-Applications {
    <#
        .DESCRIPTION
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $ListApps,
        $Destination
    )
    ###
    Install-PSResource Control -Repository DCM2_PSResources

    Write-Host "Copying applications..." -ForegroundColor Yellow

    New-Item -Path $Destination -ItemType Directory -Force | Out-Null

    ### Get all applications from repository.
    $repository = Import-Applications

    $applications = @()

    foreach ($guid in $ListApps) {

        $app = $repository | Where-Object { $_.Guid -eq $guid -and $_.Enabled -eq $true }
        if ($null -eq $app) { continue }

        Write-Host "Copying... $($app.DisplayName) to $($Destination)" -ForegroundColor Gray

        Copy-Item -Path "$($DeployRoot)$($app.Source)" -Destination $Destination -Recurse -Force

        if (Test-Path -Path "$($Destination)\$($app.NameID)\$($app.FileName)" -PathType Leaf) {
            $app.Source = "C:\Deploy\Applications\$($app.NameID)"
            $applications += $app
        }
    }
    $applications | ConvertTo-Json | Out-File "$($Destination)\Applications.json"
}

function Install-Applications {
    <#
        .DESCRIPTION
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $ListApps
    )
    Write-Host "Installing applications..." -ForegroundColor Yellow
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