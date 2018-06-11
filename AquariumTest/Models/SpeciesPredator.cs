namespace AquariumTest.Models
{
    public class SpeciesPredator
    {
        public int Id { get; set; }
        public int SpeciesId { get; set; }
        public int PredatorId { get; set; }

        public virtual Species Species { get; set; }
        public virtual Species Predator { get; set; }
    }
}
