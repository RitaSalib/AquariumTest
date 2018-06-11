using System.Collections.Generic;

namespace AquariumTest.Models
{
    public class Species
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SpeciesPredator> Predators { get; set; }

        public Species()
        {
            this.Predators = new List<SpeciesPredator>();
        }
    }
}