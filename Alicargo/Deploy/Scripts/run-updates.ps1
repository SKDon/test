param (
	[string] $server = (Read-Host -Prompt "Input server name"),
	[string] $mainDbPrefix = (Read-Host -Prompt "Input prefix of main database"),
	[string] $filesDbPrefix = (Read-Host -Prompt "Input prefix of files database"),
	[string] $newVersion = (Read-Host -Prompt "Input new version")
)

Sqlcmd -S $server -i "update-1.sql" -v MainDbName = "$mainDbPrefix`_$newVersion" FilesDbName = "$filesDbPrefix`_$newVersion"
Sqlcmd -S $server -i "update-2.sql" -v MainDbName = "$mainDbPrefix`_$newVersion" FilesDbName = "$filesDbPrefix`_$newVersion"
Sqlcmd -S $server -i "update-4.sql" -v MainDbName = "$mainDbPrefix`_$newVersion" FilesDbName = "$filesDbPrefix`_$newVersion"
Sqlcmd -S $server -i "update-5.sql" -v MainDbName = "$mainDbPrefix`_$newVersion" FilesDbName = "$filesDbPrefix`_$newVersion"
Sqlcmd -S $server -i "update-6.sql" -v MainDbName = "$mainDbPrefix`_$newVersion" FilesDbName = "$filesDbPrefix`_$newVersion"
Sqlcmd -S $server -i "update-7.sql" -v MainDbName = "$mainDbPrefix`_$newVersion" FilesDbName = "$filesDbPrefix`_$newVersion"
Sqlcmd -S $server -i "update-8.sql" -v MainDbName = "$mainDbPrefix`_$newVersion" FilesDbName = "$filesDbPrefix`_$newVersion"
Sqlcmd -S $server -i "update-9.sql" -v MainDbName = "$mainDbPrefix`_$newVersion" FilesDbName = "$filesDbPrefix`_$newVersion"

Write-Host "Done"

cmd /c pause | out-null