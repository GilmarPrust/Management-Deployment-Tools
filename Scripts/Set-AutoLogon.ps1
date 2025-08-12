

function Set-AutoLogon {
    <#
        .DESCRIPTION
        Sets the auto logon configuration for a specified user in Windows.
        
        .PARAMETER Username
        The username for which to set the auto logon.
        
        .PARAMETER Password
        The password for the specified user. This should be a secure string.
        
        .PARAMETER Domain
        The domain of the user. Defaults to the current user's domain.
        
        .PARAMETER LogonCount
        The number of times the user should be automatically logged on. Defaults to 1.
        
        .EXAMPLE
        Set-AutoLogon -Username "nickname" -Password (ConvertTo-SecureString "p@ssw0rd" -AsPlainText) -Domain "DOMAIN" -LogonCount 1

        .NOTES
        This script modifies the Windows registry to enable auto logon for a specified user.
        Ensure you run this script with administrative privileges.

    #>
    param (
        [CmdletBinding(SupportsShouldProcess = $true)]
        [Parameter(Mandatory)]
        [string]$Username,
        [SecureString]$Password,
        [string]$Domain = "$($Env:UserDomain)",
        [int]$LogonCount = 1
    )
    # Validate parameters
    $regPath = "HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"
    if (-not (Test-Path $regPath)) {
        Write-Error "Registry path $regPath does not exist."
        return
    }
    Set-ItemProperty -Path $regPath -Name "DefaultUsername" -Value $Username -type String 
    Set-ItemProperty -Path $regPath -Name "DefaultPassword" -Value (ConvertFrom-SecureString $Password) -type String
    Set-ItemProperty -Path $regPath -Name "DefaultDomainName" -Value $DomainName -type String
    Set-ItemProperty -Path $regPath -Name "AutoAdminLogon" -Value "1" -Type String
    Set-ItemProperty -Path $regPath -Name "AutoLogonCount" -Value $LogonCount -Type String

    Write-Host "Auto logon settings updated for user: $Username" -ForegroundColor Green

}

Set-AutoLogon -Username "x0508gp" -Password (ConvertTo-SecureString "Yeshua@19" -AsPlainText) -Domain "DFS" -LogonCount 1