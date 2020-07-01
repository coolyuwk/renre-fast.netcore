Write-host "$(get-date) building...."
set-location $PSScriptRoot
dotnet build 
dotnet pack -o ./bin/pack
set-location ./bin/pack
dotnet nuget push *.nupkg -k Au7NhjXCFcd4 -s http://47.112.156.106:8003/
remove-item ./* -Recurse