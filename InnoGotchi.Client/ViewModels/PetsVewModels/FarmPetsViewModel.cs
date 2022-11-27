using AutoMapper;
using InnoGotchi.ApiClient.Clients;
using InnoGotchi.Client.Models;
using InnoGotchi.Client.ViewModels.FarmViewModels;
using InnoGotchi.Domain;
using InnoGotchi.Frontend.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InnoGotchi.Client.ViewModels.PetsVewModels
{
    public class FarmPetsViewModel : ViewModelBase
    {
        public delegate void CreatePetClicked();
        public static event CreatePetClicked OnCreatePetClicked;
        public delegate void PetSelected(Pet pet);
        public static event PetSelected OnPetSelected;

        private readonly PetClient client;
        private readonly IMapper mapper;

        private string farmName;
        public string FarmName
        {
            get => farmName;
            set
            {
                farmName = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> Filters { get; set; } = new ObservableCollection<string>
        {
            "by happiness days",
            "by age",
            "by hunger level",
            "by thirsty level"
        };
        private string filter;
        public string Filter
        {
            get => filter;
            set
            {
                filter = value;
                OnPropertyChanged();
                SelectionChanged();
            }
        }

        private ObservableCollection<PetThumbnailPositionModel> petsThumbnailsList;
        public ObservableCollection<PetThumbnailPositionModel> PetsThumbnailsList
        {
            get => petsThumbnailsList;
            set
            {             
                petsThumbnailsList = value;
                OnPropertyChanged();
            }
        }

        private PetThumbnailPositionModel selectedPet;
        public PetThumbnailPositionModel SelectedPet
        {
            get => selectedPet;
            set 
            {
                selectedPet = value;
                OnPropertyChanged();
                OnPetThumbnailSelected(selectedPet);
            }
        }

        private int page = 1;
        public int Page
        {
            get => page;
            set
            {
                page = value;
                OnPropertyChanged();
            }
        }
        private int pageCount;
        public int PageCount 
        {
            get => pageCount;
            set
            {
                pageCount = value;
                OnPropertyChanged();
            }
        }

        private List<Pet> pets;

        public FarmPetsViewModel(PetClient client, IMapper mapper)
        {
            FarmOverviewViewModel.OnFarmDataReceived += OnFarmDataReceived;
            CreatePetViewModel.OnPetCreated += OnPetCreated;
            PetDetailsViewModel.OnStatusUpdated += OnStatusUpdated;

            this.client = client;
            this.mapper = mapper;
            PetsThumbnailsList = new ObservableCollection<PetThumbnailPositionModel>();
            pets = new List<Pet>();
            Filter = "by happiness days";
        }

        private async void OnFarmDataReceived(FarmInfo info)
        {
            FarmName = info.Name;
            await InitPetsThumbnails();
            await ShowPage();
        }
        private async void OnPetCreated(Pet pet)
        {
            pets.Add(pet);
        }
        private void OnStatusUpdated(Pet pet)
        {
            pets.RemoveAll(p => p.Id == pet.Id);
            pets.Add(pet);
        }
        public async void SelectionChanged()
        {
            Sort();
            Page = 1;
            await ShowPage();
        }
        private async Task InitPetsThumbnails()
        {
            pets = (List<Pet>)(await client.GetPets(AccessToken.Token)).pets;
            Sort();
        }
        private async Task ShowPage()
        {
            PageCount = (int)Math.Ceiling(pets.Count * 1.0 / 15);

            await Task.Run(() =>{

                App.Current.Dispatcher.Invoke(() =>
                {
                    PetsThumbnailsList.Clear();
                });


                if (Page > PageCount) Page = 1;
                int row = 0;
                int column = 0;


                for (int i = (Page - 1) * 15; i < pets.Count; i++)
                {
                    var thumbnail = new PetThumbnailPositionModel
                    {
                        PetThumbnail = mapper.Map<PetThumbnailModel>(pets[i]),
                        Row = row,
                        Column = column++
                    };

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        PetsThumbnailsList.Add(thumbnail);
                    });
                    

                    if (column == 5)
                    {
                        row++;
                        column = 0;
                    }
                    if (row == 3)
                    {
                        break;
                    }
                }

            }); 

        }



        public void Sort()
        {

            switch (Filter)
            {
                case "by happiness days":
                    {
                        pets.Sort((x,y) => y.Status.HappinessDayCount.CompareTo(x.Status.HappinessDayCount));
                        break;
                    }
                case "by age":
                    {
                        pets.Sort((x, y) => y.Status.Age.CompareTo(x.Status.Age));
                        break;
                    }
                case "by hunger level":
                    {
                        pets.Sort((x, y) => y.Status.HungerLevel.CompareTo(x.Status.HungerLevel));
                        break;
                    }
                case "by thirsty level":
                    {
                        pets.Sort((x, y) => y.Status.ThirstyLevel.CompareTo(x.Status.ThirstyLevel));
                        break;
                    }
            }
        }

        private void OnPetThumbnailSelected(PetThumbnailPositionModel selectedPetThumbnail)
        {
            var selectedPet = pets.FirstOrDefault(pet => pet.Name == selectedPetThumbnail.PetThumbnail.PetName);
            if (selectedPet != null) OnPetSelected.Invoke(selectedPet);
            Filter = "";
        }


        public ICommand ButtonCreatePet_Click
        {
            get => new RelayCommand((obj) =>
            {
                OnCreatePetClicked.Invoke();
            });
        }
        public ICommand ButtonPrevList_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                Page--;
                await ShowPage();
            }, (obj) => Page > 1 );
        }
        public ICommand ButtonNextList_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                Page++;
                await ShowPage();
            }, (obj) => Page < PageCount);
        }
    }
}
