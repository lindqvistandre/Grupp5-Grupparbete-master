using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealtyFirmAPI.Data;
using RealtyFirmAPI.Models;
using Serilog;
using System;
using System.IO;

namespace RealtyFirmAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Helpers.SimpleLogger.Log("Starting Service");

            string json = File.ReadAllText(@"appsettings.json");
            JObject o = JObject.Parse(@json);
            AppSettings.appSettings = JsonConvert.DeserializeObject<AppSettings>(o["AppSettings"].ToString()); //H�mtar info fr�n
                                                                                                               //appsettings.json och bygger ett objekt utifr�n Appsettings.cs

            Helpers.SimpleLogger.Log(Models.AppSettings.appSettings.JwtSecret);

            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    RealtyDbContext _context = services.GetRequiredService<RealtyDbContext>();

                    DbInitializer.Initialize(_context); //Initialisera databasen om den �r tom.
                }
                catch (Exception epicFail)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(epicFail, "Fel uppstod n�r databasen skulle seedas.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
