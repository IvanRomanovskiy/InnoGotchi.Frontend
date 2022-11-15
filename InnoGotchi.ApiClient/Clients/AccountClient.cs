using InnoGotchi.ApiClient.Interfaces.User;
using InnoGotchi.ApiClient.Models.Users;
using InnoGotchi.Application.Models.Users.Queries.GetUserData;
using InnoGotchi.Domain;
using System.Text.Json;

namespace InnoGotchi.ApiClient.Clients
{
    public class AccountClient : IUserCommands
    {
        private readonly HttpClient httpClient;
        private readonly string token;

        public AccountClient(HttpClient client) 
        {
            httpClient = client;
        }
        public async Task<UserData?> GetUserData()
        {
            HttpResponseMessage response = await httpClient.GetAsync("GetUserData?t=" + token);
            if (!response.IsSuccessStatusCode) return null;

            var userData = JsonSerializer.Deserialize<UserData>(response.Content.ReadAsStringAsync().Result);

            return userData;
        }
        public async Task<bool> ChangeName(ChangeNameDto changeName)
        {
            string stringData = JsonSerializer.Serialize(changeName);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8);

            HttpResponseMessage response = await httpClient.PutAsync("ChangeName?t=" + token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;

        }
        public async Task<bool> ChangePassword(ChangePasswordDto changePassword)
        {
            string stringData = JsonSerializer.Serialize(changePassword);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8);

            HttpResponseMessage response = await httpClient.PutAsync("ChangePassword?t=" + token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;

        }
        public async Task<bool> ChangeAvatar(ChangeAvatarDto changeAvatar)
        {
            string stringData = JsonSerializer.Serialize(changeAvatar);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8);

            HttpResponseMessage response = await httpClient.PutAsync("ChangeAvatar?t=" + token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;

        }

    }
}
