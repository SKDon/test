param (
	[string] $server = (Read-Host -Prompt "Input server name"),
	[string] $DbFolder = (Read-Host -Prompt "Db folder"),
	[string] $MainDbPath = (Read-Host -Prompt "Main Db path"),
	[string] $FilesDbPath = (Read-Host -Prompt "Files Db path"),

	[string] $mainDbPrefix = (Read-Host -Prompt "Input prefix of main database"),
	[string] $filesDbPrefix = (Read-Host -Prompt "Input prefix of files database"),
	[string] $oldVersion = (Read-Host -Prompt "Input old version"),
	[string] $newVersion = (Read-Host -Prompt "Input new version")
)

Sqlcmd -S $server -i "restore-previous.sql" -v DbFolder = "$DbFolder" `
		MainDbNew = "$mainDbPrefix`_$newVersion" MainDbOld = "$mainDbPrefix`_$oldVersion" MainDbPath = "$MainDbPath" `
		FilesDbNew = "$filesDbPrefix`_$newVersion" FilesDbOld = "$filesDbPrefix`_$oldVersion" FilesDbPath = "$FilesDbPath"