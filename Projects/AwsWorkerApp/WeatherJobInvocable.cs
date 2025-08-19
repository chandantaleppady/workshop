using Coravel.Invocable;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace AwsWorkerApp;

public class WeatherJobInvocable : IInvocable
{
    private readonly ILogger<WeatherJobInvocable> _logger;
    private readonly IConfiguration _configuration;
    private readonly IWeatherService _weatherService;

    public WeatherJobInvocable(ILogger<WeatherJobInvocable> logger, IConfiguration configuration, IWeatherService weatherService)
    {
        _logger = logger;
        _configuration = configuration;
        _weatherService = weatherService;
    }

    public async Task Invoke()
    {
        var locations = _configuration.GetSection("Worker:Locations").Get<string[]>() ?? new[] { "Bengaluru", "Nitte" };
        _logger.LogInformation("WeatherJobInvocable running at: {Time}", DateTimeOffset.Now);
        foreach (var location in locations)
        {
            var currentWeather = await _weatherService.GetWeatherForecastAsync(location);
            if (currentWeather != null && currentWeather.current_weather != null)
            {
                _logger.LogInformation("Weather for {Location}: Temperature {Temperature}°C, Windspeed {Windspeed} km/h", location, currentWeather.current_weather.temperature, currentWeather.current_weather.windspeed);
            }
            else
            {
                _logger.LogInformation("No weather data available for {Location}", location);
            }
        }
    }
}
