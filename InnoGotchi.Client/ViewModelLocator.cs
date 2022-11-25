using InnoGotchi.ApiClient;
using InnoGotchi.ApiClient.Mappings;
using InnoGotchi.Client.ViewModels;
using InnoGotchi.Client.ViewModels.AccountViewModels;
using InnoGotchi.Client.ViewModels.FarmViewModels;
using InnoGotchi.Client.ViewModels.PetsVewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InnoGotchi.Client
{
    public class ViewModelLocator
    {
        private const string API_PATH = "https://localhost:7100/api/";

        private static ServiceProvider provider;



        public static void Init()
        {
            var services = new ServiceCollection();



            services.AddSingleton<MainViewModel>();
            services.AddTransient<WelcomeViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<LoginViewModel>();


            services.AddApiClient(API_PATH);
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            });

            services.AddTransient<GameViewModel>();
            services.AddTransient<AccountViewModel>();
            services.AddTransient<ChangeNameViewModel>();
            services.AddTransient<ChangeAvatarViewModel>();
            services.AddTransient<ChangePasswordViewModel>();

            services.AddTransient<FarmOverviewViewModel>();
            services.AddTransient<CreateFarmViewModel>();
            services.AddTransient<FarmDetailsViewModel>();
            services.AddTransient<FarmStatisticViewModel>();

            services.AddTransient<FarmViewModel>();
            services.AddTransient<FarmPetsViewModel>();
            services.AddTransient<CreatePetViewModel>();


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
        public AccountViewModel AccountViewModel => provider.GetRequiredService<AccountViewModel>();
        public ChangeNameViewModel ChangeNameViewModel => provider.GetRequiredService<ChangeNameViewModel>();
        public ChangeAvatarViewModel ChangeAvatarViewModel => provider.GetRequiredService<ChangeAvatarViewModel>();
        public ChangePasswordViewModel ChangePasswordViewModel => provider.GetRequiredService<ChangePasswordViewModel>();
        public FarmViewModel FarmViewModel => provider.GetRequiredService<FarmViewModel>();
        public FarmPetsViewModel FarmPetsViewModel => provider.GetRequiredService<FarmPetsViewModel>();
        public CreatePetViewModel CreatePetViewModel => provider.GetRequiredService<CreatePetViewModel>();


        public FarmOverviewViewModel FarmOverviewViewModel => provider.GetRequiredService<FarmOverviewViewModel>();
        public CreateFarmViewModel CreateFarmViewModel => provider.GetRequiredService<CreateFarmViewModel>();
        public FarmDetailsViewModel FarmDetailsViewModel => provider.GetRequiredService<FarmDetailsViewModel>();
        public FarmStatisticViewModel FarmStatisticViewModel => provider.GetRequiredService<FarmStatisticViewModel>();
    }
}
