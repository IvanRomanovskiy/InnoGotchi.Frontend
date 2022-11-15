using InnoGotchi.ApiClient;
using InnoGotchi.ApiClient.Mappings;
using InnoGotchi.Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InnoGotchi.Client
{
    public class ViewModelLocator
    {
        private static ServiceProvider provider;



        public static void Init()
        {
            var services = new ServiceCollection();



            services.AddSingleton<MainViewModel>();
            services.AddSingleton<WelcomeViewModel>();
            services.AddSingleton<RegisterViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<GameViewModel>();


            services.AddApiClient("https://localhost:7100/api/");
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            });
            


            provider = services.BuildServiceProvider();
            foreach (var item in services)
            {
                if (!item.ServiceType.ContainsGenericParameters)
                {
                    provider.GetService(item.ServiceType);
                }
            }

        }
        public WelcomeViewModel WelcomeViewModel => provider.GetRequiredService<WelcomeViewModel>();
        public MainViewModel MainViewModel => provider.GetRequiredService<MainViewModel>();
        public LoginViewModel LoginViewModel => provider.GetRequiredService<LoginViewModel>();
        public RegisterViewModel RegisterViewModel => provider.GetRequiredService<RegisterViewModel>();
        public GameViewModel GameViewModel => provider.GetRequiredService<GameViewModel>();
    }
}
