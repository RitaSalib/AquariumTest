using AquariumTest.Models;
using AquariumTest.Repositories;
using System.Collections.Generic;

namespace AquariumTest.SeedData
{
    public class SeedDataManager
    {
        private readonly IRepository _repository;

        public SeedDataManager(IRepository repository)
        {
            this._repository = repository;
        }

        public void Seed()
        {
            this.PopulateData();
            this._repository.SaveChanges();
        }

        private void PopulateData()
        {
            var piranha = new Species() { Name = "Piranha" };
            var betta = new Species() { Name = "Crowntail Betta" };
            var goldfish = new Species() { Name = "Goldfish" };
            var crab = new Species() { Name = "Hermit Crab" };
            var snail = new Species() { Name = "Black Racer Nerite Snail" };
            var shrimp = new Species() { Name = "Ghost Shrimp" };

            this._repository.Species.AddRange(piranha, betta, goldfish, crab, snail, shrimp);

            var speciesPredators = new List<SpeciesPredator>()
            {
                new SpeciesPredator() { Species = betta, Predator = piranha },
                new SpeciesPredator() { Species = goldfish, Predator = piranha },
                new SpeciesPredator() { Species = snail, Predator = crab },
                new SpeciesPredator() { Species = shrimp, Predator = crab },
                new SpeciesPredator() { Species = crab, Predator = piranha }
            };

            this._repository.SpeciesPredators.AddRange(speciesPredators);

            var piranhaTank = new Tank() { Name = "Piranha Tank", Capacity = 5 };
            var generalTank = new Tank() { Name = "General Tank", Capacity = 15 };
            var crabTank = new Tank() { Name = "Crab Tank", Capacity = 5 };
            var shrimpSnailTank = new Tank() { Name = "Shrimp & Snail Tank", Capacity = 10 };

            this._repository.Tanks.AddRange(piranhaTank, generalTank, crabTank, shrimpSnailTank);

            var fishList = new List<Fish>()
            {
                new Fish() { Name = "Paul", Species = piranha, Color = "Grey", Tank = piranhaTank },
                new Fish() { Name = "Pip", Species = piranha, Color = "Red", Tank = piranhaTank },
                new Fish() { Name = "Pete", Species = piranha, Color = "Red", Tank = piranhaTank },

                new Fish() { Name = "Chuck", Species = crab, Color = "Red", Tank = crabTank},
                new Fish() { Name = "Carl", Species = crab, Color = "Blue", Tank = crabTank},
                new Fish() { Name = "Cora", Species = crab, Color = "Red", Tank = crabTank},
                new Fish() { Name = "Charles", Species = crab, Color = "Blue", Tank = crabTank},

                new Fish() { Name = "Sandra", Species = shrimp, Color = "Black", Tank = shrimpSnailTank},
                new Fish() { Name = "Sam", Species = shrimp, Color = "Pink", Tank = shrimpSnailTank},
                new Fish() { Name = "Sergio", Species = shrimp, Color = "Pink", Tank = shrimpSnailTank},
                new Fish() { Name = "Stuart", Species = snail, Color = "Brown", Tank = shrimpSnailTank},
                new Fish() { Name = "Shelly", Species = snail, Color = "Brown", Tank = shrimpSnailTank},

                new Fish() { Name = "George", Species = goldfish, Color = "Orange", Tank = generalTank},
                new Fish() { Name = "Gin", Species = goldfish, Color = "Orange", Tank = generalTank},
                new Fish() { Name = "Gene", Species = goldfish, Color = "Gold", Tank = generalTank},
                new Fish() { Name = "Bud", Species = betta, Color = "Purple", Tank = generalTank},
                new Fish() { Name = "Billy", Species = betta, Color = "Purple", Tank = generalTank},
                new Fish() { Name = "Betty", Species = betta, Color = "Purple", Tank = generalTank},
                new Fish() { Name = "Benjamin", Species = betta, Color = "Purple", Tank = generalTank},
            };

            this._repository.Fishes.AddRange(fishList);
        }
    }
}
