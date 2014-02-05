param (
    [string]$server = (Read-Host -Prompt "Input server name")
)

Sqlcmd -S $server -i "create-sp-for-backup-db.sql"

Sqlcmd -S $server -i "create-sp-restore-db.sql"