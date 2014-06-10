param (
	[string] $server = (Read-Host -Prompt "Input server name"),
	[string] $mainDbPrefix = (Read-Host -Prompt "Input prefix of main database"),
	[string] $version = (Read-Host -Prompt "Input version")
)

Sqlcmd -S $server -i ".\Scripts\resend-failed-emails.sql" -v MainDbName = "$mainDbPrefix`_$version"