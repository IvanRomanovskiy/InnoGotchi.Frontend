using InnoGotchi.ApiClient.Interfaces;
using InnoGotchi.ApiClient.Models.Users;
using InnoGotchi.Domain;
using System.Text;
using System.Text.Json;

namespace InnoGotchi.ApiClient.Clients
{
    public class ClientWithoutToken : Client, ICommandWithoutToken
    {
        public ClientWithoutToken(HttpClient client) : base(client)
        {
        }

        public async Task<bool> CreateUser(CreateUserDto createUser)
        {
            string stringData = JsonSerializer.Serialize(createUser);

            var contentData = new StringContent(stringData, Encoding.UTF8);

            HttpResponseMessage response = await httpClient.PostAsync("Account/CreateUser", contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;

        }
        public async Task<string?> GetToken(GetTokenDto tokenDto)
        {

            string stringData = JsonSerializer.Serialize(tokenDto);

            var contentData = new StringContent(stringData, Encoding.UTF8);

            HttpResponseMessage response = await httpClient.PostAsync("Account/GetToken", contentData);

            if (!response.IsSuccessStatusCode) return null;

            string stringJWT = await response.Content.ReadAsStringAsync();

            var jwt = JsonSerializer.Deserialize<Token>(stringJWT);

            return jwt.access_token;
        }
    }
}
