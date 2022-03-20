using ChatBotStocksQuotes.Core.Implementations;
using ChatBotStocksQuotes.Core.Interfaces;
using ChatBotStocksQuotes.Core.MessageBroker.Config;
using ChatBotStocksQuotes.Core.MessageBroker.Implementations;
using ChatBotStocksQuotes.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBotStocksQuotes.IoC
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IChatService, ChatService>();

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IChatRepository, ChatRepository>();

            return services;
        }

        public static IServiceCollection RegisterProviders(this IServiceCollection services)
        {
            services.AddSingleton<RabbitMqUow>();
            services.AddTransient<IChatProvider, ChatProvider>();
            services.AddTransient<IStockClient, StockClient>();

            return services;
        }

        public static IServiceCollection RegisterEnviromentConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetSection("RabbitMqConfig").Get<RabbitMqConfig>());

            return services;
        }
    }
}
