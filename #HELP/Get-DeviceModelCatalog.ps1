<#
+-------------------------------------------------------------------------------------------------------------+
    .NOTES
        Get-DeviceModelCatalog
        Filename: Get-DeviceModelCatalog.ps1
        Created by: Gilmar Prust
    
    .DESCRIPTION
        Obtem os catalogos, e retorna um PSCustonObject.

    .OUTPUTS
        Returns ArrayList of Devices @{ Manufacturer=""; Model=""; Types=""; Version=""; Link=""; Hash="" }
+-------------------------------------------------------------------------------------------------------------+
#>

#GLOBAL VARIABLES
New-Variable -Name DeployRoot -Value "$($PSScriptRoot)" -Scope Global -Force -Option ReadOnly
Set-Location $DeployRoot -Verbose

#IMPORT MODULES
Import-Module .\Modules\DeviceModelCatalog -Force -ErrorAction Stop -Verbose

function Main {
   
    begin {
        ###
        Write-Host "Getting device model catalog..." -ForegroundColor Magenta
    }
    process {
        ###
        $catalog = Get-DeviceModelCatalog

    }
    end {
        ###
        Write-Host "Done. $($catalog.Count) Models." -ForegroundColor Green
        return $catalog
        <#
            IsPublic IsSerial Name     BaseType
            -------- -------- -------- --------
            True     True     Object[] System.Array
        #>
    }
}
Main