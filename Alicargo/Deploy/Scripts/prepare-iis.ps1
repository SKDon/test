param (
	[string] $oldVersion = (Read-Host -Prompt "Input old version"),
	[string] $newVersion = (Read-Host -Prompt "Input new version"),	
	[string] $newFolder = (Read-Host -Prompt "Enter folder path for new site"),
    [string] $poolName = (Read-Host -Prompt "Enter a pool name")
)

Remove-Item IIS:\Sites\Alicargo_$oldVersion

New-Item iis:\Sites\Alicargo_$newVersion `
	-bindings @{protocol="http";bindingInformation=":80:Alicargo_$newVersion"} `
	-physicalPath $newFolder