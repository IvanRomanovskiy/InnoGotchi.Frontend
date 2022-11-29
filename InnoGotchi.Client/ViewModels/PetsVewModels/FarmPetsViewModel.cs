using AutoMapper;
using InnoGotchi.ApiClient.Clients;
using InnoGotchi.Client.Models;
using InnoGotchi.Domain;
using InnoGotchi.Frontend.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InnoGotchi.Client.ViewModels.PetsVewModels
{
    public abstract class FarmPetsViewModel : ViewModelBase
    {
        protected readonly PetClient client;
        protected readonly IMapper mapper;

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

        protected bool buttonVisible;

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

        protected int page = 1;
        public int Page
        {
            get => page;
            set
            {
                page = value;
                OnPropertyChanged();
            }
        }
        protected int pageCount;
        public int PageCount 
        {
            get => pageCount;
            set
            {
                pageCount = value;
                OnPropertyChanged();
            }
        }

        protected List<Pet> pets;

        public FarmPetsViewModel(PetClient client, IMapper mapper)
        {
            PetThumbnailPositionModel.OnPetThumbnailButtonClicked += PetThumbnailPositionModel_OnPetThumbnailButtonClicked;
            this.client = client;
            this.mapper = mapper;
            PetsThumbnailsList = new ObservableCollection<PetThumbnailPositionModel>();
            pets = new List<Pet>();
            Filter = "by happiness days";
            buttonVisible = false;
        }

        protected abstract void PetThumbnailPositionModel_OnPetThumbnailButtonClicked(PetThumbnailPositionModel petThumbnail);

        public async void SelectionChanged()
        {
            Sort();
            Page = 1;
            await ShowPage(buttonVisible);
        }
        protected async Task ShowPage(bool buttonVisible)
        {
            PageCount = (int)Math.Ceiling(pets.Count * 1.0 / 15);
            if (pageCount == 0) PageCount++;

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
                        Column = column++,
                        ButtonVisibility = !pets[i].IsAlive ? System.Windows.Visibility.Hidden : 
                                            buttonVisible ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden,

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



        protected void Sort()
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

        protected abstract void OnPetThumbnailSelected(PetThumbnailPositionModel selectedPetThumbnail);



        public ICommand ButtonPrevList_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                Page--;
                await ShowPage(buttonVisible);
            }, (obj) => Page > 1 );
        }
        public ICommand ButtonNextList_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                Page++;
                await ShowPage(buttonVisible);
            }, (obj) => Page < PageCount);
        }
    }
}
