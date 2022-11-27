using AutoMapper;
using InnoGotchi.ApiClient.Clients;
using InnoGotchi.ApiClient.Models.Pets;
using InnoGotchi.Client.Models;
using InnoGotchi.Domain;
using InnoGotchi.Frontend.Utilities;
using System;
using System.Windows;
using System.Windows.Input;

namespace InnoGotchi.Client.ViewModels.PetsVewModels
{
    public class PetDetailsViewModel : ViewModelBase
    {
        public delegate void PetStatusUpdated(Pet pet);
        public static event PetStatusUpdated OnStatusUpdated;
        public delegate void ButtonBackPressed();
        public static event ButtonBackPressed OnBackPressed;

        private Visibility isAlive;
        public Visibility IsAlive
        {
            get => isAlive;
            set
            {
                isAlive = value;
                OnPropertyChanged();
            }
        }

        private Pet pet;
        private readonly PetClient client;
        private readonly IMapper mapper;
        private PetActionModel petAction;
        public string Name
        {
            get => pet.Name;
            set
            {
                pet.Name = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateOfBirth
        {
            get => pet.DateOfBirth;
            set
            {
                pet.DateOfBirth = value;
                OnPropertyChanged();
            }
        }
        public DateTime? DateOfDeath
        {
            get => pet.DateOfDeath;
            set
            {
                pet.DateOfDeath = value;
                OnPropertyChanged();
            }
        }
        public uint Age
        {
            get => pet.Status.Age;
            set
            {
                pet.Status.Age = value;
            }
        }
        public uint HappyDay
        {
            get => pet.Status.HappinessDayCount;
            set
            {
                pet.Status.HappinessDayCount = value;
                OnPropertyChanged();
            }
        }
        public int FeedingCount
        {
            get => pet.Status.FeedingCount;
            set
            {
                pet.Status.FeedingCount = value;
                OnPropertyChanged();
            }
        }
        public int ThirstQuenchingCount
        {
            get => pet.Status.ThirstQuenchingCount;
            set
            {
                pet.Status.ThirstQuenchingCount = value;
                OnPropertyChanged();
            }
        }
        public int HungerLevel
        {
            get => (int)Math.Round(pet.Status.HungerLevel);
            set
            {
                pet.Status.HungerLevel = value;
                OnPropertyChanged();
            }
        }
        public int ThirstyLevel
        {
            get => (int)Math.Round(pet.Status.ThirstyLevel);
            set
            {
                pet.Status.ThirstyLevel = value;
                OnPropertyChanged();
            }
        }
        public string BodyPath
        {
            get => pet.Appearance.Body.Path;
            set
            {
                pet.Appearance.Body.Path = value;
                OnPropertyChanged();
            }
        }
        public string EyesPath
        {
            get => pet.Appearance.Eye.Path;
            set
            {
                pet.Appearance.Eye.Path = value;
                OnPropertyChanged();
            }
        }
        public string MouthPath
        {
            get => pet.Appearance.Mouth.Path;
            set
            {
                pet.Appearance.Mouth.Path = value;
                OnPropertyChanged();
            }
        }
        public string NosePath
        {
            get => pet.Appearance.Nose.Path;
            set
            {
                pet.Appearance.Nose.Path = value;
                OnPropertyChanged();
            }
        }



        public PetDetailsViewModel(PetClient client,IMapper mapper)
        {
            FarmPetsViewModel.OnPetSelected += OnPetSelected;
            pet = new Pet();
            this.client = client;
            this.mapper = mapper;
            petAction = new PetActionModel();
        }
        private void OnPetSelected(Pet selectedPet)
        {
            pet = selectedPet;
            Name = selectedPet.Name;
            DateOfBirth = selectedPet.DateOfBirth;
            DateOfDeath = selectedPet.DateOfDeath;
            Age = selectedPet.Status.Age;
            HappyDay = selectedPet.Status.HappinessDayCount;
            FeedingCount = selectedPet.Status.FeedingCount;
            ThirstQuenchingCount = selectedPet.Status.ThirstQuenchingCount;
            HungerLevel = (int)Math.Round(selectedPet.Status.HungerLevel);
            ThirstyLevel = (int)Math.Round(selectedPet.Status.ThirstyLevel);
            BodyPath = selectedPet.Appearance.Body.Path;
            EyesPath = selectedPet.Appearance.Eye.Path;
            MouthPath = selectedPet.Appearance.Mouth.Path;
            NosePath = selectedPet.Appearance.Nose.Path;
            IsAlive = selectedPet.IsAlive ? Visibility.Visible : Visibility.Hidden;
            petAction.PetId = selectedPet.Id;
        }


        public ICommand ButtonFeed_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                var status = await client.FeedPet(mapper.Map<FeedingPetDto>(petAction), AccessToken.Token);
                pet.Status = status;
                OnPetSelected(pet);
                OnStatusUpdated.Invoke(pet);

            },(obj) => HungerLevel < 80);
        }
        public ICommand ButtonDrink_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                var status = await client.ThirstQuenchingPet(mapper.Map<ThirstQuenchingPetDto>(petAction), AccessToken.Token);
                pet.Status = status;
                OnPetSelected(pet);
                OnStatusUpdated.Invoke(pet);
            },(obj) => ThirstyLevel < 80);
        }
        

        public ICommand ButtonBack_Click
        {
            get => new RelayCommand((obj) =>
            {
                OnBackPressed.Invoke();
            });
        }
    }
}
