
namespace InnoGotchi.Client
{
    public static class AccessToken
    {
        public delegate void UserAuthorizedHandler(string newToken);
        public static event UserAuthorizedHandler? UserAuthorized;

        private static string token = "";
        public static string Token
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
                UserAuthorized.Invoke(value);
            }
        }
    }
}
