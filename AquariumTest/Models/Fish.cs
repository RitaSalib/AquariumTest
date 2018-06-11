namespace AquariumTest.Models
{
    public class Fish
    {
        public int Id { get; set; }
        public int SpeciesId { get; set; }
        public int TankId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public virtual Species Species { get; set; }
        public virtual Tank Tank { get; set; }
    }
}