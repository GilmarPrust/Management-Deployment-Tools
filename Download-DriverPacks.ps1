<#
+----------------------------------------------------------------------------------------+
    .Description
    Download Driver Packs
	Created by: Gilmar Prust
	Filename:   Download-DriverPacks.ps1
+----------------------------------------------------------------------------------------+
#>
[CmdletBinding()]
param (
	[string]$Manufacturer,
    [string]$Model,
    [string]$Type
)

New-Variable -Name DeployRoot -Value "$($PSScriptRoot)" -Scope Global -Force -Option ReadOnly
Set-Location $DeployRoot -Verbose

#IMPORT MODULES
Import-Module .\Scripts\Modules\Download -Force -Global -ErrorAction Stop -Verbose
Import-Module .\Scripts\Modules\Firmware -Force -Global -ErrorAction Stop -Verbose
Import-Module .\Scripts\Modules\Catalog  -Force -Global -ErrorAction Stop -Verbose

#Load Json files.
try {
    $Script:JsonDeviceModels = Get-Content "$($DeployRoot)\Control\DeviceModels.json" -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
    $Script:JsonDriverPacks  = Get-Content "$($DeployRoot)\Control\DriverPacks.json" -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
    $Script:JsonSettings     = Get-Content "$($DeployRoot)\Control\Settings.json" -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
}
catch {
    Write-Host $_.Exception.Message -BackgroundColor Red
}


function Main {

    begin {
        <#
            .NOTES
             Get-FirmwareCatalog
        #>
        $catalog = Get-DevicesCatalog -Links $JsonSettings.Catalog
        Write-Host "Total: $($catalog.Count) models" -ForegroundColor DarkMagenta       
    }
    process {
        <#
            .NOTES
            Select device model in GridView and create new object Device.
        #>
        $Script:SelectedItemCatalog = $catalog | Out-GridView -OutputMode Single -Title "Select item"
        if ($null -eq $SelectedItemCatalog) { break }

        

    }
    end {
        <#
            .NOTES
            Check in $JsonDeviceModels if the selected model already not exists, and add.
            And convert to DeviceModels.json
        #>
        
    }
}
Main
