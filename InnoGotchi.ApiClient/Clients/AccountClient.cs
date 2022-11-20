using InnoGotchi.ApiClient.Interfaces.User;
using InnoGotchi.ApiClient.Models.Users;
using InnoGotchi.Domain;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace InnoGotchi.ApiClient.Clients
{
    public class AccountClient : IUserCommands
    {
        private readonly HttpClient httpClient;

        public AccountClient(HttpClient client) 
        {
            httpClient = client;
        }
        public async Task<UserData?> GetUserData(string token)
        {
            HttpResponseMessage response = await httpClient.GetAsync("GetUserData?t=" + token);
            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadFromJsonAsync<UserData>();

            return result;
        }
        public async Task<bool> ChangeName(ChangeNameDto changeName, string token)
        {
            string stringData = JsonSerializer.Serialize(changeName);

            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync("ChangeName?t=" + token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;

        }
        public async Task<bool> ChangePassword(ChangePasswordDto changePassword, string token)
        {
            string stringData = JsonSerializer.Serialize(changePassword);

            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync("ChangePassword?t=" + token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;

        }
        public async Task<bool> ChangeAvatar(ChangeAvatarDto changeAvatar, string token)
        {
            string stringData = JsonSerializer.Serialize(changeAvatar);

            var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync("ChangeAvatar?t=" + token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;

        }
    }
}
