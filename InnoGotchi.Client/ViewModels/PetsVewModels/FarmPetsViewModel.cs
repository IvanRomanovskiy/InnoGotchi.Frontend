using InnoGotchi.Frontend.Utilities;
using System.Windows.Input;

namespace InnoGotchi.Client.ViewModels.PetsVewModels
{
    public class FarmPetsViewModel : ViewModelBase
    {
        public delegate void CreatePetClicked();
        public static event CreatePetClicked OnCreatePetClicked;
        
        public FarmPetsViewModel()
        {

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
