param (
    [string]$server,
	[string]$backupLocation,
	[string]$databaseName
)

if (!(Test-Path $backupLocation))
{
	New-Item -ItemType directory -Path $backupLocation
}

$oldTime = [int]7
Get-ChildItem $backupLocation -recurse -Include "*.BAK" | WHERE {($_.CreationTime -le $(Get-Date).AddDays(-$oldTime))} | Remove-Item -Force

Sqlcmd -S $server -Q "EXEC [dbo].[sp_BackupDatabases] '$databaseName', '$backupLocation'"