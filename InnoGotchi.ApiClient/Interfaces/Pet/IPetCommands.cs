using InnoGotchi.ApiClient.Models.Pets;
using InnoGotchi.Domain;

namespace InnoGotchi.ApiClient.Interfaces.Pet
{
    public interface IPetCommands
    {
        public Task<Domain.Pet?> CreatePet(CreatePetDto createPet, string token);
        public Task<bool> FeedPet(Guid petId, string token);
        public Task<bool> ThirstQuenchingPet(Guid petId, string token);
        public Task<Pets?> GetPets(string token);
    }
}
