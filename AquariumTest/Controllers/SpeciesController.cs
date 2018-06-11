using AquariumTest.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AquariumTest.Controllers
{
    [Route("api/[controller]")]
    public class SpeciesController : Controller
    {
        private readonly IRepository _repository;

        public SpeciesController(IRepository repository)
        {
            this._repository = repository;
        }

        // GET: api/species
        [HttpGet]
        public IActionResult Get()
        {
            var result = this._repository.Species.ToList();

            foreach (var speciese in result)
            {
                speciese.Predators.Clear();
            }

            return this.Json(result);
        }

        // GET: api/species/1
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = this._repository.Species
                             .Include(x=>x.Predators)
                             .FirstOrDefault(x => x.Id == id);

            if (result == null)
                return this.NotFound();

            return this.Json(result);
        }
    }
}