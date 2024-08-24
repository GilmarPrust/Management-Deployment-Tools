<#
+----------------------------------------------------------------------------------------+
    .Description
    Copy
	Created by: Gilmar Prust
	Filename:   Copy.psm1
+----------------------------------------------------------------------------------------+
#>

function Copy-OEM {

    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
        $Source,
        $Destination
    )
    #Copy OEM files
    Write-Host "Copying OEM files..." -ForegroundColor Yellow -NoNewline
    Copy-Item -Path "$($Source)\Resources\OEM\*" -Destination "$($Destination)\" -Recurse -Force 
    Write-Host " Done!" -ForegroundColor Green
}

function Copy-Resources {

    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
        $Source,
        $Destination
    )
    Write-Host "Copying Deploy files..." -ForegroundColor Yellow -NoNewline
    New-Item -Path $Destination -ItemType Directory -Force | Out-Null
    Copy-Item -Path "$($Source)\Resources\AppAssociations.xml" -Destination $Destination -Force
    Copy-Item -Path "$($Source)\Resources\WEGACCESS.xml" -Destination $Destination -Force
    Copy-Item -Path "$($Source)\Resources\Registry\*" -Destination $Destination -Recurse -Force
    #Copy-Item -Path "$($Source)\Resources\PowerShell\*" -Destination $Destination -Recurse -Force
    Copy-Item -Path "$($Source)\Scripts\Deploy-Tasks\*" -Destination $Destination -Recurse -Force
    
    Write-Host " Done!" -ForegroundColor Green
}

function Copy-Applications {

    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
        $Source,
        $ProfileGuids,
        $ApplicationList
    )
    Write-Host "Copying applications..." -ForegroundColor Yellow
    $AppsToCopy = New-Object System.Collections.ArrayList

    #Add Profile applications to copy
    foreach ($guid in $ProfileGuids) {
        $app = $null
        $app = ($ApplicationList | Where-Object { $_.guid -eq $guid -and $_.enable -eq $true })
        if ($null -ne $app) { $AppsToCopy.Add($app) | Out-Null }
    }

    $AppsToCopy | ForEach-Object {
        Write-Host "Copying... $($_.Name)" -ForegroundColor Gray
        $destination = "$($Global:LetterSystem)\Deploy\Applications\$($_.Name)"
        New-Item -ItemType Directory -Path $destination -Force | Out-Null
        Copy-Item -Path "$($Source)$($_.Source)\*" -Destination $destination -Recurse -Force
    }
    $AppsToCopy | ConvertTo-Json | Out-File "$($Global:LetterSystem)\Deploy\Applications\AppToInstall.json"
}

function Copy-Office {

    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
        $ProductName
    )
    Write-Host "Copying Office..." -ForegroundColor Gray
    switch -Wildcard ($ProductName) {
        "*2010" { Write-Host "Office 2010 not working!" }
        "*2013" { Write-Host "Office 2010 not working!" }
        Default { Copy-Item -Path "$($DeployRoot)\Sources\ODT" -Destination "$($Global:LetterSystem)\Deploy\ODT"  -Recurse -Force }
    }
}

function Copy-Firmware {

    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $Device,
        $Firmware,
        $Destination
    )
    Write-Host "Copying Firmware..." -ForegroundColor Yellow -NoNewline
    New-Item -ItemType Directory -Path $Destination -Force | Out-Null
    Copy-Item -Path "$($DeployRoot)$($Firmware.Source)" -Destination "$($Destination)\" -Force
    New-Item -Path "$($Destination)\" -Name "fwVersion.txt" -Value "$($Firmware.Version)" -ItemType File | Out-Null
    if ($Device.Manufacturer -eq 'Dell Inc.') {
        Copy-Item -Path "$($DeployRoot)\Firmwares\Dell\Flash64W.exe" -Destination "$($Destination)" -Force
    }
    Write-Host " Done!" -ForegroundColor Green
}

function Copy-Driver {

    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $Pack,
        $Destination,
        $Device
    )
    Write-Host "Copying drivers..." -ForegroundColor Yellow -NoNewline
    New-Item -Path $Destination -ItemType Directory -Force | Out-Null
    switch ((Get-Item "$($DeployRoot)$($Pack)").Extension) {
        '.7z'  { Start-Process -Wait -FilePath "$($DeployRoot)\Tools\7z\x64\7za.exe" -ArgumentList "x $($DeployRoot)$($Pack)", "-o$($Destination) -y -xr!x86 -xr!amd -xr!nvidia" }
        '.CAB' { Start-Process -Wait -FilePath "$($DeployRoot)\Tools\7z\x64\7za.exe" -ArgumentList "x $($DeployRoot)$($Pack)", "-o$($Destination) -y -xr!x86 -xr!amd -xr!nvidia" }
        '.exe' {
            if($Device.Manufacturer -eq "Dell Inc.") {
                Start-Process -Wait -FilePath "$($DeployRoot)\Tools\MiniUnz\miniunz.exe" -ArgumentList "-x $($DeployRoot)$($Pack)", "-d $($Destination)"
            }
            if($Device.Manufacturer -eq "Lenovo") {
                Start-Process -Wait -FilePath "$($DeployRoot)$($Pack)" -ArgumentList "/verysilent", "/DIR=$($Destination)"
            }
            #Exclude x86|amd\nvidia drivers
            #Get-ChildItem $Destination -recurse | Where-Object { $_.PSIsContainer -eq $true -and $_.Name -match 'x86|amd|nvidia' -and $_.GetFiles().Count -eq 0 } | Remove-Item -Recurse -Verbose
            Get-ChildItem $Destination -recurse | Where-Object { $_.PSIsContainer -eq $true -and $_.Name -match 'x86' -and $_.GetFiles().Count -eq 0 } | Remove-Item -Recurse -Verbose
        }
        Default { Write-Host "Unconfigured file extension." -ForegroundColor Red }
    }
    Write-Host " Done!" -ForegroundColor Green
}