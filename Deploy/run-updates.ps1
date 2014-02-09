param (
	[string] $server = (Read-Host -Prompt "Input server name"),
	[string] $mainDbPrefix = (Read-Host -Prompt "Input prefix of main database"),
	[string] $filesDbPrefix = (Read-Host -Prompt "Input prefix of files database"),
	[string] $newVersion = (Read-Host -Prompt "Input version")
)

Sqlcmd -S $server -i "update-1.sql" -v MainDbName = "$mainDbPrefix`_$newVersion" FilesDbName = "$filesDbPrefix`_$newVersion"
Sqlcmd -S $server -i "update-2.sql" -v MainDbName = "$mainDbPrefix`_$newVersion" FilesDbName = "$filesDbPrefix`_$newVersion"

Write-Host "Done"