# Clean up all gen files

if(Test-Path ./content/.vs)
{
    Remove-Item ./content/.vs -recurse -force
}

Get-ChildItem ./content\*\*\bin | ForEach-Object {
    Remove-Item $_.FullName -recurse -force
}

Get-ChildItem ./content\*\*\obj | ForEach-Object {
    Remove-Item $_.FullName -recurse -force
}

./nuget.exe pack  ./content/CW.ApiTpl.nuspec -Exclude ".vs" -OutputDirectory ./bin 