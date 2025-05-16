using System;
using System.IO;
using EntityFrameworkCore.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.Extensions.Configuration;
using Common.Constants;

namespace Buffalo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .Enrich.FromLogContext()
                .WriteTo.File(
                    Path.Combine(Directory.GetCurrentDirectory(), @"Logs\errorlogs.txt"),
                    fileSizeLimitBytes: 1_000_000,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1),
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", configuration.GetSection("Environment")["EnvironmentName"]);

            var host = CreateHostBuilder(args).Build();

            if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == EnvironmentTypes.Production 
                || Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == EnvironmentTypes.Staging)
            {
                using (var scope = host.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AlcareDbContext>();
                    context.Database.Migrate();
                    context.Dispose();
                }
            }    

            try
            {
                Log.Information("Starting web host");
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((context, configBuilder) =>
                    {
                        configBuilder.AddJsonFile("appsettings.json", false, true);
                        configBuilder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true);
                        configBuilder.AddEnvironmentVariables();
                    });
                });
    }
}
