dotnet build
dotnet publish -c Release -o ./dist
Compress-Archive -Path ./dist/* -DestinationPath ./publish.zip