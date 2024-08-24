<#
+----------------------------------------------------------------------------------------+
    .Description
        Catalog
        Filename: Catalog.ps1
        Created by: Gilmar Prust
    
    .DESCRIPTION
        Obtem os catalogos, e retorna um PSCustonObject.

    .OUTPUTS
        Returns ArrayList of Devices @{ Manufacturer=""; Model=""; Types="" }
+----------------------------------------------------------------------------------------+
#>
using module .\Modules\Classes\Device\Device.psm1

#Define Global Variables.
New-Variable -Name DeployRoot -Value "$($PSScriptRoot)" -Scope Global -Force -Option ReadOnly
Set-Location $DeployRoot -Verbose

#IMPORT MODULES
Import-Module .\Modules\Download -Force -Global -ErrorAction Stop -Verbose
Import-Module .\Modules\DeviceCatalog -Force -Global -ErrorAction Stop -Verbose

function Main {
   
    begin {
        ###
        ###
        ###
        Write-Host "Getting all devices catalog... " -ForegroundColor Magenta -NoNewline
        $JsonSettings = Get-Content "$($DeployRoot)\Control\Settings.json" -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
        [DeviceCatalog]::Initialize()
    }
    process {
        ###
        ###
        ###
        foreach ($item in $JsonSettings.Catalog.Devices) {

            $devicesCatalog = [PScustomobject](Get-XmlContent -Url $item.Link | Get-DeviceCatalog -Manufacturer $item.Manufacturer)
            
            $devicesCatalog | ForEach-Object {

                [DeviceCatalog]::Add( 
                    [Device]::new( 
                        $_.Manufacturer, 
                        $_.Model, 
                        $_.Types, 
                        $_.Version, 
                        $_.Link, 
                        $_.Hash)
                ) | Out-Null
            }
        }
    }
    end {
        ###
        ###
        ###
        $catalog = [DeviceCatalog]::GetAll()
        Write-Host "Done. $($catalog.Count) Devices" -ForegroundColor Green
        return $catalog
        <#
            IsPublic IsSerial Name     BaseType
            -------- -------- -------- --------
            True     True     Device[] System.Array
        #>
    }
}
Main | Out-GridView
