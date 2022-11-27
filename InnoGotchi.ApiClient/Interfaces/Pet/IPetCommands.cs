using InnoGotchi.ApiClient.Models.Pets;
using InnoGotchi.Domain;

namespace InnoGotchi.ApiClient.Interfaces.Pet
{
    public interface IPetCommands
    {
        public Task<Domain.Pet?> CreatePet(CreatePetDto createPet, string token);
        public Task<PetStatus> FeedPet(FeedingPetDto petDto, string token);
        public Task<PetStatus> ThirstQuenchingPet(ThirstQuenchingPetDto petDto, string token);
        public Task<Pets?> GetPets(string token);
    }
}
