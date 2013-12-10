﻿param (
    [string] $server = (Read-Host -Prompt "Input server name"),
	[string] $backupLocation = (Read-Host -Prompt "Input backup location"),

	[string] $mainDbPrefix = (Read-Host -Prompt "Input prefix of main database"),
	[string] $filesDbPrefix = (Read-Host -Prompt "Input prefix of files database"),

    [string] $dataFolder = (Read-Host -Prompt "Folder to store databases"),

	[string] $oldVersion = (Read-Host -Prompt "Input old version"),
	[string] $newVersion = (Read-Host -Prompt "Input new version"),

    [string] $poolName = (Read-Host -Prompt "Enter a pool name")
)

.\Scripts\backup-db.ps1 $server $backupLocation "$mainDbPrefix`_$oldVersion"
.\Scripts\backup-db.ps1 $server $backupLocation "$filesDbPrefix`_$oldVersion"
Write-Host "DBs have been backuped..."

$mainDbBackup = Get-ChildItem "$mainDbPrefix`_$oldVersion*.bak" -Path $backupLocation | Sort-Object LastAccessTime -Descending | Select-Object -First 1
$filesDbBackup = Get-ChildItem "$filesDbPrefix`_$oldVersion*.bak" -Path $backupLocation | Sort-Object LastAccessTime -Descending | Select-Object -First 1

Sqlcmd -S $server -Q "EXEC [dbo].[sp_RestoreDatabase] '$mainDbPrefix`_$newVersion', '$mainDbPrefix`_$oldVersion', '$backupLocation$mainDbBackup', '$dataFolder'"
Sqlcmd -S $server -Q "EXEC [dbo].[sp_RestoreDatabase] '$filesDbPrefix`_$newVersion', '$filesDbPrefix`_$oldVersion', '$backupLocation$filesDbBackup', '$dataFolder'"
Write-Host "DBs have been created..."

Sqlcmd -S $server -i ".\Scripts\update-2.sql" -v MainDbName = "$mainDbPrefix`_$newVersion" FilesDbName = "$filesDbPrefix`_$newVersion"
Sqlcmd -S $server -i ".\Scripts\update-3.sql" -v MainDbName = "$mainDbPrefix`_$newVersion" FilesDbName = "$filesDbPrefix`_$newVersion"
Sqlcmd -S $server -i ".\Scripts\setup-rights.sql" -v PoolName = "$poolName" MainDbName = "$mainDbPrefix`_$newVersion" FilesDbName = "$filesDbPrefix`_$newVersion"
Write-Host "DBs have been updated..."