using InnoGotchi.Domain;

namespace InnoGotchi.ApiClient.Models.Farm
{
    public class GetPetsVm
    {
        public ICollection<Pet> Pets { get; set; }
    }
}
