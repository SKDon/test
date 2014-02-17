Param(
	[string] $Configuration = (read-host "Enter a build configuration"),
	[string] $branch = (read-host "Enter a branch")
)

$msbuild = $env:systemroot + "\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe"
$sourcePath = "A:\Projects\Alicargo\Alicargo"
$proj = "$sourcePath\Alicargo.csproj"
$publishUrl = "A:\Projects\Alicargo.Release"

clear

cd $publishUrl

git checkout --force -B $branch

#git pull --rebase

remove-item "$publishUrl\*" -exclude ".git" -recurse

& $msbuild $proj /t:WebPublish /p:Configuration=$Configuration /p:VisualStudioVersion=12.0 /p:WebPublishMethod=FileSystem /p:publishUrl=$publishUrl /verbosity:m

$date = Get-Date

git add --all

git commit --all -m "Deploy at $date" --quiet

git push -f --all --quiet

Write-Host "Done"
cmd /c pause | out-null