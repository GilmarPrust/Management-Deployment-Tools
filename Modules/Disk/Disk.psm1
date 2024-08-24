<#
+----------------------------------------------------------------------------------------+
    .Description
    Image
	Created by: Gilmar Prust
	Filename:   Image.psm1
    New-Partition -offset 4294967296=4gb
	100gb*1024 = 30.720mb * 1024 = 31.457.280kb *1024 = 107374182400 bytes
     30gb*1024 = 30.720mb * 1024 = 31.457.280kb *1024 = 32212254720 bytes
      2gb*1024 = 2.048mb  * 1024 = 2.097.152kb  *1024 = 2147483648 bytes
+----------------------------------------------------------------------------------------+
#>

function Format-Disk {
    
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
        $Disk,
        $FirmwareType
    )

    Write-Host "Creating partition..." -ForegroundColor Yellow
    try {
        #Clear Disk
        if ($Disk.PartitionStyle -match "(GPT|MBR|Unknown)"){
            Clear-Disk -Number $Disk.DiskNumber -RemoveData -RemoveOEM -confirm:$false -ErrorAction Stop
        }
        Switch ($FirmwareType) {

            "UEFI" {
                Initialize-Disk -Number $Disk.Number -PartitionStyle GPT -ErrorAction Stop
                New-Partition -DiskNumber $Disk.Number -Size 260MB -GptType "{c12a7328-f81f-11d2-ba4b-00a0c93ec93b}" -AssignDriveLetter | 
                Format-Volume -FileSystem FAT32 -Force -NewFileSystemLabel "Boot" -ErrorAction Stop | Out-Null
                New-Partition -DiskNumber $Disk.Number -Size 16MB -GptType "{e3c9e316-0b5c-4db8-817d-f92df00215ae}" -ErrorAction Stop | Out-Null
                New-Partition -DiskNumber $Disk.Number -UseMaximumSize -AssignDriveLetter | 
                Format-Volume -FileSystem NTFS -Force -NewFileSystemLabel "System" -ErrorAction Stop | Out-Null
            }
            "BIOS" {
                Initialize-Disk -Number $Disk.Number -PartitionStyle MBR -ErrorAction Stop
                New-Partition -DiskNumber $Disk.Number -Size 512MB -AssignDriveLetter -IsActive | 
                Format-Volume -FileSystem FAT32 -Force -NewFileSystemLabel "Boot" -ErrorAction Stop | Out-Null
                New-Partition -DiskNumber $Disk.Number -UseMaximumSize -AssignDriveLetter | 
                Format-Volume -FileSystem NTFS -Force -NewFileSystemLabel "System" -ErrorAction Stop | Out-Null
            }
        }
    }
    catch {
        Write-Host $_.Exception.Message -BackgroundColor Red
        break
    }
}