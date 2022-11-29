using InnoGotchi.ApiClient.Clients;
using InnoGotchi.ApiClient.Models.Users;
using InnoGotchi.Client.Models;
using InnoGotchi.Frontend.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InnoGotchi.Client.ViewModels.AccountViewModels
{
    public class ChangePasswordViewModel : ViewModelBase
    {
        private ChangePasswordDto changePasswordDto { get; set; } = new ChangePasswordDto();

        public delegate void UserNameChangedHandler(string FirstName, string LastName);
        public static event UserNameChangedHandler UserNameChanged;

        public string OldPassword
        {
            get => changePasswordDto.OldPassword;
            set
            {
                changePasswordDto.OldPassword = value;
                OnPropertyChanged();
            }
        }
        public string NewPassword
        {
            get => changePasswordDto.NewPassword;
            set
            {
                changePasswordDto.NewPassword = value;
                OnPropertyChanged();
            }
        }

        private string repeatPassword;
        public string RepeatPassword
        {
            get => repeatPassword;
            set
            {
                repeatPassword = value;
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


        public ChangePasswordViewModel(AccountClient client)
        {
            this.client = client;
        }
        public ICommand ButtonConfirm_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                Message = "";
                if (OldPassword == "") Message += "Old password is empty; ";
                if (NewPassword == "") Message += "New password is empty; ";
                if (RepeatPassword == "") Message += "Repeat password is empty; ";

                if (Message == "")
                {
                    if (await client.ChangePassword(changePasswordDto, AccessToken.Token))
                    {
                        Message = "Success";
                    }
                    else
                    {
                        Message = "Could not change password";
                    }
                }
            });
        }
    }
}
