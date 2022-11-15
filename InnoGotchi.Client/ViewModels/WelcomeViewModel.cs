using InnoGotchi.Client.Views;
using InnoGotchi.Frontend.Utilities;
using System.Windows.Controls;
using System.Windows.Input;
using static InnoGotchi.Client.AccessToken;

namespace InnoGotchi.Client.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        private Page Login;
        private Page Register;

        private Page currentPage;
        UserAuthorizedHandler userAuthorized;
        public Page CurrentPage
        {
            get { return currentPage; }
            set 
            { 
                currentPage = value;
                OnPropertyChanged();
            }
        }

        private double frameOpacity;
        public double FrameOpacity
        {
            get { return frameOpacity; }
            set { frameOpacity = value; }
        }

        public WelcomeViewModel()
        {
            Login = new Login();
            Register = new Register();

            frameOpacity = 1;
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

    }
}
