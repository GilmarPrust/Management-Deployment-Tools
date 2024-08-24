<#
+----------------------------------------------------------------------------------------+
    .Description
    Image
	Created by: Gilmar Prust
	Filename:   Image.psm1
    New-Partition -offset 2147483648
+----------------------------------------------------------------------------------------+
#>
function ApplyImage {

    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
        $Image,
        $Index
    )

    Write-Host "Applying Windows Image..." -ForegroundColor DarkYellow
    try {
        #Expand-WindowsImage -ImagePath "$($DeployRoot)$($Image)" -Index $Index -ApplyPath "$($Global:LetterSystem)" -CheckIntegrity -ErrorAction Stop | Out-Null
        #Start-Process -Wait -FilePath wimlib-imagex.exe -WorkingDirectory "$($DeployRoot)\Tools\WinLib" -ArgumentList "apply $($DeployRoot)$($Image) $($Index) $($Global:LetterSystem)"
        Invoke-Command -ScriptBlock { 

            Push-Location -Path "C:\Windows\System32"
            Set-Location -Path "$($DeployRoot)\Tools\amd64\DISM"
            Dism.exe /Apply-Image /ImageFile:"$($DeployRoot)$($Image)" /ApplyDir:"$($Global:LetterSystem)" /Index:$Index /CheckIntegrity
            Pop-Location
        }
        Write-Host $null
    }
    catch {
        Write-Host $_.Exception.Message -BackgroundColor Red
        break
    }
}

function CreateBcdBoot {
    param (
        [Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
        $FirmwareType
    )
    Write-Host "Creating BCDboot store..." -ForegroundColor Yellow
    try {
        Invoke-Command -ScriptBlock { 

            Push-Location -Path "C:\Windows\System32"
            Set-Location -Path "$($Global:LetterSystem)\Windows\System32"
            Switch ($FirmwareType){
                "UEFI" { bcdboot.exe $Global:LetterSystem\Windows /s $Global:LetterBoot /f UEFI }
                "BIOS" { bcdboot.exe $Global:LetterSystem\Windows /s $Global:LetterBoot /f BIOS }
            }
            Pop-Location
        }
    }
    catch {
        Write-Host $_.Exception.Message -BackgroundColor Red
        break
    }
}

function ApplyUnattend {
    param (
        [Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
        $Xml,
        $ComputerName
    )
    Write-Host "Create $($Global:LetterSystem)\Windows\Panther\unattend.xml" -ForegroundColor Yellow -NoNewline
    try {

        New-Item -Path "$($Global:LetterSystem)\Windows\" -Name "Panther" -ItemType Directory -Force | Out-Null

        #UPDATE ComputerName Unattend XML
        (($Xml.unattend.settings | Where-Object {$_.pass -eq 'specialize'}).component | Where-Object {$_.name -eq 'Microsoft-Windows-Shell-Setup'}).ComputerName = $Computername
        $XmlUnattend.Save("$($Global:LetterSystem)\Windows\Panther\unattend.xml")

        Invoke-Command -ScriptBlock { 

            Push-Location -Path "C:\Windows\System32"
            Set-Location -Path "$($DeployRoot)\Tools\amd64\DISM"
            Dism.exe /image:"$($Global:LetterSystem)\" /Apply-Unattend:"$($Global:LetterSystem)\Windows\Panther\unattend.xml" | Out-Null
            Pop-Location
        }
		Write-Host " Done!" -ForegroundColor Green
    }
    catch {
        Write-Host $_.Exception.Message -BackgroundColor Red
        break
    }    
}