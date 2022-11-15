using InnoGotchi.Client.Views;
using InnoGotchi.Domain;
using System.Windows.Controls;

namespace InnoGotchi.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Page Welcome;
        private Page Game;


        private Token token;
        public Token Token
        {
            get { return token; }
            set
            {
                token = value;
                if (token.access_token != null && token.access_token != "") CurrentPage = Game;
            }
        }

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

        public MainViewModel(Token token)
        {
            Welcome = new Welcome();
            frameOpacity = 1;
            CurrentPage = Welcome;
            this.token = token;
            AccessToken.UserAuthorized += UserAuthorized;
        }

        public void UserAuthorized()
        {
            Game = new Game();
            CurrentPage = Game;
        }

    }
}
