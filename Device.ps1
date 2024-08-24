<#
+----------------------------------------------------------------------------------------+
    .Description
    Add-Firmware
	Created by: Gilmar Prust
	Filename:   Add-Firmware.ps1
+----------------------------------------------------------------------------------------+
#>
#using module .\Device.psm1

New-Variable -Name DeployRoot -Value "$($PSScriptRoot)" -Scope Global -Force -Option ReadOnly
Set-Location $DeployRoot -Verbose

#IMPORT MODULES
Import-Module .\Device.psm1 -Force -ErrorAction Stop -Verbose -PassThru

$new = New-DeviceModel("Dell", "Latitude 5420", @("D3RT", "AB12"), "")
$new