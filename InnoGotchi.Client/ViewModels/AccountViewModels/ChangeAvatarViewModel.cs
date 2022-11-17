
using InnoGotchi.ApiClient.Clients;
using InnoGotchi.ApiClient.Models.Users;
using InnoGotchi.Client.Components;
using InnoGotchi.Client.Models;
using InnoGotchi.Domain;
using InnoGotchi.Frontend.Utilities;
using Microsoft.Win32;
using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace InnoGotchi.Client.ViewModels.AccountViewModels
{
    public class ChangeAvatarViewModel : ViewModelBase
    {
        private ChangeAvatarDto changeAvatarDto { get; set; }

        public delegate void AvatarChangedHandler(BitmapFrame Avatar);
        public static event AvatarChangedHandler AvatarChanged;

        public BitmapFrame Avatar
        {
            get => changeAvatarDto.AvatarBase.ToImageFromBase64();
            set 
            {
                changeAvatarDto.AvatarBase = value.ToBase64String();
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

        public ChangeAvatarViewModel(AccountClient client)
        {
            GameViewModel.GetUserData += UserData;
            this.client = client;
            changeAvatarDto = new ChangeAvatarDto();
        }
        public void UserData(UserDataModel userData)
        {
            this.userData = userData;
            Avatar = userData.Avatar;
        }

        public ICommand ButtonPickAvatar_Click
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    var dialog = new OpenFileDialog()
                    {
                        Filter = "Picture files|*.png;*.jpg"
                    };
                    if (dialog.ShowDialog() == true)
                    {
                        Avatar = BitmapFrame.Create(new BitmapImage(new Uri(dialog.FileName)));

                    }
                });
            }
        }

        public ICommand ButtonConfirm_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                if (await client.ChangeAvatar(changeAvatarDto, AccessToken.Token))
                {
                    Message = "Success";
                    AvatarChanged.Invoke(Avatar);
                }
                else
                {
                    Message = "Could not change avatar";
                }
                
            }, (obj) => !(Avatar.ToBase64String().Equals(userData.Avatar.ToBase64String())));
        }


    }
}
