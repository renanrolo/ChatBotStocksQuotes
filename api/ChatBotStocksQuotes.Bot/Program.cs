using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using ChatBotStocksQuotes.IoC;

namespace ChatBotStocksQuotes.Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build()
                                       .Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
                return;
            }

            Environment.Exit(0);
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext, configApp) =>
            {
                configApp.SetBasePath(Directory.GetCurrentDirectory());
                configApp.AddJsonFile("appsettings.json", optional: false);
                configApp.AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
                IConfiguration configuration = hostContext.Configuration;

                //services.AddSingleton(configuration.GetSection("ApplicationConfiguration").Get<ApplicationConfiguration>());

                services.RegisterProviders()
                        .RegisterEnviromentConfig(configuration);

                services.AddHttpClient();

                services.AddHostedService<Worker>();
            });
    }
}
