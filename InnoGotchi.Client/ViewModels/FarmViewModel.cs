using AutoMapper;
using InnoGotchi.ApiClient.Clients;
using InnoGotchi.ApiClient.Models.Farm;
using InnoGotchi.Client.Models;
using InnoGotchi.Frontend.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InnoGotchi.Client.ViewModels
{
    public class FarmViewModel : ViewModelBase
    {
        public delegate void MyPetsClicked();
        public delegate void MyCollaboratorsPetsClicked();
        public static event MyPetsClicked OnMyPetsClicked;
        public static event MyCollaboratorsPetsClicked OnMyCollaboratorsPetsClicked;


        private bool farmGridVisibility;
        public Visibility NoFarmGrid
        {
            get => ((!farmGridVisibility) ? Visibility.Visible : Visibility.Hidden);
            set
            {
                farmGridVisibility = ((Visibility.Visible != value) ? true : false);
                OnPropertyChanged();
            }
        }
        public Visibility FarmGrid
        {
            get => ((farmGridVisibility) ? Visibility.Visible : Visibility.Hidden);
            set
            {
                farmGridVisibility = ((Visibility.Visible == value) ? true : false);
                NoFarmGrid = (Visibility.Visible == value) ? Visibility.Hidden : Visibility.Visible;
                OnPropertyChanged();
            }
        }

      
        private FarmInfoModel farmInfo;
        public string Name
        {
            get => farmInfo.Name;
            set => farmInfo.Name = value;
        }
        public string CountAlivePets
        {
            get => farmInfo.CountAlivePets;
            set
            {
                farmInfo.CountAlivePets = value;
                OnPropertyChanged();
            }
        }
        public string CountDeadPets
        {
            get => farmInfo.CountDeadPets;
            set
            {
                farmInfo.CountDeadPets = value;
                OnPropertyChanged();
            }
        }
        public string AverageFeedingPeriod
        {
            get => farmInfo.AverageFeedingPeriod;
            set
            {
                farmInfo.AverageFeedingPeriod = value;
                OnPropertyChanged();
            }
        }
        public string AverageThirstQuenchingPeriod
        {
            get => farmInfo.AverageThirstQuenchingPeriod;
            set
            {
                farmInfo.AverageThirstQuenchingPeriod = value;
                OnPropertyChanged();
            }
        }
        public string AveragePetsHappinessDaysCount
        {
            get => farmInfo.AveragePetsHappinessDaysCount;
            set
            {
                farmInfo.AveragePetsHappinessDaysCount = value;
                OnPropertyChanged();
            }
        }
        public string AveragePetsAgeCount
        {
            get => farmInfo.AveragePetsAgeCount;
            set
            {
                farmInfo.AveragePetsHappinessDaysCount = value;
                OnPropertyChanged();
            }
        }

        public class PiePoint
        {
            public string Name { get; set; }
            public int Share { get; set; }
        }

        private ObservableCollection<PiePoint> _pieCollection;
        public ObservableCollection<PiePoint> PieCollection { get { return _pieCollection; } set { _pieCollection = value; OnPropertyChanged(); } }

        private readonly FarmClient client;
        private readonly IMapper mapper;
        public FarmViewModel(FarmClient client, IMapper mapper)
        {
            AccessToken.UserAuthorized += UserAuthorized;
            this.client = client;
            this.mapper = mapper;
            farmInfo = new FarmInfoModel();
            PieCollection = new ObservableCollection<PiePoint>();
        }
        private async Task UpdateFarmStats()
        {
            var info = await client.GetFarmInfo(AccessToken.Token);
            if(info == null)
            {
                FarmGrid = Visibility.Hidden;
                return;
            }
            List<PiePoint> points = new List<PiePoint>()
            {
                new PiePoint() { Name = "Alive pets", Share = info.CountAlivePets },
                new PiePoint() { Name = "Dead pets", Share = info.CountDeadPets }
            };
            PieCollection = new ObservableCollection<PiePoint>(points);

            var farm = mapper.Map<FarmInfoModel>(info);

            FarmGrid = Visibility.Visible;
            CountAlivePets = farm.CountAlivePets;
            CountDeadPets = farm.CountDeadPets;
            AverageFeedingPeriod = farm.AverageFeedingPeriod;
            AverageThirstQuenchingPeriod = farm.AverageThirstQuenchingPeriod;
            AveragePetsHappinessDaysCount = farm.AveragePetsHappinessDaysCount;
            AveragePetsAgeCount = farm.AveragePetsAgeCount;
            Name = farm.Name;
        }
        private async void UserAuthorized(string newToken)
        {
            if(newToken !="") await UpdateFarmStats();
        }

        
        public ICommand ButtonCreateFarm_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                if(await client.CreateFarm(mapper.Map<CreateFarmDto>(farmInfo), AccessToken.Token))
                {
                    await UpdateFarmStats();
                }
            });
        }
        public ICommand ButtonMyPets_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                OnMyPetsClicked.Invoke();
            });
        }
        public ICommand ButtonCollaboratorsPets_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                OnMyCollaboratorsPetsClicked.Invoke();
            });
        }
        
    }
}
