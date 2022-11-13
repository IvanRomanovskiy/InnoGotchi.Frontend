using InnoGotchi.ApiClient.Models.Farm;
using InnoGotchi.Domain;

namespace InnoGotchi.ApiClient.Interfaces.Farm
{
    public interface IFarmCommands
    {
        public Task<bool> CreateFarm(CreateFarmDto createFarm);
        public Task<bool> AddCollaborator(AddCollaboratorDto addCollaborator);
        public Task<FarmInfo?> GetFarmInfo();
        public Task<CollaboratorFarms?> GetCollaboratiorFarms();
    }
}
