# AWS Elastic Beanstalk .NET Core Scheduled Worker Sample

This is a sample .NET 8 console application designed to run as a scheduled background task in an AWS Elastic Beanstalk Worker Environment.

## Features
- Fetches and logs current weather for configured locations on a configurable schedule (using cron format)
- Locations and schedule are set in `appsettings.json` or via environment variable
- Logs each execution to the console
- Uses Coravel for robust cron-based scheduling
- Ready for deployment to AWS Elastic Beanstalk Worker (Linux)

## Configuration

You can configure the schedule (using cron format) and locations using:

- `appsettings.json`:
   ```json
   {
      "Worker": {
         "Cron": "*/5 * * * *", // every 5 minutes
         "Locations": [ "Bengaluru", "Nitte" ]
      }
   }
   ```
- Or set the environment variables:
   - `Worker__Cron` (for schedule, e.g. `0 9 * * *` for 9am UTC daily)
   - `Worker__Locations__0`, `Worker__Locations__1`, ... (for locations array)

## Deployment Steps

1. **Build and Publish**
   
   Open a terminal in the `AwsWorkerApp` directory and run:
   
   ```powershell
   dotnet publish -c Release -o publish
   ```

2. **Prepare Deployment Package**
   
   Copy the `Procfile` into the `publish` directory:
   
   ```powershell
   Copy-Item .\Procfile .\publish\
   ```
   
   Zip the contents of the `publish` directory:
   
   ```powershell
   Compress-Archive -Path .\publish\* -DestinationPath AwsWorkerApp.zip
   ```

3. **Deploy to AWS Elastic Beanstalk**
   
   - Go to the AWS Console > Elastic Beanstalk
   - Create a new **Worker Environment** (choose .NET on Linux platform)
   - Upload `AwsWorkerApp.zip` as your application version
   - (Optional) Set the environment variable `Worker__Cron` to override the schedule

4. **Monitor Logs**
   
   - Use the AWS Console to view logs and verify scheduled task execution

## Notes
- You can customize the scheduled logic in `Program.cs`
- Weather data is fetched from the Open-Meteo public API (no API key required)
