
namespace InnoGotchi.Domain
{
    public class PetStatus
    {
        public uint Age { get; set; }
        public double HungerLevel { get; set; }
        public int FeedingCount { get; set; }
        public double ThirstyLevel { get; set; }
        public int ThirstQuenchingCount { get; set; }
        public uint HappinessDayCount { get; set; }

    }
}