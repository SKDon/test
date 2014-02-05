param (
    [string]$server = (Read-Host -Prompt "Input server name"),
	[string]$backupLocation = (Read-Host -Prompt "Input backup location"),
	[string]$databaseName = (Read-Host -Prompt "Input name of database to backup")
)

if (!(Test-Path $backupLocation))
{
	New-Item -ItemType directory -Path $backupLocation
}

$oldTime = [int]10
Get-ChildItem $backupLocation -recurse -Include "*.BAK" | WHERE {($_.CreationTime -le $(Get-Date).AddDays(-$oldTime))} | Remove-Item -Force

Sqlcmd -S $server -Q "EXEC [dbo].[sp_BackupDatabase] '$databaseName', '$backupLocation'"