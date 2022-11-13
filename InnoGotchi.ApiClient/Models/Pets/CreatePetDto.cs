
namespace InnoGotchi.ApiClient.Models.Pets
{
    public class CreatePetDto
    {
        public string PetName { get; set; }
        public Guid BodyId { get; set; }
        public Guid EyeId { get; set; }
        public Guid MouthId { get; set; }
        public Guid NoseId { get; set; }
    }
}
