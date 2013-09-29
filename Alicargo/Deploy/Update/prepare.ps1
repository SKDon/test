Param(
	[string] $Configuration = (read-host "Enter build configuration")
)

$msbuild = $env:systemroot + "\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe"
$proj = "D:\S\Projects\Alicargo\Alicargo\Alicargo.csproj"
$publishUrl = "D:\S\Projects\Alicargo.Release"

remove-item "$publishUrl\*" -exclude ".git" -recurse

& $msbuild $proj /t:WebPublish /p:Configuration=$Configuration /p:VisualStudioVersion=11.0 /p:WebPublishMethod=FileSystem /p:publishUrl=$publishUrl /verbosity:m