using Serilog;
using System.Net.Http.Json;
using System.Net;

namespace WeatherCheck
{
    internal class WeatherForecast
    {
        public async Task<WeatherResponse> GetWeatherForcast(string location)
        {
            WeatherResponse? currentWeather = null;
            try
            {
                // Ensure TLS 1.2 or higher is used
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

                using var httpClient = new HttpClient();
                // Use Open-Meteo API for demonstration (no API key required)
                string url = $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(location)}";
                var geoResponse = await httpClient.GetFromJsonAsync<GeoResponse>(url);
                if (geoResponse?.results == null || geoResponse.results.Length == 0)
                {
                    Log.Error("Location not found: {Location}", location);
                    return currentWeather;
                }
                var firstResult = geoResponse.results[0];
                string weatherUrl = $"https://api.open-meteo.com/v1/forecast?latitude={firstResult.latitude}&longitude={firstResult.longitude}&current_weather=true";
                currentWeather = await httpClient.GetFromJsonAsync<WeatherResponse>(weatherUrl);
                if (currentWeather?.current_weather == null)
                {
                    Log.Error("Weather data not found for location: {Location}", location);
                    return currentWeather;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching weather for {Location}", location);
            }

            return currentWeather;
        }

        // Models for API responses
        public record GeoResponse(GeoResult[] results);
        public record GeoResult(double latitude, double longitude);
        public record WeatherResponse(CurrentWeather current_weather);
        public record CurrentWeather(double temperature, double windspeed);

    }
}
