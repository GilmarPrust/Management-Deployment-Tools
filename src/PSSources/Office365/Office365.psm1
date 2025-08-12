<#
+----------------------------------------------------------------------------------------+
    .DESCRIPTION
    Office365 Module
	Created by: Gilmar Prust
	Filename:   Office365.psm1
+----------------------------------------------------------------------------------------+
#>

function Build-ConfigOffice {

    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        $ProductID,
        $Output
    )
    #Create Config.xml
    Write-Host "Building Config.xml" -ForegroundColor Yellow
    $XML = New-Object System.XML.XMLDocument
    $Configuration = $XML.CreateElement("Configuration")
    $XML.appendChild($Configuration) | Out-Null
    $Add = $XML.CreateElement("Add")
    $Add.SetAttribute("OfficeClientEdition","64")
    $Add.SetAttribute("Channel","Current")
    $Add.SetAttribute("SourcePath","C:\Deploy\ODT")
    $Product = $XML.CreateElement("Product")
    $Product.SetAttribute("ID","$($ProductID)")
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
    $Property1 = $XML.CreateElement("Property")
    $Property1.SetAttribute("Name","AUTOACTIVATE")
    $Property1.SetAttribute("Value","1")
    $Configuration.AppendChild($Property1) | Out-Null
    $Property2 = $XML.CreateElement("Property")
    $Property2.SetAttribute("Name","FORCEAPPSHUTDOWN")
    $Property2.SetAttribute("Value","TRUE")
    $Configuration.AppendChild($Property2) | Out-Null
    $Property4 = $XML.CreateElement("Property")
    $Property4.SetAttribute("Name","PinIconsToTaskbar")
    $Property4.SetAttribute("Value","TRUE")
    $Configuration.AppendChild($Property4) | Out-Null

    #Xml Comment
    #$comment1 = $XML.CreateComment("Product Key: $($ProductKey)")
    #$XML.AppendChild($comment1) | Out-Null
    
    $XML.Save($Output)
}

function Get-DeviceIDOffice {
    <#
        .DESCRIPTION
    #>
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        $DeviceGuid
    )
    ###
    Install-PSResource Control -Repository DCM2_PSResources
    
    $deviceI = Import-DeviceIDOffice | Where-Object { $_.DeviceGuid -eq $DeviceGuid }

    return $deviceI.PIDOffice

}

function Install-Office365 {
    <#
        .DESCRIPTION
    #>
    
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