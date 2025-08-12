# Carrega a DLL
$path = "D:\_DEV\PROJETOS\D.C.M2\Core\bin\Debug\netstandard2.0\Core.dll"

# Carregar a DLL
[System.Reflection.Assembly]::LoadFrom($path)

[System.Reflection.Assembly]::LoadFrom("C:\Core\System.ComponentModel.Annotations.dll")
[System.Reflection.Assembly]::LoadFrom("C:\Core\Core.dll")



# Exemplo: instanciar uma classe da DLL (se não for estática)
$device = New-Object Core.Entities.Device

$device