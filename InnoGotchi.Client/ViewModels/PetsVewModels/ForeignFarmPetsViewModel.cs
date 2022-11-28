using AutoMapper;
using InnoGotchi.ApiClient.Clients;
using InnoGotchi.ApiClient.Models.Pets;
using InnoGotchi.Client.Models;
using InnoGotchi.Client.ViewModels.FarmViewModels;
using InnoGotchi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InnoGotchi.Client.ViewModels.PetsVewModels
{
    public class ForeignFarmPetsViewModel : FarmPetsViewModel
    {
        public delegate void PetSelected(Pet pet, bool canActivity);
        public static event PetSelected OnPetSelected;

        private bool isCollab;
        public ForeignFarmPetsViewModel(PetClient client, IMapper mapper) : base(client, mapper)
        {
            FarmOverviewViewModel.OnCollaboratorSelected += FarmOverviewViewModel_OnCollaboratorSelected;
            AllFarmsViewModel.OnUserSelected += AllFarmsViewModel_OnUserSelected;
        }

        private async void AllFarmsViewModel_OnUserSelected(UserFarmModel collaborator)
        {
            if (collaborator == null) return;
            isCollab = false;
            pets = (List<Pet>)collaborator.Pets;
            Sort();
            buttonVisible = isCollab;
            await ShowPage(buttonVisible);
        }

        private async void FarmOverviewViewModel_OnCollaboratorSelected(UserFarmModel collaborator)
        {
            if (collaborator == null) return;
            isCollab = true;
            pets = (List<Pet>)collaborator.Pets;
            Sort();
            buttonVisible = isCollab;
            await ShowPage(buttonVisible);
        }

        protected override void OnPetThumbnailSelected(PetThumbnailPositionModel selectedPetThumbnail)
        {
            if (selectedPetThumbnail == null) return;
            var selectedPet = pets.FirstOrDefault(pet => pet.Name == selectedPetThumbnail.PetThumbnail.PetName);
            if (selectedPet != null) OnPetSelected.Invoke(selectedPet, isCollab);
            Filter = "";
        }

        protected async override void PetThumbnailPositionModel_OnPetThumbnailButtonClicked(PetThumbnailPositionModel petThumbnail)
        {
            if (isCollab)
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
        }
    }
}
