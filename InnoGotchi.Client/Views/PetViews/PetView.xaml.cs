using InnoGotchi.Client.Models;
using InnoGotchi.Client.ViewModels.PetsVewModels;
using System.Windows.Controls;

namespace InnoGotchi.Client.Views.PetViews
{
    public partial class PetView : Page
    {
        public PetView(PetAppearanceModel appearance)
        {
            InitializeComponent();
            DataContext = new PetViewModel(appearance);
            UpdateLayout();
        }

        public void SetNewBodyAppearance(string BodyPath)
        {
            ((PetViewModel)DataContext).BodyPath = BodyPath;
        }

        public void SetNewNoseAppearance(string NosePath)
        {
            ((PetViewModel)DataContext).NosePath = NosePath;
        }
        public void SetNewEyesAppearance(string EyesPath)
        {
            ((PetViewModel)DataContext).EyesPath = EyesPath;
        }
        public void SetNewMouthAppearance(string MouthPath)
        {
            ((PetViewModel)DataContext).MouthPath = MouthPath;
        }
    }
}
