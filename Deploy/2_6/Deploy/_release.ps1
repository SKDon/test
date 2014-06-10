$ErrorActionPreference = "Stop"

.\server-release.ps1 `
	"C:\inetpub\wwwroot\Alicargo_2_6\" `
	2_6 `
	.\SQLEXPRESS `
	"`"C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\`"" `
	"Alicargo" `
	"Alicargo_Files" `
	2_5 `
	2_6

Write-Host "Done."