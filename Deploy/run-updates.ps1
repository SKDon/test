param (
	[string] $server = (Read-Host -Prompt "Input server name"),
	[string] $mainDbPrefix = (Read-Host -Prompt "Input prefix of main database"),
	[string] $filesDbPrefix = (Read-Host -Prompt "Input prefix of files database"),
	[string] $version = (Read-Host -Prompt "Input version")
)

Sqlcmd -S $server -i "update-1.sql" -v MainDbName = "$mainDbPrefix`_$version" FilesDbName = "$filesDbPrefix`_$version"
Sqlcmd -S $server -i "update-2.sql" -v MainDbName = "$mainDbPrefix`_$version" FilesDbName = "$filesDbPrefix`_$version"