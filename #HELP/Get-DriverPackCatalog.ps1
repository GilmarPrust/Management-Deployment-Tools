<#
+-------------------------------------------------------------------------------------------------------------+
    .NOTES
        Get-DriverPackCatalog
        Filename: Get-DriverPackCatalog.ps1
        Created by: Gilmar Prust
    
    .DESCRIPTION
        Obtem os catalogos, e retorna um PSCustonObject.

    .OUTPUTS
        Returns ArrayList of DriverPack @{ Model=""; Types=@(); OS=""; Version=""; Link=""; Hash="" }
+-------------------------------------------------------------------------------------------------------------+
#>

#GLOBAL VARIABLES
New-Variable -Name DeployRoot -Value "$($PSScriptRoot)" -Scope Global -Force -Option ReadOnly
Set-Location $DeployRoot -Verbose

#IMPORT MODULES
Import-Module .\Modules\DriverPackCatalog -Force -ErrorAction Stop -Verbose

function Main {
   
    begin {
        ###
        Write-Host "Getting driverpack catalog..." -ForegroundColor Magenta
    }
    process {
        ###
        $catalog = Get-DriverPackCatalog
    }
    end {
        ###
        Write-Host "Done. $($catalog.Count) packs." -ForegroundColor Green
        return $catalog
        <#
            IsPublic IsSerial Name     BaseType
            -------- -------- -------- --------
            True     True     Object[] System.Array
        #>
    }
}
Main