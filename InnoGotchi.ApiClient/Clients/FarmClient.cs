using InnoGotchi.ApiClient.Interfaces.Farm;
using InnoGotchi.ApiClient.Models.Farm;
using InnoGotchi.Domain;
using System.Net.Http.Json;
using System.Text.Json;

namespace InnoGotchi.ApiClient.Clients
{
    public class FarmClient : IFarmCommands
    {
        private readonly HttpClient httpClient;
        public FarmClient(HttpClient client)
        {
            httpClient = client;
        }

        public async Task<bool> CreateFarm(CreateFarmDto createFarm,string token)
        {
            string stringData = JsonSerializer.Serialize(createFarm);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("CreateFarm?t=" + token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;
        }
        public async Task<bool> AddCollaborator(AddCollaboratorDto addCollaborator, string token)
        {
            string stringData = JsonSerializer.Serialize(addCollaborator);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("AddCollaborator?t=" + token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;
        }

        public async Task<FarmInfo?> GetFarmInfo(string token)
        {
            HttpResponseMessage response = await httpClient.GetAsync("GetFarmInfo?t=" + token);
            if (!response.IsSuccessStatusCode) return null;

            var farmInfo = await response.Content.ReadFromJsonAsync<FarmInfo>();

            return farmInfo;
        }
        public async Task<CollaboratorFarms?> GetCollaboratiorFarms(string token)
        {
            HttpResponseMessage response = await httpClient.GetAsync("GetCollaboratiorFarms?t=" + token);
            if (!response.IsSuccessStatusCode) return null;

            var farmInfo = await response.Content.ReadFromJsonAsync<CollaboratorFarms>();

            return farmInfo;
        }

        public async Task<AllFarmsVms?> GetAllFarms(string token)
        {
            HttpResponseMessage response = await httpClient.GetAsync("GetAllFarms?t=" + token);
            if (!response.IsSuccessStatusCode) return null;

            var farmInfo = await response.Content.ReadFromJsonAsync<AllFarmsVms>();

            return farmInfo;
        }
    }
}
