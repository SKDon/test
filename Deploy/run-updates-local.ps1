Write-Host "Restoring..."
.\restore-previous.ps1 .\SQLEXPRESS `
	"`"A:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\`"" `
	"`"A:\Projects\Alicargo_2_3.bak`"" `
	"`"A:\Projects\Alicargo_Files_2_3.bak`"" `
	"Alicargo" `
	"Alicargo_Files" `
	2_3 `
	2_3

Write-Host "Updating..."
.\run-updates.ps1 .\SQLEXPRESS Alicargo Alicargo_Files 2_3

Write-Host "Done."
cmd /c pause | out-null