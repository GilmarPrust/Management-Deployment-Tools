<#
    .Description
    Drivers
	Created by: Gilmar Prust
	Filename:   Drivers.psm1
#>
function Add-Drivers {
    
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $Destination
    )
    Write-Host "Adding Drivers..." -ForegroundColor Yellow
    Start-Process -Wait -WindowStyle Maximized -FilePath "powershell" -ArgumentList "&{ Add-WindowsDriver -Path $($Destination) -Driver $($Destination)\Drivers -Recurse -Verbose -ErrorAction Continue | Out-Null }"
    #Add-WindowsDriver -Path "$($letterSystem)" -Driver $DriverPath -Recurse -Verbose -ErrorAction Continue | Out-Null
}