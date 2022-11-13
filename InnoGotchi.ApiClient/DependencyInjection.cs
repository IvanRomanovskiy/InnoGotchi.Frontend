using InnoGotchi.ApiClient.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace InnoGotchi.ApiClient
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiClient(this IServiceCollection services, Action<HttpClient> clientConfiguration)
        {
            services.AddTransient<HttpContextMiddleware>();

            services.AddHttpClient<Client>(clientConfiguration)
                .AddHttpMessageHandler<HttpContextMiddleware>();

            return services;
        }
    }
}
