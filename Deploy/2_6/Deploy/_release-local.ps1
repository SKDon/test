$ErrorActionPreference = "Stop"

.\server-release.ps1 `
	"A:\inetpub\wwwroot\Alicargo_2_6\" `
	2_6 `
	.\SQLEXPRESS `
	"`"A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\`"" `
	"Alicargo" `
	"Alicargo_Files" `
	2_5 `
	2_6

Write-Host "Done."