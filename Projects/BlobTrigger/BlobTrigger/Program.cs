using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Extensions.Logging; // Add this using directive

var builder = FunctionsApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// Fix: Register Serilog as the logging provider for the host
builder.Services.AddSingleton<Microsoft.Extensions.Logging.ILoggerProvider>(sp => new SerilogLoggerProvider(Log.Logger, dispose: false));

// The rest of your configuration
builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
