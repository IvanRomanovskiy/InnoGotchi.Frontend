
namespace InnoGotchi.Client
{
    public static class AccessToken
    {
        public delegate void UserAuthorizedHandler();
        public static event UserAuthorizedHandler? UserAuthorized;

        private static string token;
        public static string Token
        {
            get
            {
                return token;
            }
            set
            {
                UserAuthorized.Invoke();
                token = value;
            }
        }
    }
}
