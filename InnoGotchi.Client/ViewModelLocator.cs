﻿using InnoGotchi.ApiClient;
using InnoGotchi.ApiClient.Mappings;
using InnoGotchi.Client.Models;
using InnoGotchi.Client.ViewModels;
using InnoGotchi.Client.ViewModels.AccountViewModels;
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
            services.AddTransient<WelcomeViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<LoginViewModel>();


            services.AddApiClient("https://localhost:7100/api/");
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            });

            services.AddScoped<GameViewModel>();
            services.AddTransient<AccountViewModel>();
            services.AddTransient<ChangeNameViewModel>();
            services.AddTransient<ChangeAvatarViewModel>();
            services.AddTransient<ChangePasswordViewModel>();

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
    }
}
