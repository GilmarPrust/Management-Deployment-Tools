<#
+----------------------------------------------------------------------------------------+
    .Description
    Add-Firmware
	Created by: Gilmar Prust
	Filename:   Add-Firmware.ps1
    Get-CimInstance -ClassName CIM_ComputerSystem | Select-Object -Property Manufacturer, Model, SystemSKUNumber
+----------------------------------------------------------------------------------------+
#>

<#
    .DESCRIPTION
    Entidade principal: "Modelo de Dispositivo"
#>
class DeviceModel {

    [string]$Guid = (New-Guid).Guid
    [string]$Manufacturer
    [ValidatePattern("^[a-zA-Z]+$")]
    [string]$Model
    [string]$Type

    DeviceModel() {}
    
    DeviceModel([String]$Manufacturer, [String]$Model, [string]$Types) {
        $this.Manufacturer = $Manufacturer
        $this.Model        = $Model
        $this.Types        = $Types
    }
}

class DeviceModelList {
    static [System.Collections.Generic.List[DeviceModel]] $DeviceModels
    static [void] Initialize() { [DeviceModelList]::Initialize($false) }
    static [bool] Initialize([bool]$force) {
        if ([DeviceModelList]::Books.Count -gt 0 -and -not $force) {
            return $false
        }
        [DeviceModelList]::DeviceModels = [System.Collections.Generic.List[DeviceModel]]::new()
        return $true
    }
    static [void] Validate([DeviceModel]$DeviceModel) {
        if ($null -eq $DeviceModel) { 
            throw "was null" 
        }
        if ([string]::IsNullOrEmpty($DeviceModel.Manufacturer)) {
            throw "Manufacturer wasn't defined."
        }
        if ([string]::IsNullOrEmpty($DeviceModel.Model)) {
            throw "Model wasn't defined."
        }
        if ([string[]]::IsNullOrEmpty -eq $DeviceModel.Types) {
            throw "Types wasn't defined."
        }
    }
    
    static [void] Add([DeviceModel]$DeviceModel) {
        [DeviceModelList]::Initialize()
        [DeviceModelList]::Validate($DeviceModel)
        if ([DeviceModelList]::Books.Contains($DeviceModel)) {
            throw "Book '$DeviceModel' already in list"
        }

        $FindPredicate = {
            param([DeviceModel]$b)
                $b.Manufactures -eq $DeviceModel.Manufacturer -and
                $b.Model -eq $DeviceModel.Model -and
                $b.Types -eq $DeviceModel.Types
        }.GetNewClosure()

        if ([DeviceModelList]::Books.Find($FindPredicate)) {
            throw "Device '$DeviceModel' already in list"
        }

        [DeviceModelList]::DeviceModel.Add($DeviceModel)
    }
    static [DeviceModel] Find([scriptblock]$Predicate) {
        [DeviceModelList]::Initialize()
        return [DeviceModelList]::DeviceModel.Find($Predicate)
    }
    static [DeviceModel[]] FindAll([scriptblock]$Predicate) {
        [DeviceModelList]::Initialize()
        return [DeviceModelList]::DeviceModel.FindAll($Predicate)
    }
    [void] Add() {
        Write-Host "Author wasn't defined"
    }
    [void] Save() {
        Write-Host "write in json file."
    }
}

class Device : DeviceModel {
    hidden [string]$Guid = (New-Guid).Guid
    [string]$SerialNumber
    [string]$HashHardware

    Device([String]$SerialNumber, [String]$HashHardware) {
        $this.SerialNumber = $SerialNumber
        $this.Model = $HashHardware
    }
    Device() {}     # default constructor
}

function New-DeviceModel {
    <#
        .DESCRIPTION
        cria objeto, e salva no json DeviceModel.json
    #>
    param (
        [string]$Manufacturer,
        [string]$Model,
        [string[]]$Types = @()
    )
    begin {
        $deviceModel = [DeviceModel]::new()
        $deviceModel.Add($Manufacturer, $Model, $Types)
        $deviceModel.Save()

    }
    process {
    }
    end {
        return $deviceModel
    }
}
function New-Device {
    [CmdletBinding()]
    param (
        
    )
    begin {
    }
    process {
    }
    end {
    }
}
function Get-AllDevices {
    [CmdletBinding()]
    param (
        
    )
    begin {
    }
    process {
    }
    end {
    }
}
function Get-AllDeviceModels {
    [CmdletBinding()]
    param (
        
    )
    begin {
    }
    process {
    }
    end {
    }
}

Export-ModuleMember -Function New-DeviceModel