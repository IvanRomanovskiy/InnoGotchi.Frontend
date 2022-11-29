using InnoGotchi.Domain;


namespace InnoGotchi.ApiClient.Models.Farm
{
    public class AllFarmsVm
    {
        public string FarmName { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerAvatarBase { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }

    }
}
