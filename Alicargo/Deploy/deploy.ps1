Param(
	[string] $Configuration = (read-host "Enter a build configuration"),
	[string] $branch = (read-host "Enter a branch")
)

$msbuild = $env:systemroot + "\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe"
$proj = "D:\S\Projects\Alicargo\Alicargo\Alicargo.csproj"
$publishUrl = "D:\S\Projects\Alicargo.Release"

cd $publishUrl

git checkout $branch

remove-item "$publishUrl\*" -exclude ".git" -recurse

& $msbuild $proj /t:WebPublish /p:Configuration=$Configuration /p:VisualStudioVersion=11.0 /p:WebPublishMethod=FileSystem /p:publishUrl=$publishUrl /verbosity:m

$date = Get-Date

git commit -a -m "Deploy at $date" -q

git push -a -q