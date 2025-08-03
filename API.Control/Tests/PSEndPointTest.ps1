
$uri = "https://localhost:7012/api/devicemodels"

$devicemodel = Invoke-RestMethod -Uri $uri -Method Get


$body = @{
    ComputerName    = "VM-00098"
    SerialNumber    = "SN17e8"
    MacAddress      = "10-7C-61-R4-F6-D0"
    DeviceModelId   = ($devicemodel | Where-Object { $_.manufacturer -eq 'Unknown' }).Id
} | ConvertTo-Json

$uri = "https://localhost:7012/api/devices"

$response = Invoke-RestMethod -Uri $uri -Method Post -Body $body -ContentType "application/json"

$response




break

$uri = "https://localhost:7012/api/devices"

$devices = Invoke-RestMethod -Uri $uri -Method Get
$devices