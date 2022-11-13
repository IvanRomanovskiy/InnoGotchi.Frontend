using InnoGotchi.ApiClient.Interfaces.Farm;
using InnoGotchi.ApiClient.Models.Farm;
using InnoGotchi.Domain;
using System.Text.Json;

namespace InnoGotchi.ApiClient.Clients
{
    public class FarmAccount : Client, IFarmCommands
    {
        private readonly string Token;
        public FarmAccount(HttpClient client, string token) : base(client)
        {
            client.BaseAddress = new Uri(client.BaseAddress + "Farm/");
            Token = token;
        }

        public async Task<bool> CreateFarm(CreateFarmDto createFarm)
        {
            string stringData = JsonSerializer.Serialize(createFarm);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("CreateFarm?t=" + Token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;
        }
        public async Task<bool> AddCollaborator(AddCollaboratorDto addCollaborator)
        {
            string stringData = JsonSerializer.Serialize(addCollaborator);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("AddCollaborator?t=" + Token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;
        }

        public async Task<FarmInfo?> GetFarmInfo()
        {
            HttpResponseMessage response = await httpClient.GetAsync("GetUserData?t=" + Token);
            if (!response.IsSuccessStatusCode) return null;

            var farmInfo = JsonSerializer.Deserialize<FarmInfo>(response.Content.ReadAsStringAsync().Result);

            return farmInfo;
        }
        public async Task<CollaboratorFarms?> GetCollaboratiorFarms()
        {
            HttpResponseMessage response = await httpClient.GetAsync("GetCollaboratiorFarms?t=" + Token);
            if (!response.IsSuccessStatusCode) return null;

            var farmInfo = JsonSerializer.Deserialize<CollaboratorFarms>(response.Content.ReadAsStringAsync().Result);

            return farmInfo;
        }


    }
}
