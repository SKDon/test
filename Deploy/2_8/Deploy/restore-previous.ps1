param (
	[string] $server = (Read-Host -Prompt "Input server name"),
	[string] $dbFolder = (Read-Host -Prompt "Db storage path"),
	[string] $dbPath  = (Read-Host -Prompt "Db source path"),

	[string] $dbPrefix = (Read-Host -Prompt "Input prefix of the database"),
	[string] $oldVersion = (Read-Host -Prompt "Input old version"),
	[string] $newVersion = (Read-Host -Prompt "Input new version")
)

Sqlcmd -S $server -i restore-previous.sql -v DbFolder = $dbFolder `
	DbNew = $dbPrefix`_$newVersion DbOld = $dbPrefix`_$oldVersion DbPath = $dbPath