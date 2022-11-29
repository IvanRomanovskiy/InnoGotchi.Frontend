using InnoGotchi.Frontend.Utilities;
using System.Windows;
using System.Windows.Input;

namespace InnoGotchi.Client.Models
{
    public class PetThumbnailPositionModel
    {
        public delegate void PetThumbnailButtonClicked(PetThumbnailPositionModel petThumbnail);
        public static event PetThumbnailButtonClicked OnPetThumbnailButtonClicked;

        public PetThumbnailModel PetThumbnail { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Visibility ButtonVisibility { get; set; }

        public ICommand ButtonPetFeed_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                OnPetThumbnailButtonClicked.Invoke(this);
            }, (obj) => PetThumbnail.Thirsty < 80 && PetThumbnail.Hungry < 80);
        }
    }
}
