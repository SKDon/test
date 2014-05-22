.\Scripts\server-release.ps1 `
	"C:\inetpub\wwwroot\Alicargo_Dev\" `
	Dev `
	.\SQLEXPRESS `
	"`"C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\`"" `
	"`"C:\AlicargoDbBackups\Alicargo_2_4.bak`"" `
	"`"C:\AlicargoDbBackups\Alicargo_Files_2_4.bak`"" `
	"Alicargo" `
	"Alicargo_Files" `
	2_4 `
	Dev

Write-Host "Done."