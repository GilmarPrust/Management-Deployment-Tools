<#
+----------------------------------------------------------------------------------------+
    .Description
    Firmware.psm1
	Created by: Gilmar Prust
	Filename:   Firmware.psm1
+----------------------------------------------------------------------------------------+
#>

function Get-JsonFirmwares {
    <#
        .OUTPUTS
    #>
    $jsonFile = "$($DeployRoot)\Control\Firmwares.json"
    
    if (!Test-Path -Path $jsonFile) {
        New-Item -Path "$($DeployRoot)\Control" -Name "Firmwares.json" -ItemType File -Value "[]"
    }
    return Get-Content $jsonFile -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
}
function Save-JsonFirmwares {
    <#
        .INPUTS
    #>
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory, ValueFromPipeline, ValueFromPipelineByPropertyName)]
        $Json
    )
    $Json | ConvertTo-Json -ErrorAction Stop | Set-Content "$($DeployRoot)\Control\Firmwares.json"
}

function Add-NewFirmware {
    <#
        .PARAMETER Firmware
        Tabela Hash

        .DESCRIPTION
        Adiciona novo item em Firmwares.json

        .EXAMPLE
        $Firmware = @{ 
            DeviceGuid = $Guid;
            FileName   = "OptiPlex_3040_1.20.1.exe"; 
            Version    = "1.20.1"
            Source     = "\\Firmware\\OptiPlex_3040_1.20.1.exe"; 
            Hash       = "34b073c2bf0fba12d6569d7759c7ec8d";
            Enables    = $true
        }
    #>
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory)]
        $Firmware
    )

    begin {
        $script:JsonFirmwares = Get-JsonFirmwares
        $JsonFirmwares = Get-Content "$($DeployRoot)\Control\Firmwares.json" -ErrorAction Stop | ConvertFrom-Json -ErrorAction Stop
    }
    process {
        
        $script:JsonFirmwares += [ordered]@{ 

            DeviceGuid = "$($Firmware.DeviceGuid)";
            FileName   = "$($Firmware.FileName)";
            Version    = "$($Firmware.Version)";
            Source     = "$($Firmware.Source)";
            Hash       = "$($Firmware.Hash)";
            Enabled    = $true
        }
    }
    end {
        $script:JsonFirmwares
        $JsonFirmwares | ConvertTo-Json -ErrorAction Stop | Set-Content "$($DeployRoot)\Control\Firmwares.json"
        #$script:JsonFirmwares | Save-JsonFirmwares
    }
}

function Update-Firmware {
    <#
        .PARAMETER Firmware
        Tabela Hash

        .DESCRIPTION
        Adiciona um item em Firmwares.json
        
        .EXAMPLE
        $Firmware = @{ 
            DeviceGuid = $Guid;
            FileName   = "OptiPlex_3040_1.20.1.exe"; 
            Version    = "1.20.1"
            Source     = "\\Firmware\\OptiPlex_3040_1.20.1.exe"; 
            Hash       = "34b073c2bf0fba12d6569d7759c7ec8d";
            Enables    = true
        }
    #>
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory, ValueFromPipeline, ValueFromPipelineByPropertyName)]
        [String]$Firmware
    )

    begin {
        $JsonFirmwares = Get-JsonFirmwares
    }
    process {

        $JsonFirmwares | ForEach-Object {
            
            if ($_.DeviceGuid -eq $Firmware) {

                $_.FileName = "$($Firmware.FileName)";
                $_.Version  = "$($Firmware.Version)";
                $_.Source   = "$($Firmware.Source)";
                $_.Hash     = "$($Firmware.Hash)"
            }
        }
    }
    end {
        $JsonFirmwares | Save-JsonFirmwares
    }
}



function Add-Firmware {
    <#
        .PARAMETER DeviceModel
        Especifique o modelo de dispositivo obtido de DeviceModels.json.

        .PARAMETER ItemCatalog
        Especifique o item do catalogo obtido de Get-FirmwareCatalog.

        .DESCRIPTION
        Adiciona um item em Firmware.json.
        Este item precisa estar vinculado a um dispositivo de DeviceModels.json.
        
        .NOTES
        > Load Firmwares.json
        > Download file | Check File Hash
        > Remove se hash não for igual.
        > Remove arquivo de versão anterior.
        > Adiciona novo item em Firmwares.json.
        > Salva Firmwares.json

        .EXAMPLE
        DeviceModel = @{ 
                Guid         = (New-Guid).Guid;
                Manufacturer = "Dell Inc."; 
                Model        = "OptiPlex 5000"; 
                Types        = @("0AC3","0AC4")
        }
        .EXAMPLE
        ItemCatalog = @{ 
                Guid     = (New-Guid).Guid;
                FileName = "OptiPlex_3040_1.20.1.exe"; 
                Version  = "1.20.1"
                Source   = "\\Firmware\\OptiPlex_3040_1.20.1.exe"; 
                Hash     = "34b073c2bf0fba12d6569d7759c7ec8d"
        }
    #>
    [CmdletBinding()]
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory)]
        $Device,
        $ItemCatalog
    )
    
    begin {

        $JsonFirmwares = Get-JsomFirmwares
        $filename = Split-Path -Path $ItemCatalog.Link -Leaf
        $filepath = Join-Path -Path "$($DeployRoot)\Firmwares" -ChildPath $filename
    }
    process {

        # Download Firmware.
        $checkfilehash = Download -Url $ItemCatalog.Link -Destination $filepath -Hash $ItemCatalog.Hash -ErrorAction SilentlyContinue
    
        # Check the file hash.
        if ($false -eq $checkfilehash) {
            Write-host "Error downloading file or different hash." -ForegroundColor Red
            Remove-Item -Path $filepath -Force -ErrorAction SilentlyContinue -Verbose
            continue
        }

        #Remove old file.
        $oldfile = ($JsonFirmwares | Where-Object { $_.DeviceGuid -eq $DeviceModel.Guid }).Source
        if ($null -ne $oldfile) {
            #Remove-Item -Path "$($DeployRoot)$($oldfile)" -Force -ErrorAction SilentlyContinue -Verbose
        }
        

        if ($JsonFirmwares.DeviceGuid -contains $DeviceModel.Guid) {

            $JsonFirmwares = Update-ItemFirmware -Guid $DeviceModel.Guid -JsonFirmware $JsonFirmwares -ItemCatalog $ItemCatalog

        } else {

            $JsonFirmwares = Add-ItemFirmware -Guid $DeviceModel.Guid -JsonFirmware $JsonFirmwares -ItemCatalog $ItemCatalog
        }
    }
    end {
        #Update JsonFirmware.
        Write-Host "Updated firmware for $($DeviceModel.Model)." -ForegroundColor Green
        $JsonFirmwares | ConvertTo-Json | Set-Content "$($DeployRoot)\Control\Firmwares.json"
    }
}

function DownloadFirmware {
    <#
        .PARAMETER DeviceModel
        Especifique o modelo de dispositivo obtido de DeviceModels.json.

        .PARAMETER Destination
        Especifique o item do catalogo obtido de Get-FirmwareCatalog.

        .DESCRIPTION
        Adiciona um item em Firmware.json.
    #>
    [CmdletBinding()]
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory)]
        $ItemCatalog,
        $Destination
    )

    begin {

        $checkfilehash = Download -Url $ItemCatalog.Link -Destination $Destination -Hash $ItemCatalog.Hash -ErrorAction SilentlyContinue
    
        # Check the file hash.
        if ($false -eq $checkfilehash) {
            Write-host "Error downloading file or different hash." -ForegroundColor Red
            Remove-Item -Path $filepath -Force -ErrorAction SilentlyContinue -Verbose
            continue
        }
    }
    process {
        
    }
    end {
        
    }
}

function Get-Firmware {
    [CmdletBinding()]
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory)]
        [String]$Guid,
        [Object[]]$JsonFirmware,
        [Object[]]$ItemCatalog
    )
    
}

function Add-ItemFirmware {
    <#
        .PARAMETER DeviceModel
        Especifique o modelo de dispositivo obtido de DeviceModels.json.

        .PARAMETER ItemCatalog
        Especifique o item do catalogo obtido de Get-FirmwareCatalog.

        .DESCRIPTION
        Adiciona um item em Firmware.json.
        Este item precisa estar vinculado a um dispositivo de DeviceModels.json.
        
        .EXAMPLE
        ItemCatalog = @{ 
                Guid     = (New-Guid).Guid;
                FileName = "OptiPlex_3040_1.20.1.exe"; 
                Version  = "1.20.1"
                Source   = "\\Firmware\\OptiPlex_3040_1.20.1.exe"; 
                Hash     = "34b073c2bf0fba12d6569d7759c7ec8d"
        }
    #>
    [CmdletBinding()]
    param (
        [ValidateNotNullOrEmpty()]
        [Parameter(Mandatory)]
        [String]$Guid,
        [Object[]]$ItemCatalog
    )
    begin {

        $filename = Split-Path -Path $ItemCatalog.Link -Leaf
    }
    process {

        #Add new firmware in Firmwares.json
        $NewItem = [ordered]@{ 

            DeviceGuid = $Guid; 
            FileName   = $filename;
            Version    = $ItemCatalog.Version; 
            Source     = "\Firmwares\$($filename)"; 
            Hash       = $ItemCatalog.Hash;
            Enabled    = $true;
        }
    }
    end {
        #Update JsonFirmware.
        return $NewItem
    }
}




# SIG # Begin signature block
# MIIFlAYJKoZIhvcNAQcCoIIFhTCCBYECAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUYevSz1LXrWMEGmzNepreWUdB
# 5XCgggMiMIIDHjCCAgagAwIBAgIQONirzSpNl5FNggQlpn5yyDANBgkqhkiG9w0B
# AQsFADAnMSUwIwYDVQQDDBxQb3dlclNoZWxsIENvZGUgU2lnbmluZyBDZXJ0MB4X
# DTIzMDcxNDE1NTUxN1oXDTI0MDcxNDE2MTUxN1owJzElMCMGA1UEAwwcUG93ZXJT
# aGVsbCBDb2RlIFNpZ25pbmcgQ2VydDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCC
# AQoCggEBAL/rqq9BFlGt8bVmQ6EqY43B8W3d2b82C/QN8O37QTChQKUPws+osX1f
# tj9MPdL93WC/XEPqvUiwqPtvBS63JinOJGATsxXh/+fcHCbvJnCestdrZ36VnJJ+
# rvslUBvorv5ynfpBSZRbCuA9aK06rBXksGTbu1wLdGc3oezK7iKymqYwIilFxqz8
# FnliKSbiUWhQmpGxx9RTjZ4YP6YItETeeDMRFGBVp6wlW+qXt6fV7VfqwRkpFCNJ
# YBO9j1Cpz/WcIjoXIeorb9iY5UEsNlLzIcJV0eqJlsvvqqyK07ITKug1OK/OXsYk
# ziA9WWLmpsvv0jRQYHFwXpcx2pwmr7UCAwEAAaNGMEQwDgYDVR0PAQH/BAQDAgeA
# MBMGA1UdJQQMMAoGCCsGAQUFBwMDMB0GA1UdDgQWBBQ3wm3NMuMjb8eeEE5cDN2+
# 4M0FOzANBgkqhkiG9w0BAQsFAAOCAQEAEQsmQjGeMB04f0awcyggFRT988/9EN80
# bzdIWYdJFKJ3Nu2N/vKZOaSBF7AAMrNpYZGFVUgZ8wkfobw12RnH+Lz9cyt+rCSS
# TBfMyIZfoVT9sAu7NQV5cufqH82ObysqoZa3PbniXvDE+PaP1ceWje1ONmFdtr8X
# Ffkv0QFmKeH2x4cpzrJBBD58GI3XssUD2xxtHZntC98VOy0Vk4eQQZCXr42m2C3Q
# lMZby+vvm179rkn8icFkDMlQ81Im6DBSSKDP5AqWGfI5d+54ACFayHiztSS0lyl6
# OBo8+fK5kJXoTqSR2F7oh4hvFvfaBanGWmYHwcXo1yKRX2jD80/RgDGCAdwwggHY
# AgEBMDswJzElMCMGA1UEAwwcUG93ZXJTaGVsbCBDb2RlIFNpZ25pbmcgQ2VydAIQ
# ONirzSpNl5FNggQlpn5yyDAJBgUrDgMCGgUAoHgwGAYKKwYBBAGCNwIBDDEKMAig
# AoAAoQKAADAZBgkqhkiG9w0BCQMxDAYKKwYBBAGCNwIBBDAcBgorBgEEAYI3AgEL
# MQ4wDAYKKwYBBAGCNwIBFTAjBgkqhkiG9w0BCQQxFgQUoCKK/wgGQkU0G6YIqMv4
# kx09lmcwDQYJKoZIhvcNAQEBBQAEggEAHhFmSqNnGiUokoAqG+89HfYszpEI2c9i
# wT+uhN6vmVX953wg37r7MnGL3Dr5sTGwkjzYsCOSfYNzYP6cqWSeUzE3CyUGinr5
# 9zapN3dVvsaKyjzaz9yt4c7/loXygBm0Rx0ZPFoPxA3lIMPpHVXMVg7BZ0piyVzV
# PbYVw9leHo8xDcDnmmVfuLWNQNlzLxCbrIH5Hm3G3Lv/X8wf2b4/5iAJBXdw9WbO
# 30BsRLlBoWoPSxUto+fZy1I7Ja5oA2Pf27MGLcN37usO/qMly4ozwLm0yZ6Za7gl
# 8t2haaRV7gs2JVSWMDBvm4WyTUwmhybgowFQofaXiw402/E0ajQZyQ==
# SIG # End signature block