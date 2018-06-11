using AquariumTest.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AquariumTest.Controllers
{
    [Route("api/[controller]")]
    public class TankController : Controller
    {
        private readonly IRepository _repository;

        public TankController(IRepository repository)
        {
            this._repository = repository;
        }

        // GET: api/tank
        [HttpGet]
        public IActionResult Get()
        {
            var tanks = this._repository.Tanks
                .ToList();

            return this.Json(tanks);
        }

        // GET: api/tank/1
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = this._repository.Tanks.FirstOrDefault(x => x.Id == id);

            if (result == null)
                return this.NotFound();

            var fishes = this._repository.Fishes
                             .Include(x => x.Species)
                             .Where(x => x.TankId == result.Id)
                             .ToList();

            result.Fishes = fishes;

            return this.Json(result);
        }

        // GET: api/tank/1/fish
        [HttpGet("{id}/fish")]
        public IActionResult GetTankFish(int id)
        {
            var tank = this._repository.Tanks.FirstOrDefault(x => x.Id == id);

            if (tank == null)
                return this.NotFound();

            var fishes = this._repository.Fishes
                             .Include(x => x.Species)
                             .Where(x => x.TankId == tank.Id)
                             .ToList();

            return this.Json(fishes);
        }

        #region For Modifying Tanks

        //// POST: api/tank
        //[HttpPost]
        //public IActionResult Post([FromBody] Tank value)
        //{

        //    var tank = new Tank()
        //    {
        //        Name = value.Name,
        //        Capacity = value.Capacity
        //    };

        //    if (value.Fishes.Any())
        //    {
        //        foreach (var fish in value.Fishes)
        //        {
        //            if (fish.Id > 0)
        //            {
        //                var existing = this._repository.Fishes.FirstOrDefault(x => x.Id == fish.Id);
        //                tank.Fishes.Add(existing);
        //            }
        //            else
        //            {
        //                this._repository.Fishes.Add(fish);
        //                tank.Fishes.Add(fish);
        //            }
        //        }
        //    }

        //    this._repository.Tanks.Add(tank);
        //    this._repository.SaveChanges();

        //    this._foodChainService.EatFish();

        //    return this.Json(tank.Id);
        //}

        //// PUT: api/tank/1
        //[HttpPut("{id}")]
        //public IActionResult Put([FromBody] Tank value, int id)
        //{
        //    var tank = this._repository.Tanks
        //        .Include(x => x.Fishes)
        //        .FirstOrDefault(x => x.Id == id);

        //    if (tank == null)
        //        return this.HttpNotFound();

        //    tank.Name = value.Name;
        //    tank.Capacity = value.Capacity;

        //    if (value.Fishes.Any())
        //    {
        //        tank.Fishes.Clear();

        //        foreach (var fish in value.Fishes)
        //        {
        //            if (fish.Id > 0)
        //            {
        //                var existing = this._repository.Fishes.FirstOrDefault(x => x.Id == fish.Id);
        //                tank.Fishes.Add(existing);
        //            }
        //            else
        //            {
        //                this._repository.Fishes.Add(fish);
        //                tank.Fishes.Add(fish);
        //            }
        //        }
        //    }

        //    this._repository.SaveChanges();

        //    this._foodChainService.EatFish();

        //    return this.Ok();
        //}

        //// DELETE: api/tank/1
        //[HttpDelete("{id:int}")]
        //public IActionResult Delete(int id)
        //{
        //    var tank = this._repository.Tanks.FirstOrDefault(x => x.Id == id);

        //    if (tank == null)
        //        return this.HttpNotFound();

        //    this._repository.Tanks.Remove(tank);
        //    this._repository.SaveChanges();

        //    return this.Ok();
        //}

        #endregion
    }
}