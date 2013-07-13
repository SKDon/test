param (
    [string]$server = (Read-Host -Prompt "Input server name")
)

Sqlcmd -S $server -i "backup-db.sql"