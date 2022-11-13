using InnoGotchi.ApiClient.Models.Users;
using InnoGotchi.Domain;
using System.Text.Json;

namespace InnoGotchi.ApiClient.Clients
{
    public class Client
    {
        protected readonly HttpClient httpClient;

        public Client(HttpClient client)
        {
            httpClient = client;
        }

    }
}
