using InnoGotchi.ApiClient.Models.Pets;
using InnoGotchi.Domain;

namespace InnoGotchi.ApiClient.Interfaces.Pet
{
    public interface IPetCommands
    {
        public Task<bool> CreatePet(CreatePetDto createPet);
        public Task<bool> FeedPet(Guid petId);
        public Task<bool> ThirstQuenchingPet(Guid petId);
        public Task<Pets?> GetPets();
    }
}
