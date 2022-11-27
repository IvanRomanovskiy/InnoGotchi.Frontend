using InnoGotchi.ApiClient.Interfaces.Pet;
using InnoGotchi.ApiClient.Models.Farm;
using InnoGotchi.ApiClient.Models.Pets;
using InnoGotchi.Domain;
using System.Net.Http.Json;
using System.Text.Json;

namespace InnoGotchi.ApiClient.Clients
{
    public class PetClient : IPetCommands
    {
        private readonly HttpClient httpClient;

        public PetClient(HttpClient client)
        {
            httpClient = client;
        }
        public async Task<Pet?> CreatePet(CreatePetDto createPet, string token)
        {
            string stringData = JsonSerializer.Serialize(createPet);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("CreatePet?t=" + token, contentData);

            if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<Pet>();
            else return null;
        }

        public async Task<PetStatus> FeedPet(FeedingPetDto petDto, string token)
        {
            string stringData = JsonSerializer.Serialize(petDto);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync("FeedPet?t=" + token, contentData);

            var petStatus = await response.Content.ReadFromJsonAsync<PetStatus>();
            return petStatus;

        }
        public async Task<PetStatus> ThirstQuenchingPet(ThirstQuenchingPetDto petDto, string token)
        {
            string stringData = JsonSerializer.Serialize(petDto);

            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync("ThirstQuenchingPet?t=" + token, contentData);

            var petStatus = await response.Content.ReadFromJsonAsync<PetStatus>();
            return petStatus;
        }

        public async Task<Pets?> GetPets(string token)
        {
            HttpResponseMessage response = await httpClient.GetAsync("GetPets?t=" + token);
            if (!response.IsSuccessStatusCode) return null;

            var farmInfo = await response.Content.ReadFromJsonAsync<GetPetsVm>();

            return new Pets { pets = farmInfo.Pets };
        }


    }
}
