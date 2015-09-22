param (
	[string] $server = (Read-Host -Prompt "Input server name"),
	[string] $dbName = (Read-Host -Prompt "Input name of main database"),
	[string] $sqlPath = (Read-Host -Prompt "Input path to sql script")
)

Sqlcmd -S $server -i "$sqlPath" -v MainDbName = "$dbName"