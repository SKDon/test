$ErrorActionPreference = "Stop"

.\_server-release.ps1 `
	C:\inetpub\wwwroot\Alicargo_2_7\ `
	2_7 `
	.\SQLEXPRESS `
	"`"C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\`"" `
	C:\AlicargoDbBackups\ `
	Alicargo `
	Alicargo_Files `
	2_6 `
	2_7

Write-Host "Done."