using Serilog;
using WeatherCheck;
using static WeatherCheck.WeatherForecast;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("WeatherCheck started");

string[] locations = new[] { "Bengaluru", "Nitte" };
foreach (string location in locations)
{
    WeatherForecast util = new();
    WeatherResponse? currentWeather = await util.GetWeatherForcast(location);
    if (currentWeather != null)
    {
        Log.Information("Weather for {Location}: Temperature {Temperature}°C, Windspeed {Windspeed} km/h", location, currentWeather.current_weather.temperature, currentWeather.current_weather.windspeed);
    }
    else
    {
        Log.Information("No weather data available for {Location}", location);
    }
}

