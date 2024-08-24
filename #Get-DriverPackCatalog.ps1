<#
+----------------------------------------------------------------------------------------+
    .Description
        Get-DriverPackCatalog
        Filename: Get-DriverPackCatalog.ps1
        Created by: Gilmar Prust
    
    .DESCRIPTION
        Obtem os catalogos, e retorna um PSCustonObject.

    .OUTPUTS
        Returns ArrayList of Devices @{ Manufacturer=""; Model=""; Types="" }
+----------------------------------------------------------------------------------------+
#>
using module .\Modules\Classes\DriverPack\DriverPack.psm1

#Define Global Variables.
New-Variable -Name DeployRoot -Value "$($PSScriptRoot)" -Scope Global -Force -Option ReadOnly
Set-Location $DeployRoot -Verbose

#IMPORT MODULES
Import-Module .\Modules\Download -Force -Global -ErrorAction Stop -Verbose
Import-Module .\Modules\DriverPackCatalog -Force -Global -ErrorAction Stop -Verbose



function Main {
   
    begin {
        ###
        ###
        ###
        Write-Host "Getting all devices catalog... " -ForegroundColor Magenta -NoNewline
        $JsonSettings = Get-Content "$($DeployRoot)\Control\Settings.json" -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
        [DriverPackCatalog]::Initialize()
    }
    process {
        ###
        ###
        ###
        foreach ($item in $JsonSettings.Catalog.DriverPacks) {

            $driverpackCatalog = [PScustomobject](Get-XmlContent -Url $item.Link | Get-DriverPackCatalog -Manufacturer $item.Manufacturer)

            $driverpackCatalog | ForEach-Object {

                $newdriverpack = [DriverPack]::new( 
                        $_.Manufacturer, 
                        $_.Model, 
                        $_.Types, 
                        $_.OS, 
                        $_.Version, 
                        $_.Link, 
                        $_.Hash)

                [DriverPackCatalog]::Add( $newdriverpack )
            }
        }
    }
    end {
        ###
        ###
        ###
        $catalog = [DriverPackCatalog]::GetAll()
        Write-Host "Done. $($catalog.Count) Devices" -ForegroundColor Green
        return $catalog
        <#
            IsPublic IsSerial Name         BaseType
            -------- -------- ------------ --------
            True     True     DriverPack[] System.Array
        #>
    }
}
Main | Out-GridView
