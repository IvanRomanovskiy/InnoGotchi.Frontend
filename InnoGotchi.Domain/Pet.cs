namespace InnoGotchi.Domain
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PetAppearance Appearance { get; set; } = new PetAppearance();
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public PetStatus Status { get; set; } = new PetStatus();
        public bool IsAlive { get; set; }

    }
}