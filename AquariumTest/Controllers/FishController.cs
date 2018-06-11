using AquariumTest.Models;
using AquariumTest.Repositories;
using AquariumTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AquariumTest.Controllers
{
    [Route("api/[controller]")]
    public class FishController : Controller
    {
        private readonly IRepository _repository;
        private readonly IFoodChainService _foodChainService;

        public FishController(IRepository repository, IFoodChainService foodChainService)
        {
            this._repository = repository;
            this._foodChainService = foodChainService;
        }

        // GET: api/fish
        [HttpGet]
        public IActionResult Get()
        {
            var result = this._repository.Fishes.ToList();

            foreach (var fish in result)
            {
                fish.Species = null;
                fish.Tank = null;
            }

            return this.Json(result);
        }

        // GET: api/fish/1
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = this._repository.Fishes
                .Include(x=>x.Species)
                .FirstOrDefault(x => x.Id == id);

            var predators = this._repository.SpeciesPredators.Where(x => x.SpeciesId == result.SpeciesId).ToList();
            if (result.Species != null)
            {
                result.Species.Predators = predators;
            }

            if (result == null)
                return this.NotFound();

            return this.Json(result);
        }

        // POST: api/fish
        [HttpPost]
        public IActionResult Post([FromBody] Fish value)
        {
            var species = this._repository.Species.FirstOrDefault(x => x.Id == value.SpeciesId);
            var tank = this._repository.Tanks.FirstOrDefault(x => x.Id == value.TankId);

            if (species == null || tank == null)
                return this.NotFound();

            var fish = new Fish()
            {
                Name = value.Name,
                Color = value.Color,
                Species = species,
                SpeciesId = species.Id,
                Tank = tank,
                TankId = tank.Id
            };

            this._repository.Fishes.Add(fish);
            this._repository.SaveChanges();

            this._foodChainService.EatFish();

            return this.Json(fish.Id);
        }

        // PUT: api/fish/1
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Fish value, int id)
        {
            var fish = this._repository.Fishes.FirstOrDefault(x => x.Id == id);

            if (fish == null)
                return this.NotFound();

            var species = this._repository.Species.FirstOrDefault(x => x.Id == value.SpeciesId);
            var tank = this._repository.Tanks.FirstOrDefault(x => x.Id == value.TankId);

            if (species == null || tank == null)
                return this.BadRequest();

            fish.Species = species;
            fish.SpeciesId = species.Id;
            fish.Tank = tank;
            fish.TankId = tank.Id;
            fish.Color = value.Color;
            fish.Name = value.Name;

            this._repository.SaveChanges();

            this._foodChainService.EatFish();

            return this.Ok();
        }

        // DELETE: api/fish/1
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var fish = this._repository.Fishes.FirstOrDefault(x => x.Id == id);

            if (fish == null)
                return this.NotFound();

            this._repository.Fishes.Remove(fish);
            this._repository.SaveChanges();

            return this.Ok();
        }
    }
}