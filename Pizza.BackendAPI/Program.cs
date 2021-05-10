using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Pizza.BackendAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            //.WriteTo.Console()
            .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "\\LogWebsite.txt")
            .CreateLogger();
            try
            {
                Log.Information("Starting up the web");
                CreateHostBuilder(args).Build().Run();
                return;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "There was a problem starting the web");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseStartup<Startup>();
                    //.UseKestrel(x => x.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2));
                });
    }
}
