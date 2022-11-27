using AutoMapper;
using InnoGotchi.ApiClient.Clients;
using InnoGotchi.Client.Models;
using InnoGotchi.Client.ViewModels.AccountViewModels;
using InnoGotchi.Client.ViewModels.FarmViewModels;
using InnoGotchi.Client.ViewModels.PetsVewModels;
using InnoGotchi.Client.Views;
using InnoGotchi.Client.Views.FarmViews;
using InnoGotchi.Client.Views.PetViews;
using InnoGotchi.Domain;
using InnoGotchi.Frontend.Utilities;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace InnoGotchi.Client.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private Page farm;
        private Page account;
        private Page farmPets;
        private Page statistic;
        private Page petDetails;

        private Page lastPage;

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
            InitEvents();

            this.client = client;
            this.mapper = mapper;
            userData = new UserDataModel();
            farm = new FarmOverview();
            account = new Account();
            farmPets = new FarmPets();
            statistic = new FarmStatistic();
            petDetails = new PetDetails();
            CurrentPage = farm;
        }
        public void InitEvents()
        {
            AccessToken.UserAuthorized += UserAuthorized;
            ChangeNameViewModel.UserNameChanged += UserNameChanged;
            ChangeAvatarViewModel.AvatarChanged += AvatarChanged;
            FarmPetsViewModel.OnCreatePetClicked += OnCreatePetClicked;
            FarmViewModel.OnMyPetsClicked += OnMyPetsClicked;
            CreatePetViewModel.OnPetCreated += OnPetCreated;
            FarmDetailsViewModel.OnPetButtonPressed += OnPetMenuButtonPressed;
            FarmDetailsViewModel.OnStatButtonPressed += OnStatisticButtonPressed;
            FarmPetsViewModel.OnPetSelected += OnPetSelected;
            PetDetailsViewModel.OnBackPressed += PetDetailsViewModel_OnBackPressed;
        }

        private void PetDetailsViewModel_OnBackPressed()
        {
            CurrentPage = lastPage;
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
        private void OnCreatePetClicked()
        {
            CurrentPage = new CreatePet();
        }
        private void OnMyPetsClicked()
        {
            CurrentPage = farmPets;
        }
        private void OnPetCreated(Pet pet)
        {
            CurrentPage = farmPets;
        }
        private void OnPetMenuButtonPressed()
        {
            CurrentPage = farmPets;
        }
        private void OnStatisticButtonPressed()
        {
            CurrentPage = statistic;
        }
        private void OnPetSelected(Pet pet)
        {
            lastPage = CurrentPage;
            CurrentPage = petDetails;
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
                AccessToken.Token = "";
            });
        }
    }
}
