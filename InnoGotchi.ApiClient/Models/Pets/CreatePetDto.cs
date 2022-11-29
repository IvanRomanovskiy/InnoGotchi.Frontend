
namespace InnoGotchi.ApiClient.Models.Pets
{
    public class CreatePetDto
    {
        public string PetName { get; set; }
        public string BodyPath { get; set; }
        public string EyePath { get; set; }
        public string MouthPath { get; set; }
        public string NosePath { get; set; }
    }
}
