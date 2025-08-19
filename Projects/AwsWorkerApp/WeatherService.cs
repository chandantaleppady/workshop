using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace AwsWorkerApp;

public record GeoResponse(GeoResult[] results);
public record GeoResult(double latitude, double longitude);
public record WeatherResponse(CurrentWeather current_weather);
public record CurrentWeather(double temperature, double windspeed);

public interface IWeatherService
{
    Task<WeatherResponse?> GetWeatherForecastAsync(string location);
}

public class WeatherService : IWeatherService
{
    private readonly ILogger<WeatherService> _logger;
    public WeatherService(ILogger<WeatherService> logger)
    {
        _logger = logger;
    }

    public async Task<WeatherResponse?> GetWeatherForecastAsync(string location)
    {
        WeatherResponse? currentWeather = null;
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
            using var httpClient = new HttpClient();
            string url = $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(location)}";
            var geoResponse = await httpClient.GetFromJsonAsync<GeoResponse>(url);
            if (geoResponse?.results == null || geoResponse.results.Length == 0)
            {
                _logger.LogError("Location not found: {Location}", location);
                return currentWeather;
            }
            var firstResult = geoResponse.results[0];
            string weatherUrl = $"https://api.open-meteo.com/v1/forecast?latitude={firstResult.latitude}&longitude={firstResult.longitude}&current_weather=true";
            currentWeather = await httpClient.GetFromJsonAsync<WeatherResponse>(weatherUrl);
            if (currentWeather?.current_weather == null)
            {
                _logger.LogError("Weather data not found for location: {Location}", location);
                return currentWeather;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching weather for {Location}", location);
        }
        return currentWeather;
    }
}
