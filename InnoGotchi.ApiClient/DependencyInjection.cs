using InnoGotchi.ApiClient.Clients;

using InnoGotchi.Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace InnoGotchi.ApiClient
{
    public static class DependencyInjection
    {
        private static Token token;
        private static Token Token
        {
            get { return token; }
            set { token = value; }
        }

        public static IServiceCollection AddApiClient(this IServiceCollection services, string baseAddress)
        {
            services.AddTransient<HttpContextMiddleware>();

            var WTClientFactory = services.AddHttpClient<ClientWithoutToken>(config =>
            {
                config.BaseAddress = new Uri(baseAddress);
                config.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
                .AddHttpMessageHandler<HttpContextMiddleware>();

            services.AddSingleton<Token>(ser => {
                return Token;
            });

            services.AddHttpClient<AccountClient>(config =>
            {
                config.BaseAddress = new Uri(baseAddress + "Account/");               
            })
                .AddHttpMessageHandler<HttpContextMiddleware>();

            return services;
        }
    }
}
