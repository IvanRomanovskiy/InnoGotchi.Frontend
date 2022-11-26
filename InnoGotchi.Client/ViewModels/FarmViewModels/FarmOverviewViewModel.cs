using AutoMapper;
using System.Threading.Tasks;
using InnoGotchi.ApiClient.Clients;
using System.Windows.Controls;
using InnoGotchi.Client.Views.FarmViews;
using System.Collections.ObjectModel;
using System.Linq;
using InnoGotchi.Client.Models;
using InnoGotchi.Domain;
using InnoGotchi.Client.ViewModels.PetsVewModels;
using static InnoGotchi.Client.ViewModels.PetsVewModels.CreatePetViewModel;

namespace InnoGotchi.Client.ViewModels.FarmViewModels
{
    public class FarmOverviewViewModel : ViewModelBase
    {
        public delegate void FarmDataReceived(FarmInfo info);
        public static event FarmDataReceived OnFarmDataReceived;


        private readonly FarmClient client;
        private readonly IMapper mapper;

        private Page myFarm;
        public Page MyFarm
        {
            get => myFarm;
            set
            {
                myFarm = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<CollaboratorFarmModel> collaboratorsFarms;
        public ObservableCollection<CollaboratorFarmModel> CollaboratorsFarms
        {
            get => collaboratorsFarms;
            set
            {
                collaboratorsFarms = value;
                OnPropertyChanged();
            }
        }

        public FarmOverviewViewModel(FarmClient client, IMapper mapper)
        {
            AccessToken.UserAuthorized += UserAuthorized;
            CreateFarmViewModel.OnFarmCreated += OnFarmCreated;
            CreatePetViewModel.OnPetCreated += OnPetCreated;
            this.client = client;
            this.mapper = mapper;
            MyFarm = new Page();
            collaboratorsFarms = new ObservableCollection<CollaboratorFarmModel>();
        }
        private async void UserAuthorized(string newToken)
        {
            if (newToken != "") await GetData(newToken);
        }
        private async void OnFarmCreated()
        {
            await GetData(AccessToken.Token);
        }
        private async void OnPetCreated(Pet pet)
        {
            await GetData(AccessToken.Token);
        }

        private async Task GetData(string token)
        {
            var collabs = await client.GetCollaboratiorFarms(token);
            CollaboratorsFarms = new ObservableCollection<CollaboratorFarmModel>(collabs.collaboratorFarmsVm.
                Select(collab => mapper.Map<CollaboratorFarmModel>(collab)));

            var info = await client.GetFarmInfo(token);
            if (info == null)
            {
                MyFarm = new CreateFarm();
                return;
            }
            MyFarm = new FarmDetails();
            OnFarmDataReceived.Invoke(info);

        }
    }
}
