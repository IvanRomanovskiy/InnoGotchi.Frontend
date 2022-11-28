using AutoMapper;
using InnoGotchi.ApiClient.Clients;
using InnoGotchi.ApiClient.Models.Pets;
using InnoGotchi.Client.Models;
using InnoGotchi.Client.ViewModels.FarmViewModels;
using InnoGotchi.Domain;
using InnoGotchi.Frontend.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InnoGotchi.Client.ViewModels.PetsVewModels
{
    public class OwnFarmPetsViewModel : FarmPetsViewModel
    {
        public delegate void CreatePetClicked();
        public static event CreatePetClicked OnCreatePetClicked;
        public delegate void PetSelected(Pet pet);
        public static event PetSelected OnPetSelected;

        public OwnFarmPetsViewModel(PetClient client, IMapper mapper) : base(client, mapper)
        {
            CreatePetViewModel.OnPetCreated += OnPetCreated;
            PetDetailsViewModel.OnStatusUpdated += OnStatusUpdated;
            FarmOverviewViewModel.OnFarmDataReceived += OnFarmDataReceived;

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
        private async void OnFarmDataReceived(FarmInfo info)
        {
            FarmName = info.Name;
            await InitPetsThumbnails();
            buttonVisible = true;
            await ShowPage(buttonVisible);
        }

        protected async Task InitPetsThumbnails()
        {
            pets = (List<Pet>)(await client.GetPets(AccessToken.Token)).pets;
            Sort();
        }
        protected override void OnPetThumbnailSelected(PetThumbnailPositionModel selectedPetThumbnail)
        {
            if (selectedPetThumbnail == null) return;
            var selectedPet = pets.FirstOrDefault(pet => pet.Name == selectedPetThumbnail.PetThumbnail.PetName);
            if (selectedPet != null) OnPetSelected.Invoke(selectedPet);
            Filter = "";
        }

        protected async override void PetThumbnailPositionModel_OnPetThumbnailButtonClicked(PetThumbnailPositionModel petThumbnail)
        {
            var selectedPet = pets.FirstOrDefault(pet => pet.Name == petThumbnail.PetThumbnail.PetName);
            if (selectedPet == null) return; 
            await client.FeedPet(mapper.Map<FeedingPetDto>(new PetActionModel { PetId = selectedPet.Id }), AccessToken.Token);
            var status = await client.ThirstQuenchingPet(mapper.Map<ThirstQuenchingPetDto>(new PetActionModel { PetId = selectedPet.Id }), AccessToken.Token);
            selectedPet.Status = status;
            var selectedThumbnail = PetsThumbnailsList
                .FirstOrDefault(thumbnail => thumbnail.Column == petThumbnail.Column &&
                                thumbnail.Row == petThumbnail.Row &&
                                thumbnail.PetThumbnail.PetName == petThumbnail.PetThumbnail.PetName);
            if (selectedThumbnail != null)
            {
                PetsThumbnailsList.Remove(selectedThumbnail);
                selectedThumbnail.PetThumbnail = mapper.Map<PetThumbnailModel>(selectedPet);
                PetsThumbnailsList.Add(selectedThumbnail);
            }
        }

        public ICommand ButtonCreatePet_Click
        {
            get => new RelayCommand((obj) =>
            {
                OnCreatePetClicked.Invoke();
            });
        }

    }
}
