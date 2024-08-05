using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Refit;
using IExchangeRateService = SampleApp.Application.Interfaces.IExchangeRateService;
using SampleApp.Infrastructure.Interfaces;

namespace SampleApp.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IExchangeRateService, ExchangeRateService>();

            //TODO: URI can configure in app setting file and can be different for different environment
            services.AddRefitClient<IExchangeRate>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://open.er-api.com/"));
            
            return services;
        }
    }
}
