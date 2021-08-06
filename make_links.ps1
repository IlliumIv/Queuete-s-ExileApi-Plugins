$dirs = Get-ChildItem .\Plugins\Source\
rm -fo .\Plugins\Source\*
rm -r -fo .\.git\modules\Plugins\Source\*
rm -r -fo .\modules\Plugins\Source\*

if (![System.IO.File]::Exists(".\.git\modules\Plugins\Source\")) { mkdir .\.git\modules\Plugins\Source }
if (![System.IO.File]::Exists(".\modules\Plugins\Source\")) { mkdir .\modules\Plugins\Source }

foreach($dir in $dirs) {
	$name = $dir.Name;
	cmd /c mklink /J .\Plugins\Source\$name ..\..\ExileApi\Plugins\Source\$name;
	cmd /c mklink /J .\.git\modules\Plugins\Source\$name ..\..\ExileApi\.git\modules\Plugins\Source\$name;
	cmd /c mklink /J .\modules\Plugins\Source\$name ..\..\ExileApi\.git\modules\Plugins\Source\$name;
}