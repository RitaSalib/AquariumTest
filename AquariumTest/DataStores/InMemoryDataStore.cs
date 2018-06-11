using AquariumTest.Models;
using System.Collections.Generic;

namespace AquariumTest.DataStores
{
    public interface IDataStore
    {
        List<Tank> Tanks { get; set; }
        List<Fish> Fishes { get; set; }
        List<Species> Species { get; set; }
        List<SpeciesPredator> SpeciesPredators { get; set; }
    }

    public class InMemoryDataStore : IDataStore
    {
        public List<Tank> Tanks { get; set; }
        public List<Fish> Fishes { get; set; }
        public List<Species> Species { get; set; }
        public List<SpeciesPredator> SpeciesPredators { get; set; }

        public InMemoryDataStore()
        {
            this.Tanks = new List<Tank>();
            this.Fishes = new List<Fish>();
            this.Species = new List<Species>();
            this.SpeciesPredators = new List<SpeciesPredator>();
        }
    }
}
