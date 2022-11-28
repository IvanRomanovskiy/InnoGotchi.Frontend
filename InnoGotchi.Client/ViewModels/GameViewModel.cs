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
        public delegate void GetUserDataHandler(UserDataModel userData);
        public static event GetUserDataHandler GetUserData;
        public delegate void UserLoggedOut();
        public static event UserLoggedOut OnLoggedOut;


        private Page farm;
        private Page account;
        private Page farmPets;
        private Page statistic;
        private Page petDetails;
        private Page foreignFarmPets;

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
            foreignFarmPets = new ForeignFarmPets();
            CurrentPage = farm;
        }
        public void InitEvents()
        {
            MainViewModel.OnLoggedIn += MainViewModel_OnLoggedIn;


            ChangeNameViewModel.UserNameChanged += UserNameChanged;
            ChangeAvatarViewModel.AvatarChanged += AvatarChanged;
            CreatePetViewModel.OnPetCreated += OnPetCreated;
            FarmDetailsViewModel.OnPetButtonPressed += OnPetMenuButtonPressed;
            FarmDetailsViewModel.OnStatButtonPressed += OnStatisticButtonPressed;     
            PetDetailsViewModel.OnBackPressed += PetDetailsViewModel_OnBackPressed;
            FarmOverviewViewModel.OnCollaboratorSelected += OnCollaboratorSelected;

            OwnFarmPetsViewModel.OnCreatePetClicked += OnCreatePetClicked;
            OwnFarmPetsViewModel.OnPetSelected += OnOwnPetSelected;
            ForeignFarmPetsViewModel.OnPetSelected += OnForeignPetSelected;

        }

        private async void MainViewModel_OnLoggedIn(string token)
        {
                var data = mapper.Map<UserDataModel>(await client.GetUserData(token));

                userData.FirstName = data.FirstName;
                userData.LastName = data.LastName;
                Avatar = data.Avatar;
                userData.Email = data.Email;
                FullName = $"{data.FirstName} {data.LastName}";
                CurrentPage = farm;
        }

        private void OnCollaboratorSelected(CollaboratorFarmModel collaborator)
        {
            CurrentPage = foreignFarmPets;
        }

        private void PetDetailsViewModel_OnBackPressed()
        {
            CurrentPage = lastPage;
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
        private void OnOwnPetSelected(Pet pet)
        {
            lastPage = CurrentPage;
            CurrentPage = petDetails;
        }
        private void OnForeignPetSelected(Pet pet, bool canActivity)
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
                OnLoggedOut.Invoke();
            });
        }
    }
}
