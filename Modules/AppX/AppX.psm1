<#
+----------------------------------------------------------------------------------------+
    .Description
    AppX
	Created by: Gilmar Prust
	Filename:   AppX.psm1
+----------------------------------------------------------------------------------------+
#>

function Remove-ProvisionedAppx {
    
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $BlackList,
        $Image
    )
    $BlackListApps = New-Object -TypeName System.Collections.ArrayList
    Write-Host "Removing Appx..." -ForegroundColor Yellow
    ($BlackList).Appx | ForEach-Object {
        if(($_.OS -eq "All") -or ($_.OS -eq $Image.ShortName)) { $BlackListApps.Add($_.Name) | Out-Null }
    }
    $percentItem = 100/$BlackListApps.Count
    $percentRemoved = 0
    Get-AppxProvisionedPackage -Path "$($Global:LetterSystem)\" | Select-Object -Property DisplayName, PackageName | ForEach-Object { 
        if ($_.DisplayName -in $BlackListApps) {
            Write-Progress -Id 1 -Activity "Remove Provisioned Appx" -Status " $($_.DisplayName)" -PercentComplete $percentRemoved
            Remove-AppxProvisionedPackage -Path "$($Global:LetterSystem)\" -PackageName $_.PackageName -ErrorAction Continue | Out-Null
            Write-Host "$($_.DisplayName) removed." -ForegroundColor Gray
            $percentRemoved = $percentItem + $percentRemoved
        }
    }
    Write-Progress -Id 1 -Activity "Remove Provisioned Appx" -Status "Completed!" -Completed
}