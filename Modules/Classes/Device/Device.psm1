<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    DevicesCatalog
    Created by: @GilmarPrust
    Filename:   DevicesCatalog.psm1
    Get-DevicesCatalog
+----------------------------------------------------------------------------------------+
#>

class Device {

    [string]$Manufacturer
    [string]$Model
    [string[]]$Types = @()
    [string]$Version
    [string]$Link
    [string]$Hash
    
    Device([String]$Manufacturer, [String]$Model, [string[]]$Types, [String]$Version, [string]$Link, [string]$Hash) {
        $this.Manufacturer = $Manufacturer
        $this.Model        = $Model
        $this.Types        = $Types
        $this.Version      = $Version
        $this.Link         = $Link
        $this.Hash         = $Hash
    }
}

class DeviceCatalog {

    static [System.Collections.Generic.List[Device]] $Devices

    ### Método estático para inicializar a lista de Devices.
    static [void] Initialize() {
        
        [DeviceCatalog]::Devices = [System.Collections.Generic.List[Device]]::new()
    }

    ### Método estático para obter todos os Devices.
    static [Device[]] GetAll() {
        return [DeviceCatalog]::Devices
    }

    ### Método estático para adicionar um Device na Lista.
    static [void] Add([Device]$Device) {
        
        [DeviceCatalog]::Devices.Add($Device)
    } 
}