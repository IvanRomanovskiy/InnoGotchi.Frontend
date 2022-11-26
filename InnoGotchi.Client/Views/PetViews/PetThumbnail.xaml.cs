using InnoGotchi.Client.ViewModels.PetsVewModels;
using InnoGotchi.Domain;
using System.Windows.Controls;

namespace InnoGotchi.Client.Views.PetViews
{

    public partial class PetThumbnail : Page
    {
        public PetThumbnail(Pet pet)
        {
            InitializeComponent();
            DataContext = new PetThumbnailViewModel(pet);
            UpdateLayout();
        }
    }
}
