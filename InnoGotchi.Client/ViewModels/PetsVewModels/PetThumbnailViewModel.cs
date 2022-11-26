using InnoGotchi.Client.Models;
using InnoGotchi.Client.Views.PetViews;
using InnoGotchi.Domain;

using System.Windows.Controls;
using System.Windows.Media;

namespace InnoGotchi.Client.ViewModels.PetsVewModels
{
    public class PetThumbnailViewModel : ViewModelBase
    {
        private Page petView;
        public Page PetView
        {
            get => petView;
            set
            {
                petView = value;
                OnPropertyChanged();
            }
        }

        private string backgroundColor;
        public string BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                OnPropertyChanged();
            }
        }

        private Pet pet;

        public string PetName
        {
            get => pet.Name;
            set
            {
                pet.Name = value;

                OnPropertyChanged();
            }
        }

        public PetThumbnailViewModel(Pet pet)
        {
            BackgroundColor = "Transperent";
            this.pet = pet;
            if (pet.IsAlive) BackgroundColor = "Aqua";
            else BackgroundColor = "Brown";
            PetName = pet.Name;

            var appearance = new PetAppearanceModel
            {
                BodyPath = pet.Appearance.Body.Path,
                EyesPath = pet.Appearance.Eye.Path,
                MouthPath = pet.Appearance.Mouth.Path,
                NosePath = pet.Appearance.Nose.Path
            };

            PetView = new PetView(appearance);
        }

    }
}
