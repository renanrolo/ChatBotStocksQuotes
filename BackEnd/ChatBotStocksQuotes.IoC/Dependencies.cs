using ChatBotStocksQuotes.Core.Implementations;
using ChatBotStocksQuotes.Core.Interfaces;
using ChatBotStocksQuotes.Core.MessageBroker.Config;
using ChatBotStocksQuotes.Core.MessageBroker.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBotStocksQuotes.IoC
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<RabbitMqUow>();

            services.AddTransient<IChat, Chat>();
            services.AddTransient<IChatProvider, ChatProvider>();

            return services;
        }

        public static IServiceCollection RegisterEnviromentConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //Get enviroment variables from appsettings.json mapping directly to models
            services.AddSingleton(configuration.GetSection("RabbitMqConfig").Get<RabbitMqConfig>());

            return services;
        }
    }
}
