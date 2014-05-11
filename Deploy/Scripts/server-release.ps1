param (
	[string] $newFolder = (Read-Host -Prompt "Enter folder path for new site"),
	[string] $branch = (Read-Host -Prompt "Enter a branch"),
	
	[string] $server = (Read-Host -Prompt "Input server name"),
	[string] $DbFolder = (Read-Host -Prompt "Db folder"),
	[string] $MainDbPath = (Read-Host -Prompt "Main Db path"),
	[string] $FilesDbPath = (Read-Host -Prompt "Files Db path"),

	[string] $mainDbPrefix = (Read-Host -Prompt "Input prefix of main database"),
	[string] $filesDbPrefix = (Read-Host -Prompt "Input prefix of files database"),
	[string] $oldVersion = (Read-Host -Prompt "Input old version"),
	[string] $newVersion = (Read-Host -Prompt "Input new version")
)

clear

git clone http://git.alicargo.ru/Deploy.git $newFolder -b $branch
Write-Host "Repository has been cloned..."

Write-Host "Restoring db..."
.\restore-previous.ps1 $server $DbFolder $MainDbPath $FilesDbPath $mainDbPrefix $filesDbPrefix $oldVersion $newVersion

Write-Host "Updating..."
.\update-db.ps1 $server $mainDbPrefix $filesDbPrefix $newVersion