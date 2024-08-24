<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    DriverPack
    Created by: @GilmarPrust
    Filename:   DriverPack.psm1
+----------------------------------------------------------------------------------------+
#>

class DriverPack {

    [string]$Manufacturer
    [string]$Model
    [string]$Types
    [string]$OS
    [string]$Version
    [string]$Link
    [string]$Hash
    
    DriverPack([String]$Manufacturer, [String]$Model, [string]$Types, [string]$OS, [String]$Version, [string]$Link, [string]$Hash) {
        $this.Manufacturer = $Manufacturer
        $this.Model        = $Model
        $this.Types        = $Types
        $this.OS           = $OS
        $this.Version      = $Version
        $this.Link         = $Link
        $this.Hash         = $Hash
    }
}

class DriverPackCatalog {

    static [System.Collections.Generic.List[DriverPack]] $DriverPack
    

    ### Método estático para inicializar a lista de Devices.
    static [void] Initialize() {
        
        [DriverPackCatalog]::DriverPack = [System.Collections.Generic.List[DriverPack]]::new()
    }

    ### Método estático para obter todos os Devices.
    static [DriverPack[]] GetAll() {
        return [DriverPackCatalog]::DriverPack
    }

    ### Método estático para adicionar um Device na Lista.
    static [void] Add([DriverPack]$DriverPack) {
        
        [DriverPackCatalog]::DriverPack.Add($DriverPack)
    } 
}