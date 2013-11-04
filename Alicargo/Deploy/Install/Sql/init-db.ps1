param (
    [string]$server = (Read-Host -Prompt "Input server name")
)

Sqlcmd -S $server -i "Master/create-sp-for-backup-db.sql"

Sqlcmd -S $server -i "Master/create-sp-restore-db.sql"

Sqlcmd -S $server -i "Alicargo/create-db_1_1.sql" -v DatabaseName="Alicargo"

Sqlcmd -S $server -i "Alicargo/1_1_To_2_0.sql" -v DatabaseName="Alicargo"

Sqlcmd -S $server -i "Alicargo/create-files-db.sql" -v DatabaseName="Alicargo_Files"

Sqlcmd -S $server -i "Alicargo/set-initial-data.sql"