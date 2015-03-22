$ErrorActionPreference = "Stop"

.\_server-release.ps1 `
	A:\inetpub\wwwroot\Alicargo_Release\ `
	2_7 `
	.\SQLEXPRESS `
	"`"A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\`"" `
	A:\Temp\ `
	Alicargo `
	Alicargo_Files `
	2_6 `
	2_7

Write-Host "Done."