using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Coravel;

namespace AwsWorkerApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddScheduler();
                services.AddTransient<WeatherJobInvocable>();
                services.AddSingleton<IWeatherService, WeatherService>();
                services.AddSingleton(context.Configuration);
            })
            .Build();

        // Schedule the weather job using Coravel
        var provider = host.Services;
        var config = provider.GetRequiredService<IConfiguration>();
        var cron = config.GetValue<string>("Worker:Cron", "*/5 * * * *"); // Default: every 5 minutes
        provider.UseScheduler(scheduler =>
        {
            scheduler
                .Schedule<WeatherJobInvocable>()
                .Cron(cron)
                .PreventOverlapping(nameof(WeatherJobInvocable));
        });

        await host.RunAsync();
    }
}
