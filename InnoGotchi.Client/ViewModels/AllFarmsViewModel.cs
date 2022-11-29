using AutoMapper;
using InnoGotchi.ApiClient.Clients;
using InnoGotchi.Client.Models;
using System.Collections.ObjectModel;
using System.Linq;


namespace InnoGotchi.Client.ViewModels
{
    public class AllFarmsViewModel : ViewModelBase
    {
        public delegate void UserSelected(UserFarmModel collaborator);
        public static event UserSelected OnUserSelected;


        private readonly FarmClient client;
        private readonly IMapper mapper;
        private ObservableCollection<UserFarmModel> userFarms;
        public ObservableCollection<UserFarmModel> UserFarms
        {
            get => userFarms;
            set
            {
                userFarms = value;

                OnPropertyChanged();
            }
        }
        private UserFarmModel selectedUser;

        public UserFarmModel SelectedUser
        {
            get => selectedUser;
            set
            {
                OnUserSelected.Invoke(value);
                selectedUser = null;
                OnPropertyChanged();
            }
        }

        public AllFarmsViewModel(FarmClient client,IMapper mapper)
        {
            GameViewModel.OnAllFarmEntered += GameViewModel_OnAllFarmEntered;
            this.client = client;
            this.mapper = mapper;
        }

        private async void GameViewModel_OnAllFarmEntered()
        {
            var farms = await client.GetAllFarms(AccessToken.Token);
            UserFarms = new ObservableCollection<UserFarmModel>(farms.UserFarmsVm
            .Select(collab => mapper.Map<UserFarmModel>(collab)));
        }
    }
}
