Write-Host "Restoring..."
.\Scripts\restore-previous.ps1 .\SQLEXPRESS `
	"`"A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\`"" `
	"`"A:\Projects\Alicargo_2_4.bak`"" `
	"`"A:\Projects\Alicargo_Files_2_4.bak`"" `
	"Alicargo" `
	"Alicargo_Files" `
	2_4 `
	Dev

Write-Host "Updating..."
.\Scripts\update-db.ps1 .\SQLEXPRESS Alicargo Alicargo_Files Dev

Write-Host "Done."
cmd /c pause | out-null