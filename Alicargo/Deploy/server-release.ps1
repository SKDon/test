param (
    [string] $server = (Read-Host -Prompt "Input server name"),
	[string] $backupLocation = (Read-Host -Prompt "Input backup location"),

	[string] $mainDbPrefix = (Read-Host -Prompt "Input prefix of main database"),
	[string] $filesDbPrefix = (Read-Host -Prompt "Input prefix of files database"),

    [string] $dataFolder = (Read-Host -Prompt "Folder to store databases"),

	[string] $oldVersion = (Read-Host -Prompt "Input old version"),
	[string] $newVersion = (Read-Host -Prompt "Input new version"),

	[string] $newFolder = (Read-Host -Prompt "Enter folder path for new site"),
	[string] $branch = (Read-Host -Prompt "Enter a branch"),
    [string] $poolName = (Read-Host -Prompt "Enter a pool name")
)

clear

git clone http://git.alicargo.ru/Deploy.git $newFolder -b $branch
Write-Host "Repository has been cloned..."

cd "$newFolder`Deploy\"

.\Scripts\prepare-release-db.ps1 $server $backupLocation $mainDbPrefix $filesDbPrefix $dataFolder $oldVersion $newVersion $poolName

.\server-update.ps1