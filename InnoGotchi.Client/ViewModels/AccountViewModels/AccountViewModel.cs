using InnoGotchi.Client.Views.AccountViews;
using InnoGotchi.Frontend.Utilities;
using System.Windows.Controls;
using System.Windows.Input;

namespace InnoGotchi.Client.ViewModels.AccountViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        private Page changeAvatar;
        private Page changeName;
        private Page changePassword;

        private Page currentPage;
        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                OnPropertyChanged();
            }
        }
        public AccountViewModel()
        {
            CurrentPage = new Page();
            changeAvatar = new ChangeAvatar();
            changeName = new ChangeName();
            changePassword = new ChangePassword();
        }

        public ICommand ButtonChangeAvatar_Click
        {
            get => new RelayCommand((obj) =>
            {
                CurrentPage = changeAvatar;
            });
        }
        public ICommand ButtonChangeName_Click
        {
            get => new RelayCommand((obj) =>
            {
                CurrentPage = changeName;
            });
        }
        public ICommand ButtonChangePassword_Click
        {
            get => new RelayCommand((obj) =>
            {
                CurrentPage = changePassword;
            });
        }

    }
}
