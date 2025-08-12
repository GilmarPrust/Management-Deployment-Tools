@echo off
cls

REM Powershell -ExecutionPolicy Bypass -Command "&{ start-process PowerShell -ArgumentList '-ExecutionPolicy Bypass -File %~dp0Deploy-Image.ps1' -WindowStyle Maximized -Verb RunAs}"

Powershell -ExecutionPolicy Bypass -Command "&{ start-process .\Tools\PowerShell\pwsh.exe -ArgumentList '-ExecutionPolicy Bypass -File %~dp0Deploy-System.ps1' -WindowStyle Maximized -Verb RunAs}"

