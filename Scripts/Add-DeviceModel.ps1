<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    DCM2 Module.
    
	.NOTES
    Created by: Gilmar Prust
	Filename: DCM2.psm1

+----------------------------------------------------------------------------------------+
#>

$body = @{
    manufacturer = "Lenovo"
    model = "ThinkPad T14"
    type = "desktop"
} | ConvertTo-Json


Invoke-RestMethod -Uri "http://localhost:5041/devices" -Method Post -Body $body -ContentType "application/json"




Invoke-RestMethod -Uri "http://localhost:5041/devices" -Method Get



$model = "Optiplex"
$type = "0930"
$url = "http://localhost:5000/devices/filter?model=$model&type=$type"

Invoke-RestMethod -Uri $url -Method Get



$guid = "a88ccd5a-1877-4298-a56f-2634808b92fb"

Invoke-RestMethod -Uri "http://localhost:5041/devices/$guid" -Method Delete
