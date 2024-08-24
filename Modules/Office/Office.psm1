<#
    .Description
    Office
	Created by: Gilmar Prust
	Filename:   Office.psm1
#>

function Get-OfficeInfos {
    
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $ComputerName,
        $DataBase
    )
    Begin {
        $product = @{}
        $SqlConnection = New-Object System.Data.SqlClient.SqlConnection
        $server = "$($DataBase.Server)\$($DataBase.Instance)"
        $SqlConnection.ConnectionString = "Server=$($server); Database=$($DataBase.Name); Integrated Security=false; User ID=aes_user; Password=aesweggch; Connect Timeout=30"
        $SqlConnection.Open()
    }
    Process{
        try {
            
            if($SqlConnection.State -eq 'Open')	{
                Write-Host "Getting office license..." -ForegroundColor Yellow
                Write-Host "SQL connection open." -ForegroundColor Red -NoNewline
                $SqlCmd = New-Object System.Data.SqlClient.SqlCommand
                $SqlCmd.Connection = $SqlConnection
                $SqlCmd.CommandText = "SELECT ProductName, ActivationKeyOffice, ProductKeyOffice, EmailActivationOffice FROM Settings WHERE OSDComputerName='$($ComputerName)' AND Type='C'"
                $reader = $SqlCmd.ExecuteReader()
                if($reader.HasRows) {
                    $reader.Read() | Out-Null
                    $product.Add("ProductName", "$($reader[0])")
                    $product.Add("ActivationKey", "$($reader[1])")
                    $product.Add("ProductKey", "$($reader[2])")
                    $product.Add("EmailActivation", "$($reader[3])")
                    $reader.Close()
                }
                if (![string]::IsNullOrEmpty($product["ProductName"])) {
                    $SqlCmd.CommandText = "SELECT ID FROM Office_Product_Name WHERE ProductName='$($product["ProductName"])'"
                    $reader = $SqlCmd.ExecuteReader()
                    if($reader.HasRows) {
                        $reader.Read() | Out-Null
                        $product.Add("ProductId", "$($reader[0])")
                        $reader.Close()
                    }
                }
            }
        }
        catch {
            Write-Host $_.Exception.Message -BackgroundColor Red
        }
        finally {
            if($SqlConnection.State -eq 'Open') { 
                $SqlConnection.Close()
                Write-Host " Close!" -ForegroundColor Green
            }
        }
    }
    End{
        return $product
    }
}

function Get-OfficeInfoKace {
    
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $ComputerName,
        $DataBase
    )
    Begin {
        $product = @{}
        $SqlConnection = New-Object System.Data.SqlClient.SqlConnection
        $server = "$($DataBase.Server)\$($DataBase.Instance)"
        $SqlConnection.ConnectionString = "Server=$($server); Database=$($DataBase.Name); Integrated Security=false; User ID=aes_user; Password=aesweggch; Connect Timeout=30"
        $SqlConnection.Open()
    }
    Process{
        try {
            
            if($SqlConnection.State -eq 'Open')	{
                Write-Host "Getting office license..." -ForegroundColor Yellow
                Write-Host "SQL connection open." -ForegroundColor Red -NoNewline
                $SqlCmd = New-Object System.Data.SqlClient.SqlCommand
                $SqlCmd.Connection = $SqlConnection
                $SqlCmd.CommandText = "SELECT ProductName, ActivationKeyOffice, ProductKeyOffice, EmailActivationOffice FROM Settings WHERE OSDComputerName='$($ComputerName)' AND Type='C'"
                $reader = $SqlCmd.ExecuteReader()
                if($reader.HasRows) {
                    $reader.Read() | Out-Null
                    $product.Add("ProductName", "$($reader[0])")
                    $product.Add("ActivationKey", "$($reader[1])")
                    $product.Add("ProductKey", "$($reader[2])")
                    $product.Add("EmailActivation", "$($reader[3])")
                    $reader.Close()
                }
                if (![string]::IsNullOrEmpty($product["ProductName"])) {
                    $SqlCmd.CommandText = "SELECT ID FROM Office_Product_Name WHERE ProductName='$($product["ProductName"])'"
                    $reader = $SqlCmd.ExecuteReader()
                    if($reader.HasRows) {
                        $reader.Read() | Out-Null
                        $product.Add("ProductId", "$($reader[0])")
                        $reader.Close()
                    }
                }
            }
        }
        catch {
            Write-Host $_.Exception.Message -BackgroundColor Red
        }
        finally {
            if($SqlConnection.State -eq 'Open') { 
                $SqlConnection.Close()
                Write-Host " Close!" -ForegroundColor Green
            }
        }
    }
    End{
        return $product
    }
}


function CreateConfigOffice {

    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        $ProductID,
        [Parameter(Mandatory=$false)]
        $ProductKey,
        $ActivationKey,
        $EmailActivation
    )
    #Create Config.xml
    Write-Host "Creating config.xml" -ForegroundColor Yellow -NoNewline
    $XML = New-Object System.XML.XMLDocument
    $Configuration = $XML.CreateElement("Configuration")
    $XML.appendChild($Configuration) | Out-Null
    $Add = $XML.CreateElement("Add")
    $Add.SetAttribute("OfficeClientEdition","64")
    $Add.SetAttribute("Channel","MonthlyEnterprise")
    $Add.SetAttribute("SourcePath","C:\Deploy\ODT")
    $Product = $XML.CreateElement("Product")
    $Product.SetAttribute("ID","$($ProductID)")
    if (($ProductID -ne "O365ProPlusRetail") -or ($ProductID -ne "O365BusinessRetail")) {
        if ($ProductKey.Length -eq 29) {
            $Product.SetAttribute("PIDKEY","$($ProductKey)")
        }
        elseif ($ActivationKey.Length -eq 29) {
            $Product.SetAttribute("PIDKEY","$($ActivationKey)")
        }
    }
    $Language = $XML.CreateElement("Language")
    $Language.SetAttribute("ID","pt-BR")
    $Product.AppendChild($Language) | Out-Null
    $Add.AppendChild($Product) | Out-Null
    $Configuration.AppendChild($Add) | Out-Null
    $Updates = $XML.CreateElement("Updates")
    $Updates.SetAttribute("Enabled","False")
    $Configuration.AppendChild($Updates) | Out-Null
    $Display = $XML.CreateElement("Display")
    $Display.SetAttribute("Level","Full")
    $Display.SetAttribute("AcceptEULA","TRUE")
    $Configuration.AppendChild($Display) | Out-Null
    <#
    $Logging = $XML.CreateElement("Logging")
    $Logging.SetAttribute("Level","Standard")
    $Logging.SetAttribute("Path","%Temp%")
    $Configuration.AppendChild($Logging) | Out-Null
    #>
    $Property1 = $XML.CreateElement("Property")
    $Property1.SetAttribute("Name","AUTOACTIVATE")
    $Property1.SetAttribute("Value","1")
    $Configuration.AppendChild($Property1) | Out-Null
    $Property2 = $XML.CreateElement("Property")
    $Property2.SetAttribute("Name","FORCEAPPSHUTDOWN")
    $Property2.SetAttribute("Value","TRUE")
    $Configuration.AppendChild($Property2) | Out-Null
    <#
    $Property3 = $XML.CreateElement("Property")
    $Property3.SetAttribute("Name","SharedComputerLicensing")
    $Property3.SetAttribute("Value","0")
    $Configuration.AppendChild($Property3) | Out-Null
    #>
    $Property4 = $XML.CreateElement("Property")
    $Property4.SetAttribute("Name","PinIconsToTaskbar")
    $Property4.SetAttribute("Value","TRUE")
    $Configuration.AppendChild($Property4) | Out-Null

    #Xml Comment
    $comment1 = $XML.CreateComment("Product Key: $($ProductKey)")
    $comment2 = $XML.CreateComment("Activation Key: $($ActivationKey)")
    $comment3 = $XML.CreateComment("Email: $($EmailActivation)")
    $XML.AppendChild($comment1) | Out-Null
    $XML.AppendChild($comment2) | Out-Null
    $XML.AppendChild($comment3) | Out-Null
    
    New-Item -Path "$($Global:LetterSystem)\" -Name "Temp" -ItemType Directory -Force | Out-Null
    $XML.Save("$($Global:LetterSystem)\Temp\Config.xml")
    Write-Host " Done!" -ForegroundColor Green
}