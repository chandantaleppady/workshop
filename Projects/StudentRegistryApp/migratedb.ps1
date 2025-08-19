dotnet tool install --global dotnet-ef
while (-not (dotnet tool list --global | Select-String 'dotnet-ef')) {
    Start-Sleep -Seconds 2
}
Write-Host "dotnet-ef tool installed successfully."

Set-Location -Path "./StudentRegistryApp"
dotnet ef migrations add InitialCreate
dotnet ef database update