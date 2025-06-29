<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    Deployment Module
	Created by: Gilmar Prust
	Filename:   Deployment.psm1
+----------------------------------------------------------------------------------------+
#>
function Get-HardwareInfo {
    <#
        .DESCRIPTION
    #>
    ### INSTALL MODULE
    Install-PSResource Hardware -Repository DCM2_PSResources
    
    $hardwareinfo = Get-Hardware
    
    if ($null -eq $hardwareinfo) { Throw "Hardware info is null." }
    
    if ($null -eq $hardwareinfo.Network.MACAddress -or $hardwareinfo.Network.IPAddress -like "169.*" -or $null -eq $hardwareinfo.Network.IPAddress) {
        Write-Host "Network info not found." -ForegroundColor Red
    }
    <#
        Check network state before continue.
    #>

    return $hardwareinfo
}
function Get-DeviceModelInfo {
    <#
        .DESCRIPTION
    #>
    ### INSTALL RESOURCE
    Install-PSResource DeviceModels -Repository DCM2_PSResources
    
    $hardware = $global:deployInfo.Hardware
 
    $devicemodelinfo = Get-DeviceModel -Manufacturer $hardware.Manufacturer -Model $hardware.Model -Type $hardware.Type
    
    if ($null -eq $devicemodelinfo) { 
        $devicemodelinfo = Add-DeviceModel -Manufacturer $hardware.Manufacturer -Model $hardware.Model -Type $hardware.Type
        Write-Host "Add new Device Model." -ForegroundColor DarkRed
    }
    if ($null -eq $devicemodelinfo) { Throw "DeviceModel info is null." }

    return $devicemodelinfo
}
function Get-FirmwareInfo {
    <#
        .DESCRIPTION
    #>
    ### INSTALL RESOURCE
    Install-PSResource Firmware -Repository DCM2_PSResources

    $firmware = Get-Firmware -DeviceModelGuid $global:deployInfo.DeviceModel.Guid

    if ($null -eq $firmware) { Write-Host "Firmware not found." -ForegroundColor DarkYellow }

    return $firmware
}
function Get-DriverPackInfo {
    <#
        .DESCRIPTION
    #>
    ### INSTALL RESOURCE
    Install-PSResource DriverPack -Repository DCM2_PSResources
    
    $driverpack = Get-DriverPack -DeviceModelGuid $global:deployInfo.DeviceModel.Guid

    if ($null -eq $driverpack) { Write-Host "DriverPack not found." -ForegroundColor DarkYellow }

    return $driverpack
}
function Get-DeviceInfo {
    <#
        .DESCRIPTION
    #>
    ### INSTALL RESOURCE
    Install-PSResource Device -Repository DCM2_PSResources

    $hardware = $global:deployInfo.Hardware

    $device = Get-Device -SerialNumber $hardware.SerialNumber -MacAddress $hardware.Network.MacAddress

    if ($null -eq $device) { Throw "Device not found." }

    return $device 
}
function Get-DeviceProfileInfo {
    <#
        .DESCRIPTION
    #>
    ### INSTALL RESOURCE
    Install-PSResource Associations -Repository DCM2_PSResources

    $deviceprofile = Get-DeviceProfile -DeviceGuid $global:deployInfo.Device.Guid

    if ($null -eq $deviceprofile) { Throw "DeviceProfile not found." }
    
    return $deviceprofile
}
function Get-DeviceIDOfficeInfo {
    <#
        .DESCRIPTION
    #>
    ### INSTALL RESOURCE
    Install-PSResource Office365 -Repository DCM2_PSResources

    $deviceoffice = Get-DeviceIDOffice -DeviceGuid $global:deployInfo.Device.Guid

    if ($null -eq $deviceoffice) { Write-Host "DeviceIDOffice not found." -ForegroundColor DarkRed }

    return $deviceoffice
}
function Get-ApplicationsInfo {
    <#
        .DESCRIPTION
    #>
    ### INSTALL RESOURCE
    Install-PSResource Application -Repository DCM2_PSResources
    Install-PSResource Associations -Repository DCM2_PSResources

    ### Get apps from Device, DeviceModel, Profile.
    $applications = @($global:deployInfo.Profile.Applications)

    Get-DeviceApplications -DeviceGuid $global:deployInfo.Device.Guid | ForEach-Object {
        ###
        if ($applications -notcontains $_ ) { $applications += $_ }
    }

    Get-DeviceModelApplications -DeviceModelGuid $global:deployInfo.DeviceModel.Guid | ForEach-Object {
        ###
        if ($applications -notcontains $_ ) { $applications += $_ }
    }
    
    if ($null -eq $applications) { Write-Host "Applications is null." -ForegroundColor DarkRed }

    return $applications
}
function Get-ImageInfo {
    <#
        .DESCRIPTION
    #>
    ### INSTALL RESOURCE
    Install-PSResource Image -Repository DCM2_PSResources
    
    $image = Get-Image -Guid $global:deployInfo.Profile.ImageGuid

    if ($null -eq $image) { Throw "Image not found." }
    
    return $image
}
function Get-DiskInfo {
    <#
        .DESCRIPTION
    #>
    ### INSTALL RESOURCE
    Install-PSResource Disk -Scope AllUsers -Repository DCM2_PSResources

    $disk = $null
    ### if not running WinPE select manual disk.
    if (Test-Path -Path "HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\WinPE") {
        $disk = Get-DeployDisk -Auto
    } else {
        $disk = Get-DeployDisk | Out-GridView -OutputMode Single -Title "Select disk"
    }
    ### Check disk.
    if ($null -eq $disk) { Throw "Disk is null." }

    return $disk 
}
function Get-FirmwareTypeInfo {
    <#
        .DESCRIPTION
    #>
    ###
    return (Get-Content "$($DCM2Root)\Settings.json" | ConvertFrom-Json).FirmwareType
}
function Get-DeployInfo {
    <#
        .DESCRIPTION
    #>
    $global:deployInfo = New-Object -TypeName PSObject

    $global:deployInfo | Add-Member -MemberType NoteProperty -Name Hardware -Value (Get-HardwareInfo)
    Write-Host "Hardware: $($global:deployInfo.Hardware)"
    
    $global:deployInfo | Add-Member -MemberType NoteProperty -Name DeviceModel -Value (Get-DeviceModelInfo)
    Write-Host "DeviceModel: $($global:deployInfo.DeviceModel)"
    
    $global:deployInfo | Add-Member -MemberType NoteProperty -Name Firmware -Value (Get-FirmwareInfo)
    Write-Host "Firmware: $($global:deployInfo.Firmware)"
    
    $global:deployInfo | Add-Member -MemberType NoteProperty -Name DriverPack -Value (Get-DriverPackInfo)
    Write-Host "DriverPack: $($global:deployInfo.DriverPack.Count)"
    
    $global:deployInfo | Add-Member -MemberType NoteProperty -Name Device -Value (Get-DeviceInfo)
    Write-Host "Device: $($global:deployInfo.Device)"
    
    $global:deployInfo | Add-Member -MemberType NoteProperty -Name Profile -Value (Get-DeviceProfileInfo)
    Write-Host "Profile: $($global:deployInfo.Profile)"

    $global:deployInfo | Add-Member -MemberType NoteProperty -Name PIDOffice -Value (Get-DeviceIDOfficeInfo)
    Write-Host "PIDOffice: $($global:deployInfo.PIDOffice)"

    $global:deployInfo | Add-Member -MemberType NoteProperty -Name Applications -Value (Get-ApplicationsInfo)
    Write-Host "Applications: $($global:deployInfo.Applications)"

    $global:deployInfo | Add-Member -MemberType NoteProperty -Name Image -Value (Get-ImageInfo)
    Write-Host "Image: $($global:deployInfo.Image)"

    $global:deployInfo | Add-Member -MemberType NoteProperty -Name FirmwareType -Value (Get-FirmwareTypeInfo)
    Write-Host "FirmwareType: $($global:deployInfo.FirmwareType)"

    $global:deployInfo | Add-Member -MemberType NoteProperty -Name Disk -Value (Get-DiskInfo)
    Write-Host "Disk: $($global:deployInfo.Disk)"

}


function Set-DeployWorkDir {
    <#
        .FUNCTIONALITY
        Set DeployWorkDir
    #>
    ### INSTALL RESOURCE

    switch ($global:DeployMode) {
        ###
        "Refresh" { 
            $global:drivesystem = "C:\"
        }

    }

    ### Set Deploy work dir.
    New-Variable -Name DeployWorkDir -Value "$($global:drivesystem)\Deploy" -Scope Global -Force

    ### Create Deploy work path.
    New-Item -Path DeployWorkDir -ItemType Directory -Force | Out-Null

    Write-Host "> Set DeployWorkDir=$($DeployWorkDir)" -ForegroundColor DarkMagenta

}
function Invoke-DeployDisk {
    <#
        .FUNCTIONALITY
        Set variables: "$global:driveboot:\" ; "$global:drivesystem:\" ; $DeployWorkDir
    #>
    ### INSTALL MODULE
    Install-PSResource Disk -Repository DCM2_PSResources

    Write-Host "> Deploy-Disk" -ForegroundColor Magenta

    switch ($global:deployInfo.FirmwareType) {
        "UEFI" { $local:deploydisk = Format-Disk -DiskNumber $global:deployInfo.Disk.Number -PartitionStyle GPT }
        "BIOS" { $local:deploydisk = Format-Disk -DiskNumber $global:deployInfo.Disk.Number -PartitionStyle MBR }
    }
    $global:driveboot = "$(($local:deploydisk | Where-Object { $_.FileSystemLabel -eq 'Boot' }).DriveLetter):"
    $global:drivesystem = "$(($local:deploydisk | Where-Object { $_.FileSystemLabel -eq 'System' }).DriveLetter):"
}
function Invoke-DeployImage {
    <#
        .FUNCTIONALITY
        Deploy Windows Image.
        Create BcdBoot store.
    #>
    ### INSTALL MODULE
    Install-PSResource Image -Repository DCM2_PSResources

    Write-Host "> Deploy-Image" -ForegroundColor DarkMagenta

    Invoke-ApplyImage -ImageFile "$($global:DeployRoot)$($global:deployInfo.Image.Source)" -Index $global:deployInfo.Image.ImageIndex -ApplyPath $global:drivesystem

    ### Crete BcdBoot Store.
    $local:result = New-BcdBootStore -DriveBoot $global:driveboot -DriveSystem $global:drivesystem
    #Validate
    if ($local:result -eq $true) {
        #Get-BcdEntry
        Write-Host "BCDBoot integro." -ForegroundColor Green
     }
}
function Invoke-DeployUnattend {
    <#
        .FUNCTIONALITY
    #>
    ### INSTALL RESOURCE
    Install-PSResource Unattend -Repository DCM2_PSResources

    Write-Host "> Deploy-Unattend" -ForegroundColor DarkMagenta

    Build-Unattend -ComputerName $global:deployInfo.Device.ComputerName -Manufacturer $global:deployInfo.DeviceModel.Manufacturer | ApplyUnattend -DriveSystem $global:drivesystem
}
function Invoke-DeployDrivers {
    <#
        .FUNCTIONALITY
        Extract driverpack to $DriveSystem\Drivers and apply on system.
    #>
    ### INSTALL RESOURCE
    Install-PSResource Drivers -Repository DCM2_PSResources

    Write-Host "> Deploy-Drivers" -ForegroundColor DarkMagenta

    $DriverPack = $global:deployInfo.DriverPack
    if ($null -eq $DriverPack) { continue }

    #$driveworkdir = "$($global:drivesystem)\Drivers"
    
    ### Win11 utiliza driver Win10; Win10 não utiliza driver Win11.
    if ($global:deployInfo.Image.ShortName -eq 'Win11') {
        if ($DriverPack.Count -gt 1) {
            $DriverPack = $DriverPack | Where-Object { $_.OS -eq 'Win11' }
        }
    } else {
        $DriverPack = $DriverPack | Where-Object { $_.OS -eq 'Win10' }
    }

    switch ($global:DeployMode) {

        "Refresh" { 
            ### OnLine
            # check drivers before continue.
            $dest_ = New-Item -Path "C:\Drivers" -ItemType Directory -Force
            Expand-DriverPack -FilePath "$($DeployRoot)$($DriverPack.Source)" -Destination $dest_.FullName -Manufacturer $global:deployInfo.DeviceModel.Manufacturer
            Add-DriversOnline -DriverPath $dest_.FullName
        }
        "BareMetal" { 
            ### OffLine
            $dest_ = New-Item -Path "$($global:drivesystem)\Drivers" -ItemType Directory -Force
            Expand-DriverPack -FilePath "$($DeployRoot)$($DriverPack.Source)" -Destination $dest_.FullName -Manufacturer $global:deployInfo.DeviceModel.Manufacturer
            Add-DriversOffline -DriveSystem $global:drivesystem -DriverPath $dest_.FullName 
        }
    }
}
function Invoke-DeployFirmware {
    <#
        .FUNCTIONALITY
    #>
    ### INSTALL RESOURCE
    Install-PSResource Firmware -Repository DCM2_PSResources

    Write-Host "> Deploy-Firmware" -ForegroundColor DarkMagenta

    $Firmware = $global:deployInfo.Firmware
    if ($null -eq $Firmware) { continue }

    New-Item -Path "$($global:DeployWorkDir)\Firmware" -ItemType Directory -Force | Out-Null
    if (-not (Test-Path -Path "$($global:DeployWorkDir)\Firmware\$($Firmware.FileName)" -PathType Leaf)) {
        Copy-Firmware -Firmware $Firmware -Destination "$($global:DeployWorkDir)\Firmware"
    }

    switch ($global:DeployMode) {

        "BareMetal" {
            ###
            #Invoke-Updatefirmware
        }
        "Refresh" { 
            ###
            Invoke-Updatefirmware
        }
    }
}
function Invoke-DeployApplications {
    <#
        .FUNCTIONALITY
        Offline, Copia instaladores para disco local.
        Online, verifica se existe repo local, se não, instalado do repo da rede.
    #>
    ### INSTALL RESOURCES
    Install-PSResource Application -Repository DCM2_PSResources

    Write-Host "> Deploy-Applications" -ForegroundColor DarkMagenta

    switch ($global:DeployMode) {

        "BareMetal" {
            ###
            Copy-Applications -ListApps $global:deployInfo.Applications -Destination "$($global:drivesystem)\Deploy\Applications"
        }
        "Resume" { 
            ###
            if (-not (Test-Path -Path "$($global:DeployWorkDir)\Applications" -PathType Container)) {
                Copy-Applications -ListApps $global:deployInfo.Applications -Destination "$($global:drivesystem)\Deploy\Applications"
            }
            Install-Applications -ListApps $global:deployInfo.Applications
        }
        ###
        ### atençao aqui resume, refresh, replace
        ### 
    }
}
function Invoke-DeployOffice365 {
    <#
        .FUNCTIONALITY
    #>
    ### INSTALL RESOURCES
    Install-PSResource Office365 -Repository DCM2_PSResources

    Write-Host "> Deploy-Office365" -ForegroundColor DarkMagenta

    switch ($global:DeployMode) {

        "BareMetal" {
            ###
            Copy-Item -Path "$($DeployRoot)\Sources\ODT" -Destination "$($global:drivesystem)\Deploy\ODT"  -Recurse -Force
            Build-ConfigOffice -ProductID $global:deployInfo.PIDOffice -Output "$($global:drivesystem)\Deploy\ODT\Config.xml"
        }
        "Refresh" { 
            ###
            if (-not (Test-Path -Path "C:\Deploy\ODT\setup.exe" -PathType Leaf)) {
                Copy-Item -Path "$($DeployRoot)\Sources\ODT" -Destination "C:\Deploy\ODT"  -Recurse -Force
            }
            if (-not (Test-Path -Path "C:\Deploy\ODT\Config.xml" -PathType Leaf)) {
                Build-ConfigOffice -ProductID $global:deployInfo.PIDOffice -Output "C:\Deploy\ODT\Config.xml"
            }
            Install-Office365
        }
    }
}
function Invoke-DeployProvisionedAppx {
    <#
        .FUNCTIONALITY
    #>
    ### INSTALL RESOURCES
    Install-PSResource Appx -Repository DCM2_PSResources
    Install-PSResource Control -Repository DCM2_PSResources

    Write-Host "> Deploy-ProvisionedAppx" -ForegroundColor DarkMagenta

    $blacklist = Import-BlackListAppx | Where-Object { $_.OS -eq $global:deployInfo.Image.ShortName }

    switch ($global:DeployMode) {

        "BareMetal" {
            Remove-ProvisionedAppx -List $blacklist.Packages
        }
        "Refresh" { 
            Remove-ProvisionedAppx -List $blacklist.Packages -Online
        }
    }
}
function Invoke-DeployFiles {
    <#
        .FUNCTIONALITY
    #>
    ### INSTALL RESOURCES
    Install-PSResource Copy -Repository DCM2_PSResources

    Write-Host "> Deploy-Files" -ForegroundColor DarkMagenta

}
function Invoke-DeployFeatures {
    <#
        .FUNCTIONALITY
    #>
    ### INSTALL RESOURCES
    Install-PSResource Features -Repository DCM2_PSResources
    
    Write-Host "> Deploy-Features" -ForegroundColor DarkMagenta

}


<#
    .NOTES
    BareMetal, Refresh, Replace, Resume.
#>
function Invoke-BareMetal {
    <#
        .DESCRIPTION
    #>
    begin {
        ###
        Write-Host "> Invoke-BareMetal" -ForegroundColor Blue
        ###
        New-Variable -Name DeployMode -Value BareMetal -Scope Global -Force
        
        Install-PSResource Control -Repository DCM2_PSResources
 
        Get-DeployInfo
 
        #Continue?
        $private:Result = [System.Windows.MessageBox]::Show('Check information! continue?','Informations','YesNo','Question')
        if ($private:Result.value__ -eq 7) { Exit }
        
    }
    process {
        
        ###
        Invoke-DeployDisk
  
        ###
        Set-DeployWorkDir
 
        ###
        Invoke-DeployImage

        ###
        Invoke-DeployUnattend

        ###
        Invoke-DeployDrivers
 
        ###
        Invoke-DeployFirmware

        ###
        Invoke-DeployApplications

        ###
        Invoke-DeployOffice365

        ###
        Invoke-DeployProvisionedAppx

        ###
        Invoke-DeployFiles

        ###
        Invoke-DeployFeatures
        
    }
    end {
        ### Save DeployInfo.json file.
        $global:deployInfo | ConvertTo-Json -Depth 5 | Set-Content "$($global:drivesystem)\Deploy\DeployInfo.json" -Force

        ### Restart computer if running WinPE.
        if (Test-Path -Path "HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\WinPE") {
            Restart-Computer -Force
        }
        ###
        ### Show Msg Finish. or send log, or  Set Finish.
    }
}
function Invoke-Refresh {
    <#
        .DESCRIPTION
    #>
    begin {
        ###
        Write-Host "> Invoke-BareMetal" -ForegroundColor Blue

        ###
        New-Variable -Name DeployMode -Value "Refresh" -Scope Global -Force

        Install-PSResource Control -Repository DCM2_PSResources

        $global:deployInfo = Get-DeployInfo

        #Continue?
        $private:Result = [System.Windows.MessageBox]::Show('Check information! continue?','Informations','YesNo','Question')
        if ($private:Result.value__ -eq 7) { Exit }
    }
    process {

        ###
        Set-DeployWorkDir
        
        ###
        Invoke-DeploySetup

        ###
        Invoke-DeployUnattend

        ###
        Invoke-DeployDrivers
        
        ###
        Invoke-DeployFirmware
        
        ###
        Invoke-DeployApplications
        
        ###
        Invoke-DeployOffice365
        
        ###
        Invoke-DeployProvisionedAppx
        
        ###
        Invoke-DeployFiles
         
        ###
        Invoke-DeployFeatures
        
    }
    end {
        ### Save DeployInfo.json file.
        $global:deployInfo | ConvertTo-Json -Depth 5 | Set-Content "$($global:drivesystem)\Deploy\DeployInfo.json" -Force

        ### Restart.
        Restart-Computer -Force
    }
}
function Invoke-Replace {
     <#
        .DESCRIPTION
    #>
    ###
    Write-Host "Invoke-Replace" -ForegroundColor Blue

    Install-PSResource Control -Repository DCM2_PSResources

    ###
    New-Variable -Name DeployMode -Value "Replace" -Scope Global -Force
}
function Invoke-Resume {
    <#
        .DESCRIPTION
    #>
    begin {
        ###
        Write-Host "Resume-Deploy" -ForegroundColor Blue
        break
        Set-Variable -Name DeployMode -Value "Online" -Scope Global
        
        Install-PSResource Control -Repository DCM2_PSResources

    }
    process {
        ###
        #Conect to server

        ###
        Deploy-Drivers

        ###
        Deploy-Firmware

        ###
        Deploy-Applications

        ###
        Deploy-Office365

        ###
        Deploy-ProvisionedAppx

        ###
        Deploy-Files

        ###
        Deploy-Features
        # add registries
        #...  
    }
    end {
        Restart-Computer -Force
    }
}

function Invoke-DeploySystem {
    <#
        .DESCRIPTION
        Invoke Deploy.

        .PARAMETER BareMetal
        Este cenário ocorre quando existe um dispositivo sem nenhum SO instalado que precisa de ser implementado. 
        Este cenário também pode ser um dispositivo existente que precisa de ser apagado e reimplementado sem ter de preservar quaisquer dados existentes.

        .PARAMETER Refresh
        O processo costuma ser iniciado no sistema operacional em execução. 
        É feito o backup das configurações e dos dados do usuário, depois restaurados como parte do processo de implantação.

        .PARAMETER Replace
        A substituição de um computador é semelhante ao cenário de atualização. 
        No entanto, uma vez que estamos a substituir o dispositivo, dividimos este cenário em duas tarefas:
        Cópia de segurança do cliente antigo e implementação Bare-Metal do novo cliente.

        .PARAMETER Resume
        Continua deploy após reinicializar o dispositivo.
    #>
    [CmdletBinding(DefaultParameterSetName = 'BareMetal')]
    param(
        [Parameter(ParameterSetName="BareMetal")] [switch]$BareMetal,
        [Parameter(ParameterSetName="Refresh")] [switch]$Refresh,
        [Parameter(ParameterSetName="Replace")] [switch]$Replace,
        [Parameter(ParameterSetName="Resume")] [switch]$Resume
    )  
    ###
    if ($null -ne $DCM2Root) {
        New-Variable -Name DeployRoot -Value "$($DCM2Root)\Deployment" -Scope Global -Force
    }
    if ($null -eq $DeployRoot) {
        New-Variable -Name DeployRoot -Value "$($PSScriptRoot)" -Scope Global -Force
    }
    
    ###
    switch ($psCmdlet.ParameterSetName) {

        "BareMetal" { 
            Invoke-BareMetal
        }
        "Refresh" { 
            Invoke-Refresh
        }
        "Replace" { 
            Invoke-Replace
        }
        "Resume" { 
            Invoke-Resume
        }
    }
    ###
    return "Finish!"
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
