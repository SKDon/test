param (
    [string] $server = (Read-Host -Prompt "Input server name"),
	[string] $backupLocation = (Read-Host -Prompt "Input backup location"),

	[string] $mainDbPrefix = (Read-Host -Prompt "Input prefix of main database"),
	[string] $filesDbPrefix = (Read-Host -Prompt "Input prefix of files database"),

    [string] $dataFolder = (Read-Host -Prompt "Folder to store databases"),

	[string] $oldVersion = (Read-Host -Prompt "Input old version"),
	[string] $newVersion = (Read-Host -Prompt "Input new version")
)

.\Scripts\backup-db.ps1 $server $backupLocation "$mainDbPrefix`_$oldVersion"
.\Scripts\backup-db.ps1 $server $backupLocation "$filesDbPrefix`_$oldVersion"
Write-Host "DBs have been backuped..."

$mainDbBackup = Get-ChildItem "$mainDbPrefix`_$oldVersion*.bak" -Path $backupLocation | Sort-Object LastAccessTime -Descending | Select-Object -First 1
$filesDbBackup = Get-ChildItem "$filesDbPrefix`_$oldVersion*.bak" -Path $backupLocation | Sort-Object LastAccessTime -Descending | Select-Object -First 1

Sqlcmd -S $server -Q "EXEC [dbo].[sp_RestoreDatabase] '$mainDbPrefix`_$newVersion', '$mainDbPrefix`_$oldVersion', '$backupLocation$mainDbBackup', '$dataFolder'"
Sqlcmd -S $server -Q "EXEC [dbo].[sp_RestoreDatabase] '$filesDbPrefix`_$newVersion', '$filesDbPrefix`_$oldVersion', '$backupLocation$filesDbBackup', '$dataFolder'"
Write-Host "DBs have been created..."