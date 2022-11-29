namespace InnoGotchi.Domain
{
    public class PetAppearance
    {
        public Body Body { get; set; } = new Body(); 
        public Eye Eye { get; set; } = new Eye();
        public Mouth Mouth { get; set; } = new Mouth();
        public Nose Nose { get; set; } = new Nose();
    }
}
