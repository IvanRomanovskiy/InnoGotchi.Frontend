using InnoGotchi.Client.Views;
using System.Windows.Controls;

namespace InnoGotchi.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
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
            frameOpacity = 1;
            CurrentPage = Welcome;
            AccessToken.UserAuthorized += UserAuthorized;
        }
        public void UserAuthorized(string newToken)
        {
            if (newToken != "")
            {
                CurrentPage = Game;
            }
            else 
            {
                Game = new Game();
                Welcome = new Welcome();             
                CurrentPage = Welcome; 
            }
        }

    }
}
