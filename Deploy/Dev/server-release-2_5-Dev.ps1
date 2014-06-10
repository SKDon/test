.\Scripts\server-release.ps1 `
	"C:\inetpub\wwwroot\Alicargo_Dev\" `
	Dev `
	.\SQLEXPRESS `
	"`"C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\`"" `
	"`"C:\AlicargoDbBackups\Alicargo_2_5.bak`"" `
	"`"C:\AlicargoDbBackups\Alicargo_Files_2_5.bak`"" `
	"Alicargo" `
	"Alicargo_Files" `
	2_5 `
	Dev

Write-Host "Done."