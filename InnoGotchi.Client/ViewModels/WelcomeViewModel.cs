using InnoGotchi.Client.Views;
using InnoGotchi.Frontend.Utilities;
using System.Windows.Controls;
using System.Windows.Input;

namespace InnoGotchi.Client.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        private Page Login;
        private Page Register;
        private Page Page;
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

        public WelcomeViewModel()
        {
            Login = new Login();
            Register = new Register();
            Page = new Page();
            CurrentPage = Page;
            GameViewModel.OnLoggedOut += GameViewModel_OnLoggedOut;
        }

        private void GameViewModel_OnLoggedOut()
        {
           CurrentPage = new Page();
        }

        public ICommand ButtonLogin_Click
        {
            get 
            {
                return new RelayCommand((obj) =>
                {
                    CurrentPage = Login;
                });
            }
        }
        public ICommand ButtonRegister_Click
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    CurrentPage = Register;
                });
            }
        }

        public ICommand ButtonExit_Click
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    System.Windows.Application.Current.Shutdown();
                });
            }
        }

    }
}
