using AquariumTest.Models;
using AquariumTest.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AquariumTest.Services
{
    public class FoodChainService : IFoodChainService
    {
        private readonly IRepository _repository;

        public FoodChainService(IRepository repository)
        {
            this._repository = repository;
        }

        public void EatFish()
        {
            try
            {
                var tanks = this._repository.Tanks.ToList();

                foreach (var tank in tanks)
                {
                    var tankFish = this._repository.Fishes
                                       .Include(x=>x.Species)
                                       .ThenInclude(x=>x.Predators)
                                       .Where(x => x.TankId == tank.Id).ToList();

                    var eatenFish = new List<Fish>();

                    foreach (var fish in tankFish)
                    {
                        if (fish.Species.Predators.Any())
                        {
                            var predatorIds = fish.Species.Predators.Select(x => x.PredatorId).ToList();

                            if (tankFish.Any(x => predatorIds.Contains(x.SpeciesId)))
                            {
                                eatenFish.Add(fish);
                            }
                        }
                    }

                    if (eatenFish.Any())
                        this._repository.Fishes.RemoveRange(eatenFish);
                }

                this._repository.SaveChanges();
            }catch(Exception e)
            {
                throw e;
            }
        }
    }
}
