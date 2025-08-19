# PowerShell script to publish and zip .NET app for AWS Elastic Beanstalk (Linux)
# Usage: Run from the project root (where the .csproj is located)

$projectName = "AwsWorkerApp"
$publishDir = "publish"
$zipName = "AwsWorkerApp.zip"

# Clean up previous publish and zip
if (Test-Path $publishDir) { Remove-Item $publishDir -Recurse -Force }
if (Test-Path $zipName) { Remove-Item $zipName -Force }


# Publish the app for Linux (no .exe, only .dll)
Write-Host "Publishing $projectName for linux-x64..."
dotnet publish -c Release -r linux-x64 --self-contained false -o $publishDir

# Copy Procfile and appsettings.json into publish directory
if (Test-Path "Procfile") { Copy-Item "Procfile" "$publishDir/Procfile" -Force }
if (Test-Path "appsettings.json") { Copy-Item "appsettings.json" "$publishDir/appsettings.json" -Force }

# Zip the contents of the publish directory (not the folder itself)
Write-Host "Zipping published output..."
Push-Location $publishDir
Compress-Archive -Path * -DestinationPath "../$zipName"
Pop-Location

Write-Host "Done! Zip file created: $zipName"
