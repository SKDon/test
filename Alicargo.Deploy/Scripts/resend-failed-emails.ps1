param (
	[string] $server = (Read-Host -Prompt "Input server name"),
	[string] $dbName = (Read-Host -Prompt "Input name of main database")
)

Sqlcmd -S $server -i ".\Scripts\resend-failed-emails.sql" -v MainDbName = "$dbName"