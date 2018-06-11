using AquariumTest.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AquariumTest.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly IRepository _repository;

        public TestController(IRepository repository)
        {
            this._repository = repository;
        }

        // GET: api/test
        [HttpGet]
        public IActionResult Get()
        {
            var tanks = this._repository.Tanks.ToList();

            foreach (var tank in tanks)
            {
                var fishes = this._repository.Fishes
                                 .Include(x => x.Species)
                                 .Where(x => x.TankId == tank.Id)
                                 .ToList();

                foreach (var fish in fishes)
                {
                    var predators = this._repository.SpeciesPredators.Where(x => x.SpeciesId == fish.SpeciesId).ToList();
                    if (fish.Species != null)
                    {
                        fish.Species.Predators = predators;
                    }
                }

                tank.Fishes = fishes;
            }

            // Sends all tank objects fully hydrated (pun intended) back in the response.
            return this.Json(tanks);
        }
    }
}
