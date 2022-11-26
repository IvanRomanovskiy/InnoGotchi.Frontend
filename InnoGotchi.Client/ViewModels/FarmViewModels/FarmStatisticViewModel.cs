using AutoMapper;
using InnoGotchi.Client.Models;
using InnoGotchi.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace InnoGotchi.Client.ViewModels.FarmViewModels
{
    public class FarmStatisticViewModel : ViewModelBase
    {
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
                farmInfo.AveragePetsAgeCount = value;
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

        public FarmStatisticViewModel()
        {
            FarmOverviewViewModel.OnFarmDataReceived += OnFarmDataReceived;
            farmInfo = new FarmInfoModel();
        }
        private void OnFarmDataReceived(FarmInfo info)
        {
            Name = info.Name;
            CountAlivePets = info.CountAlivePets.ToString();
            CountDeadPets = info.CountDeadPets.ToString();
            AverageFeedingPeriod = info.AverageFeedingPeriod.ToString();
            AverageThirstQuenchingPeriod = info.AverageThirstQuenchingPeriod.ToString();
            AveragePetsHappinessDaysCount = info.AveragePetsHappinessDaysCount.ToString();
            AveragePetsAgeCount = info.AveragePetsAgeCount.ToString();

            List<PiePoint> points = new List<PiePoint>()
            {
                new PiePoint() { Name = "Alive pets", Share = info.CountAlivePets },
                new PiePoint() { Name = "Dead pets", Share = info.CountDeadPets }
            };
            PieCollection = new ObservableCollection<PiePoint>(points);
        }

    }
}
