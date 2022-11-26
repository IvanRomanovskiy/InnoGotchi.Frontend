using InnoGotchi.Client.Models;
using System;

namespace InnoGotchi.Client.ViewModels.PetsVewModels
{
    public class PetViewModel : ViewModelBase
    {
        private PetAppearanceModel appearance;
        public string BodyPath
        {
            get => appearance.BodyPath;
            set
            {
                appearance.BodyPath = value;
                OnPropertyChanged();
            }
        }
        public string NosePath
        {
            get => appearance.NosePath;
            set
            {
                appearance.NosePath = value;
                OnPropertyChanged();
            }
        }
        public string EyesPath
        {
            get => appearance.EyesPath;
            set
            {
                appearance.EyesPath = value;
                OnPropertyChanged();
            }
        }
        public string MouthPath
        {
            get => appearance.MouthPath;
            set
            {
                appearance.MouthPath = value;
                OnPropertyChanged();
            }
        }

       public PetViewModel(PetAppearanceModel appearance)
        {
            this.appearance = new PetAppearanceModel();
            BodyPath = appearance.BodyPath;
            NosePath = appearance.NosePath;
            EyesPath = appearance.EyesPath;
            MouthPath = appearance.MouthPath;
        }
    }
}
