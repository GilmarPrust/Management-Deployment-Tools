<#
+----------------------------------------------------------------------------------------+
    .Description
    Download Windows Image
	Created by: Gilmar Prust
	Filename: Download-Image.ps1
	.NOTES
	$randomHex = -join ((65..70) + (48..57) | Get-Random -Count 5 | ForEach-Object { [char]$_ })
	Write-Output $randomHex
+----------------------------------------------------------------------------------------+
#>
Add-Type -AssemblyName PresentationCore, PresentationFramework


### GLOBAL VARIABLES
New-Variable -Name DeployRoot -Value "$($PSScriptRoot)" -Scope Global -Force -Option ReadOnly
Set-Location $DeployRoot -Verbose

### IMPORT MODULES
Import-Module .\Modules\Download -Force -ErrorAction Stop -Verbose
Import-Module .\Modules\Control -Force -ErrorAction Stop -Verbose


$Script:JsonSettings = Import-JsonSettings
$Script:JsonImages = Import-Images


$SelectedOS = $JsonSettings.Catalog.ESD | Out-GridView -Title "Select Catalog to Download" -OutputMode Single
if ($null -eq $SelectedOS) { Exit }

# Get link to download
$request = [System.Net.HttpWebRequest]::Create($SelectedOS.Link)
$request.Method = "HEAD"
$response = $request.GetResponse()
$fUri = $response.ResponseUri
$response.Close()
$fileCab = Split-Path -Path $fUri.OriginalString -Leaf

try {
    Invoke-WebRequest -Uri $fUri.OriginalString -OutFile "$($env:Temp)\$($fileCab)" | Out-Null
    $filename = [System.IO.Path]::GetFileNameWithoutExtension("$($env:Temp)\$($fileCab)")
    expand "$($env:Temp)\$($fileCab)" "$($PSScriptRoot)\Images\$($filename).xml" | Out-Null
} Catch {
    Write-Host $_.Exception.Message -BackgroundColor Red
}


$product = Select-Xml -Path "$($PSScriptRoot)\Images\$($filename).xml" -XPath '/MCT/Catalogs/Catalog/PublishedMedia/Files/File' | Select-Object -ExpandProperty Node
$product = $product | Select-Object -Property LanguageCode, Edition, Architecture, FileName, FilePath, Sha1 | Out-GridView -Title "Select file to download." -OutputMode Single
if ($null -eq $product) { exit }


$esdfilepath = "$($PSScriptRoot)\Images\ESD\$($product.FileName)"


#Check if file alread exist
If (!(Test-Path -Path $esdfilepath -PathType Leaf)) {
    
    $esdfile = Download -Url $product.FilePath -Destination $esdfilepath

} else {
    Write-host "File already exist." -ForegroundColor Yellow
}

#Create Default Media
$InfoImage = Get-WindowsImage -ImagePath $esdfile -Verbose

#Set build folder name
$buildsplit = $product.FileName.Split('.')
$buildFolder = "$($buildsplit[0]).$($buildsplit[1])"

$build = Get-Item -Path "$($PSScriptRoot)\Images\WIM\$($buildFolder)\Default\*" -ErrorAction SilentlyContinue
if ($null -ne $build) {

    $Result = [System.Windows.MessageBox]::Show('Select (Yes) to create new, (No) use existing image','Build alread exist','YesNo','Question')
    #Yes
    if ($Result.value__ -eq 6) {
        Remove-Item -Path "$($PSScriptRoot)\Images\WIM\$($buildFolder)\Default\*" -Recurse -Force -Verbose -ErrorAction SilentlyContinue
        Write-Host "Expanding image index: 1 - Windows Setup" -ForegroundColor Green
        Expand-WindowsImage -ImagePath $esdfilepath -ApplyPath "$($PSScriptRoot)\Images\WIM\$($buildFolder)\Default" -Index 1 | Out-Null
        #Create Boot.wim
        foreach ($item in $InfoImage | Where-Object { $_.ImageIndex -match "(2|3)" }) {
            Write-Host "Exporting image index: $($item.ImageIndex) - $($item.ImageName)" -ForegroundColor Green
            Export-WindowsImage -SourceImagePath $esdfilepath -SourceIndex $item.ImageIndex -DestinationImagePath "$($PSScriptRoot)\Images\WIM\$($buildFolder)\Default\Sources\boot.wim" -CheckIntegrity -CompressionType Max -Verbose | Out-Null
        }
        #Create install.wim
        $InfoImage = $InfoImage | Where-Object { $_.ImageIndex -notmatch "(1|2|3)" } | Out-GridView -OutputMode Multiple -Title "Select images to create install.wim"
        foreach ($item in $InfoImage) {
            Write-Host "Exporting image index: $($item.ImageIndex) - $($item.ImageName)" -ForegroundColor Green
            Export-WindowsImage -SourceImagePath $esdfilepath -SourceIndex $item.ImageIndex -DestinationImagePath "$($PSScriptRoot)\Images\WIM\$($buildFolder)\Default\Sources\install.wim" -CheckIntegrity -CompressionType Max -Verbose | Out-Null
        }
    }
    elseif ($Result.value__ -eq 7) {
        #Create install.wim
        $InfoImage = $InfoImage | Where-Object { $_.ImageIndex -notmatch "(1|2|3)" } | Out-GridView -OutputMode Multiple -Title "Select images to add to install.wim"
        foreach ($item in $InfoImage) {
            Write-Host "Exporting image index: $($item.ImageIndex) - $($item.ImageName)" -ForegroundColor Green
            Export-WindowsImage -SourceImagePath $esdfilepath -SourceIndex $item.ImageIndex -DestinationImagePath "$($PSScriptRoot)\Images\WIM\$($buildFolder)\Default\Sources\install.wim" -CheckIntegrity -CompressionType Max -Verbose | Out-Null
        }
    } else { Exit }

} else {

    Write-Host "Expanding image index: 1 - Windows Setup" -ForegroundColor Green
    New-Item -Path "$($PSScriptRoot)\Images\WIM\$($buildFolder)\Default" -ItemType Directory -Force | Out-Null
    Expand-WindowsImage -ImagePath $esdfilepath -ApplyPath "$($PSScriptRoot)\Images\WIM\$($buildFolder)\Default" -Index 1 | Out-Null
    #Create Boot.wim
    foreach ($item in $InfoImage | Where-Object { $_.ImageIndex -match "(2|3)" }) {
        Write-Host "Exporting image index: $($item.ImageIndex) - $($item.ImageName)" -ForegroundColor Green
        Export-WindowsImage -SourceImagePath $esdfilepath -SourceIndex $item.ImageIndex -DestinationImagePath "$($PSScriptRoot)\Images\WIM\$($buildFolder)\Default\Sources\boot.wim" -CheckIntegrity -CompressionType Max -Verbose | Out-Null
    }

    #Create install.wim
    $InfoImage = $InfoImage | Where-Object { $_.ImageIndex -notmatch "(1|2|3)" } | Out-GridView -OutputMode Multiple -Title "Select images to create install.wim"
    foreach ($item in $InfoImage) {
        Write-Host "Exporting image index: $($item.ImageIndex) - $($item.ImageName)" -ForegroundColor Green
        Export-WindowsImage -SourceImagePath $esdfilepath -SourceIndex $item.ImageIndex -DestinationImagePath "$($PSScriptRoot)\Images\WIM\$($buildFolder)\Default\Sources\install.wim" -CheckIntegrity -CompressionType Max -Verbose | Out-Null
    }
}




#Create Jsonlist from all Images
$installFiles = Get-ChildItem -Path "$($PSScriptRoot)\Images\WIM" -Filter "install.wim" -Recurse
$listimages = New-Object System.Collections.ArrayList
foreach ($file in $installFiles) {

    Get-WindowsImage -ImagePath $file.FullName | ForEach-Object {
        $info = Get-WindowsImage -ImagePath $file.FullName -Index $_.ImageIndex | Select-Object -Property ImageIndex,ImageName,ImageDescription,EditionId,ImageSize,Version,Languages
        $info | Add-Member -NotePropertyMembers @{Guid=(New-Guid).Guid}
        switch -Regex ($_.ImageName) {
            "Windows 10" { 
                $info | Add-Member -NotePropertyMembers @{ShortName="Win10"}
            }
            "Windows 11" { 
                $info | Add-Member -NotePropertyMembers @{ShortName="Win11"}
            }
        }

        $info | Add-Member -NotePropertyMembers @{Source="$($file.FullName.Replace("$($PSScriptRoot)", ''))"}
        $listimages.Add(($info | Select-Object -Property Guid, ImageName, ImageDescription, ImageIndex, ShortName, EditionId, Version, Languages, ImageSize, Source)) | Out-Null
    }
}
$selectedImagens = $listimages | Out-GridView -Title "Select imagens" -OutputMode Multiple

if ($null -ne $selectedImagens) { 
    $selectedImagens | ConvertTo-Json | Out-File "$($PSScriptRoot)\Modules\Control\Images.json" -Force
}