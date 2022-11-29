using InnoGotchi.ApiClient.Clients;

using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace InnoGotchi.ApiClient
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiClient(this IServiceCollection services, string baseAddress)
        {
            services.AddTransient<HttpContextMiddleware>();

            var WTClientFactory = services.AddHttpClient<ClientWithoutToken>(config =>
            {
                config.BaseAddress = new Uri(baseAddress);
            })
                .AddHttpMessageHandler<HttpContextMiddleware>();

            services.AddHttpClient<AccountClient>(config =>
            {
                config.BaseAddress = new Uri(baseAddress + "Account/");               
            })
                .AddHttpMessageHandler<HttpContextMiddleware>();

            services.AddHttpClient<FarmClient>(config =>
            {
                config.BaseAddress = new Uri(baseAddress + "Farm/");
            })
                .AddHttpMessageHandler<HttpContextMiddleware>();

            services.AddHttpClient<PetClient>(config =>
            {
                config.BaseAddress = new Uri(baseAddress + "Pet/");
            })
                .AddHttpMessageHandler<HttpContextMiddleware>();

            return services;
        }
    }
}
