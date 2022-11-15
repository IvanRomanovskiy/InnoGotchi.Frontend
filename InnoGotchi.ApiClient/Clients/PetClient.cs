using InnoGotchi.ApiClient.Interfaces.Pet;
using InnoGotchi.ApiClient.Models.Pets;
using InnoGotchi.Domain;
using System.Text.Json;

namespace InnoGotchi.ApiClient.Clients
{
    public class PetClient : IPetCommands
    {
        private readonly HttpClient httpClient;
        private readonly string token;

        public PetClient(IHttpClientFactory factory, string token)
        {
            httpClient = factory.CreateClient();
            this.token = token;
        }
        public async Task<bool> CreatePet(CreatePetDto createPet)
        {
            string stringData = JsonSerializer.Serialize(createPet);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8);

            HttpResponseMessage response = await httpClient.PostAsync("CreatePet?t=" + token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;
        }

        public async Task<bool> FeedPet(Guid petId)
        {
            string stringData = JsonSerializer.Serialize(petId);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8);

            HttpResponseMessage response = await httpClient.PutAsync("FeedPet?t=" + token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;

        }
        public async Task<bool> ThirstQuenchingPet(Guid petId)
        {
            string stringData = JsonSerializer.Serialize(petId);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8);

            HttpResponseMessage response = await httpClient.PutAsync("ThirstQuenchingPet?t=" + token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;

        }

        public async Task<Pets?> GetPets()
        {
            HttpResponseMessage response = await httpClient.GetAsync("GetPets?t=" + token);
            if (!response.IsSuccessStatusCode) return null;

            var farmInfo = JsonSerializer.Deserialize<Pets>(response.Content.ReadAsStringAsync().Result);

            return farmInfo;
        }


    }
}
