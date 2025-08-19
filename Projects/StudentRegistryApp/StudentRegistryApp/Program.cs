using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Extensions.Logging;
using UtilsLib.Configs;
using UtilsLib.Context;
using UtilsLib.Repositories;
using UtilsLib.Tools;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// Add services to the container.
builder.Services.AddControllersWithViews();
ConfigureServices(builder.Services, configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}");

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddSingleton<Microsoft.Extensions.Logging.ILoggerProvider>(sp => new SerilogLoggerProvider(Log.Logger, dispose: false));
    services.AddDbContextPool<AppDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("EmployeeDb"), b => b.MigrationsAssembly("StudentRegistryApp"))); // Move this line here
    services.Configure<BlobConfigs>(configuration.GetSection("BlobConfigs"));
    services.AddOptions();

    services
        .AddTransient<IStudentsRepository, StudentsRepository>()
        .AddTransient<IDataUploadUtil, DataUploadUtil>()
        .AddTransient<IBlobUtils, BlobUtils>();
}
