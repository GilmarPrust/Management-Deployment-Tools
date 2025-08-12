<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    Installer Module
    Created by: Gilmar Prust
    Filename: Installer.psm1
+----------------------------------------------------------------------------------------+
#>

function Install-Program {
    <#
        .DESCRIPTION
    #>
	param (
        [ValidateNotNullOrEmpty()]
        $List
    )
	Write-Host "Installing $($app.DisplayName)..." -ForegroundColor Yellow -NoNewline
    switch -Wildcard ($app.File) {
        
        "*.exe" { 
            Start-Process -wait -FilePath "$($app.File)" -ArgumentList "$($app.Argument)" -WorkingDirectory "$($PSScriptRoot)$($app.WorkDir)" 
        }
        "*.msi" { 
            Start-Process -wait -FilePath "msiexec.exe"  -ArgumentList "/i $($app.File) $($app.Argument)" -WorkingDirectory "$($PSScriptRoot)$($app.WorkDir)" 
        }
        "*.bat" {
            if([string]::IsNullOrEmpty($app.Argument)) {
                Start-Process -wait -FilePath "$($app.File)" -WorkingDirectory "$($PSScriptRoot)$($app.WorkDir)"
            } else {
                Start-Process -Wait -FilePath "$($app.File)" -ArgumentList "$($app.Argument)" -WorkingDirectory "$($PSScriptRoot)$($app.WorkDir)"
            }
        }
        "*.ps1" {
            #Invoke-Command -FilePath "$($PSScriptRoot)$($app.Source)\$($app.File)" -ComputerName localhost -ArgumentList "$($PSScriptRoot)$($app.Source)"
            start-process PowerShell -ArgumentList "-ExecutionPolicy Bypass -File $($PSScriptRoot)$($app.Source)\$($app.File)" -WindowStyle Maximized -Verb RunAs
        }
    }
	Write-Host " Done!" -ForegroundColor Green
}

if (Test-Path "$($PSScriptRoot)\Applications\AppToInstall.json" -PathType Leaf) {

	#List of Applications
	$JsonApps = Get-Content "$($PSScriptRoot)\Applications\AppToInstall.json" | ConvertFrom-Json
	$SelectedApps = $JsonApps | Select-Object -Property DisplayName, File, Argument, Source, WorkDir | Out-GridView -OutputMode Multiple -Title "Select Applications"
	Install-Programs -List $SelectedApps
}