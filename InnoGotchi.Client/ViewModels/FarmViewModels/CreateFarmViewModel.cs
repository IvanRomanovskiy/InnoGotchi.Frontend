using InnoGotchi.ApiClient.Clients;
using InnoGotchi.ApiClient.Models.Farm;
using InnoGotchi.Frontend.Utilities;
using System.Windows.Input;

namespace InnoGotchi.Client.ViewModels.FarmViewModels
{
    public class CreateFarmViewModel : ViewModelBase
    {
        public delegate void FarmCreated();
        public static event FarmCreated OnFarmCreated;

        private readonly FarmClient client;
        private CreateFarmDto farmDto;
        private string error;
        public string Name
        {
            get => farmDto.Name;
            set
            {
                farmDto.Name = value;
                OnPropertyChanged();
            }
        }
        public string Error
        {
            get => error;
            set
            {
                error = value;
                OnPropertyChanged();
            }
        }
        public CreateFarmViewModel(FarmClient client)
        {
            this.client = client;
            farmDto = new CreateFarmDto();
        }





        public ICommand ButtonCreateFarm_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                Error = "";
                if (Name != "")
                {
                    if (await client.CreateFarm(farmDto, AccessToken.Token))
                    {
                        OnFarmCreated.Invoke();
                    }
                    else Error = "Farm name already exists.";
                }
                else Error = "Name is empty";
            });
        }
    }
}
