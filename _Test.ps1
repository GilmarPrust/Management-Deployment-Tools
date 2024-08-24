<#
+----------------------------------------------------------------------------------------+
    .Description
    Add-Firmware
	Created by: Gilmar Prust
	Filename:   Add-Firmware.ps1
+----------------------------------------------------------------------------------------+
#>


New-Variable -Name DeployRoot -Value "$($PSScriptRoot)" -Scope Global -Force -Option ReadOnly
Set-Location $DeployRoot -Verbose



$New = [PScustomobject]@{ 
    Guid = "rererer";
    Manufacturer="rerer";
    Model="erererer";
    Types="fghjkl";
}

$tipo1 = New-Object System.Collections.ArrayList
$tipo1.GetType()

[void]$tipo1.Add($New)

$tipo2 = @()
$tipo2.GetType()


$tipo3 = Get-Content "C:\Users\e-gilmarp\OneDrive - WEG EQUIPAMENTOS ELETRICOS S.A\Documents\Projetos\Management-Deployment-Tools\Control\DeviceModels.json" | ConvertFrom-Json
$tipo3.GetType()


[PsObject[]]tipo4 = @()
$tipo4.GetType()






#$JsonFirmwares += $New

#$JsonFirmwares | ConvertTo-Json -ErrorAction Stop | Set-Content "$($DeployRoot)\Control\Firmwares.json"

New-Item -Path "D:" -Name "Firmwares.json" -ItemType File -Value "[]" -Force

$jsonfile = Get-Content "D:\Firmwares.json" | ConvertFrom-Json

#################

function Get-JsonFirmwares {
    <#
        .OUTPUTS
    #>

    $jsonFile = "D:\Firmwares.json"
    
    if (!(Test-Path -Path $jsonFile)) {
        New-Item -Path $jsonFile -ItemType File -Value "[]"
    }
    return Get-Content $jsonFile -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
}
$New = [PScustomobject]@{ 
    Guid = "rererer";
    Manufacturer="rerer";
    Model="erererer";
    Types="fghjkl";
}

$temp6 = Get-JsonFirmwares

$temp6 += $New
$temp6.GetType()

$temp6



###############

$jsonBase = @{}
$jsonBase | ConvertTo-Json -AsArray | Out-File "Firmwares.json"

$json = Get-Content "D:\DeviceModels.json" | ConvertFrom-Json

$json += $New
$json | ConvertTo-Json -AsArray | Set-Content "D:\DeviceModels.json"





$New = [PScustomobject]@{ 
    Guid = "rererer";
    Manufacturer="rerer";
    Model="erererer";
    Types="fghjkl";
}

[PsObject[]]$jsonfids = Get-Content "D:\DeviceModels.json" | ConvertFrom-Json

$jsonfids.GetType()
$jsonfids += $New

$jsonfids | ConvertTo-Json -AsArray | Set-Content "D:\DeviceModels.json"


#########################################
#https://learn.microsoft.com/pt-br/powershell/module/microsoft.powershell.core/about/about_classes?view=powershell-7.4

$New = [PScustomobject]@{ 
    Guid = "rererer";
    Manufacturer="rerer";
    Model="erererer";
    Types="fghjkl";
}

enum Type {
    Desktop
    Latitude
}

class Device2 {

 hidden [string]$Guid = (New-Guid).Guid
        [string]$id
        [Type]$Type
 static [string[]] $Projects = @()
        [Int32]TestReturn (){
            return 10
        }
        [void]Grow() {
            $this.id += 1;
            $this.model = "latitude"
        }
}

#$dev = New-Object -TypeName Device
$dev = [Device]::new()
$dev.id = 1
$dev.Type = 'Desktop'
$dev.TestReturn()


#############################

class Device {
    [string]$Guid = (New-Guid).Guid
    [string]$Manufacturer
    [ValidatePattern("^[a-zA-Z]+$")]
    [string]$Model

    #Default constructor    
    Device(){}

    #Common constructor for title and author
    Device([String]$Manufacturer, [String]$Model) {
        $this.Manufacturer = $Manufacturer
        $this.Model        = $Model
    }
    
}


$device = [Device]::new("Dell", "latitude")
#$device.Guid = "df
$device
