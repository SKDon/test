Param(
	[string] $Configuration = (read-host "Enter a build configuration"),
	[string] $branch = (read-host "Enter a branch")
)

$msbuild = $env:systemroot + "\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe"
$proj = "A:\Projects\Alicargo\Alicargo\Alicargo.csproj"
$publishUrl = "A:\Projects\Alicargo.Release"

cd $publishUrl

git checkout $branch

remove-item "$publishUrl\*" -exclude ".git" -recurse

& $msbuild $proj /t:WebPublish /p:Configuration=$Configuration /p:VisualStudioVersion=12.0 /p:WebPublishMethod=FileSystem /p:publishUrl=$publishUrl /verbosity:m

$date = Get-Date

#git pull --rebase

#git commit --all -m "Deploy at $date" --quiet

#git push push --recurse-submodules=check --progress "Alicargo.Deploy" Dev:Dev --quiet