<#
+----------------------------------------------------------------------------------------+
    .Description
    Features
	Created by: Gilmar Prust
	Filename:   Features.psm1
+----------------------------------------------------------------------------------------+
#>

function Enable-Features {
    
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
        $Source,
        $Destination
    )
    #Enable Optional Features
    Write-Host "Enabling optional features..." -ForegroundColor Yellow -NoNewline
    $source = Split-Path -Path "$($Global:DeployRoot)$($Source)" -Parent
    Enable-WindowsOptionalFeature  -Path "$($Destination)\" -FeatureName "NetFx3" -Source "$($source)\sxs" -LimitAccess | Out-Null
    Enable-WindowsOptionalFeature  -Path "$($Destination)\" -FeatureName "SMB1Protocol-Client" -LimitAccess -All | Out-Null
    Disable-WindowsOptionalFeature -Path "$($Destination)\" -FeatureName "SMB1Protocol-Deprecation" -NoRestart | Out-Null
    Disable-WindowsOptionalFeature -Path "$($Destination)\" -FeatureName "SMB1Protocol-Server" -NoRestart | Out-Null
    Write-Host " Done!" -ForegroundColor Green
}