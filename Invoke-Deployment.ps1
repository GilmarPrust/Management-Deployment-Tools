#Requires -RunAsAdministrator
<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    Deployment System.

    .NOTES
    Created by: Gilmar Prust
	Filename: Invoke-Deployment.ps1

    .PARAMETER BareMetal
    Este cenário ocorre quando existe um dispositivo sem nenhum SO instalado que precisa de ser implementado. 
    Este cenário também pode ser um dispositivo existente que precisa de ser apagado e reimplementado sem ter de preservar quaisquer dados existentes.

    .PARAMETER Refresh
    O processo costuma ser iniciado no sistema operacional em execução. 
    É feito o backup das configurações e dos dados do usuário, depois restaurados como parte do processo de implantação.

    .PARAMETER Replace
    A substituição de um computador é semelhante ao cenário de atualização. 
    No entanto, uma vez que estamos a substituir o dispositivo, dividimos este cenário em duas tarefas main:
    cópia de segurança do cliente antigo e implementação bare-metal do novo cliente.

    .PARAMETER Resume
    Continua deploy após reinicializar o dispositivo.
+----------------------------------------------------------------------------------------+
#>
[CmdletBinding(DefaultParameterSetName = 'BareMetal')]
param(
    [Parameter(ParameterSetName="BareMetal")] [switch]$BareMetal,
    [Parameter(ParameterSetName="Refresh")] [switch]$Refresh,
    [Parameter(ParameterSetName="Replace")] [switch]$Replace,
    [Parameter(ParameterSetName="Resume")] [switch]$Resume
)
###
Import-Module PowerShellGet -Scope Global -Force

### Implatar get from API.
$settings = Get-Content "$($PSScriptRoot)\Settings.json" -ErrorAction SilentlyContinue | ConvertFrom-Json -ErrorAction SilentlyContinue
if ($null -eq $settings) {
    $settings = Get-Content "$($PSScriptRoot)\Settings.json" -ErrorAction SilentlyContinue | ConvertFrom-Json -ErrorAction SilentlyContinue
}
### Get local Settings.
if (Test-Path -Path "$($PSScriptRoot)\Settings.json" -PathType Leaf) {
    $settings = Get-Content "$($PSScriptRoot)\Settings.json" | ConvertFrom-Json
}

### Registra repositorio do powerShell.
Register-PSResourceRepository -Name $settings.PSResources.Name  -Uri $settings.PSResources.Uri -Trusted -Priority 100 -Force

### Define variavel global.
New-Variable -Name DCM2Root -Value $settings.DCM2Root -Scope Global -Force -Option ReadOnly

### Define localização da pasta raiz do DCM.
Set-Location -Path $DCM2Root -Verbose

### Configura o computador para receber comandos remotos.
#Enable-PSRemoting -SkipNetworkProfileCheck -Force

Remove-Item C:\Users\Gilma\OneDrive\Documentos\PowerShell -Force -Recurse -ErrorAction SilentlyContinue

### Install Deployment module.
Install-PSResource Deployment -Repository DCM2_PSResources -Reinstall -Verbose


###
switch ($psCmdlet.ParameterSetName) {
    ###
    "BareMetal" { 
        Invoke-DeploySystem -BareMetal
    }
    "Refresh" { 
        Invoke-DeploySystem -Refresh
    }
    "Replace" {
        Invoke-DeploySystem -Replace
    }
    "Resume" { 
        Invoke-DeploySystem -Resume
    }
}
### proximo passo, definir como este script será invokado, atraves de API?


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
