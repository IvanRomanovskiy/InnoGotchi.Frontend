using InnoGotchi.ApiClient.Interfaces.Farm;
using InnoGotchi.ApiClient.Models.Farm;
using InnoGotchi.Domain;
using System.Text.Json;

namespace InnoGotchi.ApiClient.Clients
{
    public class FarmClient : IFarmCommands
    {
        private readonly HttpClient httpClient;
        private readonly string token;

        public FarmClient(IHttpClientFactory factory, string token)
        {
            httpClient = factory.CreateClient();
            this.token = token;
        }

        public async Task<bool> CreateFarm(CreateFarmDto createFarm)
        {
            string stringData = JsonSerializer.Serialize(createFarm);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("CreateFarm?t=" + token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;
        }
        public async Task<bool> AddCollaborator(AddCollaboratorDto addCollaborator)
        {
            string stringData = JsonSerializer.Serialize(addCollaborator);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("AddCollaborator?t=" + token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;
        }

        public async Task<FarmInfo?> GetFarmInfo()
        {
            HttpResponseMessage response = await httpClient.GetAsync("GetUserData?t=" + token);
            if (!response.IsSuccessStatusCode) return null;

            var farmInfo = JsonSerializer.Deserialize<FarmInfo>(response.Content.ReadAsStringAsync().Result);

            return farmInfo;
        }
        public async Task<CollaboratorFarms?> GetCollaboratiorFarms()
        {
            HttpResponseMessage response = await httpClient.GetAsync("GetCollaboratiorFarms?t=" + token);
            if (!response.IsSuccessStatusCode) return null;

            var farmInfo = JsonSerializer.Deserialize<CollaboratorFarms>(response.Content.ReadAsStringAsync().Result);

            return farmInfo;
        }


    }
}
