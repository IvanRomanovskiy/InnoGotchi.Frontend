
namespace InnoGotchi.Domain
{
    public class CollaboratorFarm 
    {
        public string FarmName { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public byte[] OwnerAvatar { get; set; }
        public ICollection<Pet> Pets { get; set; }


    }
}