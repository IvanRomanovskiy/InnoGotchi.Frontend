using InnoGotchi.Client.Views;
using System.Windows.Controls;

namespace InnoGotchi.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public delegate void UserLoggedIn(string token);
        public static event UserLoggedIn OnLoggedIn;

        private Page Welcome;
        private Page Game;

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

        private double frameOpacity;
        public double FrameOpacity
        {
            get { return frameOpacity; }
            set { frameOpacity = value; }
        }

        public MainViewModel()
        {
            Welcome = new Welcome();
            Game = new Game();
            CurrentPage = Welcome;
            AccessToken.UserAuthorized += UserAuthorized;
        }
        public void UserAuthorized(string newToken)
        {
            if (newToken != "")
            {
                OnLoggedIn.Invoke(newToken);
                CurrentPage = Game;
            }
            else 
            {
                CurrentPage = Welcome;
            }
        }

    }
}
