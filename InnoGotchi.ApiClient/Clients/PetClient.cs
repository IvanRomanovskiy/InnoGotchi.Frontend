using InnoGotchi.ApiClient.Interfaces.Pet;
using InnoGotchi.ApiClient.Models.Pets;
using InnoGotchi.Domain;
using System.Text.Json;

namespace InnoGotchi.ApiClient.Clients
{
    public class PetClient : Client, IPetCommands
    {
        private readonly string Token;
        public PetClient(HttpClient client, string token) : base(client)
        {
            client.BaseAddress = new Uri(client.BaseAddress + "Pet/");

            Token = token;
        }
        public async Task<bool> CreatePet(CreatePetDto createPet)
        {
            string stringData = JsonSerializer.Serialize(createPet);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8);

            HttpResponseMessage response = await httpClient.PostAsync("CreatePet?t=" + Token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;
        }

        public async Task<bool> FeedPet(Guid petId)
        {
            string stringData = JsonSerializer.Serialize(petId);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8);

            HttpResponseMessage response = await httpClient.PutAsync("FeedPet?t=" + Token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;

        }
        public async Task<bool> ThirstQuenchingPet(Guid petId)
        {
            string stringData = JsonSerializer.Serialize(petId);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8);

            HttpResponseMessage response = await httpClient.PutAsync("ThirstQuenchingPet?t=" + Token, contentData);

            if (response.IsSuccessStatusCode) return true;
            else return false;

        }

        public async Task<Pets?> GetPets()
        {
            HttpResponseMessage response = await httpClient.GetAsync("GetPets?t=" + Token);
            if (!response.IsSuccessStatusCode) return null;

            var farmInfo = JsonSerializer.Deserialize<Pets>(response.Content.ReadAsStringAsync().Result);

            return farmInfo;
        }


    }
}
