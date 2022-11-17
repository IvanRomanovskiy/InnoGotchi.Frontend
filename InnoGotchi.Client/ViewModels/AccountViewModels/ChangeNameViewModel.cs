
using InnoGotchi.ApiClient.Clients;
using InnoGotchi.ApiClient.Models.Users;
using InnoGotchi.Client.Models;
using InnoGotchi.Frontend.Utilities;
using System.Windows.Input;
using static InnoGotchi.Client.ViewModels.GameViewModel;

namespace InnoGotchi.Client.ViewModels.AccountViewModels
{
    public class ChangeNameViewModel : ViewModelBase
    {
        private ChangeNameDto changeNameDto { get; set; }

        public delegate void UserNameChangedHandler(string FirstName, string LastName);
        public static event UserNameChangedHandler UserNameChanged;

        public string FirstName 
        { 
            get => changeNameDto.FirstName;
            set 
            { 
                changeNameDto.FirstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get => changeNameDto.LastName;
            set 
            { 
                changeNameDto.LastName = value;
                OnPropertyChanged();
            }
        }
        private string message;
        public string Message
        {
            get { return message; }
            set 
            {
                message = value;
                OnPropertyChanged();
            }
        }
        private AccountClient client;

        private UserDataModel userData = new UserDataModel();

        public ChangeNameViewModel(AccountClient client)
        {
            GetUserData += UserData;
            this.client = client;
            changeNameDto = new ChangeNameDto();
        }

        public void UserData(UserDataModel userData)
        {
            FirstName = userData.FirstName;
            LastName = userData.LastName;
            this.userData = userData;
        }

        public ICommand ButtonConfirm_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                Message = "";
                if (FirstName == "") Message += "First name is empty; ";
                if (LastName == "") Message += "Last name is empty; ";

                if (Message == "")
                {
                    if (await client.ChangeName(changeNameDto, AccessToken.Token))
                    {
                        Message = "Success";
                        UserNameChanged.Invoke(FirstName, LastName);
                        userData.FirstName = FirstName;
                        userData.LastName = LastName;
                    }
                    else
                    {
                        Message = "Could not change name";
                    }
                }
            },(obj) => (FirstName != userData.FirstName || LastName != userData.LastName));
        }

    }
}
