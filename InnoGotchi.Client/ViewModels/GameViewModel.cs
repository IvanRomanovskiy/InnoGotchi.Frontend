using AutoMapper;
using InnoGotchi.ApiClient.Clients;
using InnoGotchi.Client.Models;
using InnoGotchi.Client.ViewModels.AccountViewModels;
using InnoGotchi.Client.Views;
using InnoGotchi.Domain;
using InnoGotchi.Frontend.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static InnoGotchi.Client.ViewModels.AccountViewModels.ChangeNameViewModel;

namespace InnoGotchi.Client.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private Page farm;
        private Page friends;
        private Page account;
        private Page currentPage;
        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                OnPropertyChanged();
            }
        }

        private UserDataModel userData;

        private string fullName;
        public string FullName
        {
            get => fullName;
            set
            {
                fullName = value; 
                OnPropertyChanged();
            }

        }
        public BitmapFrame Avatar
        {
            get 
            {
               return userData.Avatar;
            }
            set
            {
                userData.Avatar = value;
                OnPropertyChanged();
            }
        }

        public delegate void GetUserDataHandler(UserDataModel userData);
        public static event GetUserDataHandler GetUserData;


        private readonly AccountClient client;
        private readonly IMapper mapper;
        public GameViewModel(AccountClient client, IMapper mapper)
        {
            AccessToken.UserAuthorized += UserAuthorized;
            ChangeNameViewModel.UserNameChanged += UserNameChanged;
            ChangeAvatarViewModel.AvatarChanged += AvatarChanged;

            this.client = client;
            this.mapper = mapper;
            userData = new UserDataModel();
            farm = new Farm();
            friends = new Friends();
            account = new Account();
            CurrentPage = farm;
        }

        private async void UserAuthorized(string newToken)
        {
            if(newToken != "")
            {
                var data = mapper.Map<UserDataModel>(await client.GetUserData(newToken));

                userData.FirstName = data.FirstName;
                userData.LastName = data.LastName;
                Avatar = data.Avatar;
                userData.Email = data.Email;
                FullName = $"{data.FirstName} {data.LastName}";
            }
        }
        private void UserNameChanged(string firstName, string lastname)
        {
            FullName = $"{firstName} {lastname}";
        }
        private void AvatarChanged(BitmapFrame avatar)
        {
            Avatar = avatar;
        }

        public ICommand ButtonFarm_Click
        {
            get => new RelayCommand((obj) =>
            {
                CurrentPage = farm;
            });
        }
        public ICommand ButtonAccount_Click
        {
            get => new RelayCommand((obj) =>
                {
                    CurrentPage = account;
                    GetUserData.Invoke(userData);
                });
        }
        public ICommand ButtonLogout_Click
        {
            get => new RelayCommand((obj) =>
            {
                CurrentPage = new Page();
                farm = new Farm();
                friends = new Friends();
                account = new Account();
                AccessToken.Token = "";
            });
        }

    }
}
