Write-host "$(get-date) building...."
set-location $PSScriptRoot
dotnet build 
dotnet pack -o ./bin/pack
set-location ./bin/pack
dotnet nuget push *.nupkg -k oy2gqky4azbfofqxgwja53rre4p5jquerenr6zicb2kvlq -s  https://api.nuget.org/v3/index.json
remove-item ./* -Recurse