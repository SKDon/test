param (
    [string]$server
)

Sqlcmd -S $server -i "backup-db.sql"