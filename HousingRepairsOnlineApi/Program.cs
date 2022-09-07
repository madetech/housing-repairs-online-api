using System;
using HousingRepairsOnlineApi.Data;
using HousingRepairsOnlineApi.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HousingRepairsOnlineApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseSentry(o =>
                    //{
                    //o.Dsn = Environment.GetEnvironmentVariable("SENTRY_DNS");
                    //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    //if (environment == Environments.Development)
                    //{
                    //o.Debug = true;
                    //o.TracesSampleRate = 1.0;
                    //}
                    //});
                    webBuilder.UseStartup<Startup>();
                });

        // TODO: Need to do this with migrations instead
        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<RepairContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
    }
}