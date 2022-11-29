using InnoGotchi.ApiClient.Models.Farm;
using InnoGotchi.Domain;

namespace InnoGotchi.ApiClient.Interfaces.Farm
{
    public interface IFarmCommands
    {
        public Task<bool> CreateFarm(CreateFarmDto createFarm, string token);
        public Task<bool> AddCollaborator(AddCollaboratorDto addCollaborator, string token);
        public Task<FarmInfo?> GetFarmInfo(string token);
        public Task<CollaboratorFarms?> GetCollaboratiorFarms(string token);
        public Task<AllFarmsVms?> GetAllFarms(string token);
    }
}
