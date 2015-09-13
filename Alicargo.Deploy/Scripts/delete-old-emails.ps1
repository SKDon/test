param (
	[string] $server = (Read-Host -Prompt "Input server name"),
	[string] $dbName = (Read-Host -Prompt "Input name of main database")
)

Sqlcmd -S $server -i ".\Scripts\delete-old-emails.sql" -v MainDbName = "$dbName"