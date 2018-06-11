using AquariumTest.DataContexts;
using AquariumTest.DataStores;
using AquariumTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AquariumTest.Repositories
{
    public class Repository : IRepository
    {
        private readonly IContext _context;
        private readonly IDataStore _dataStore;

        public DbSet<Tank> Tanks { get; set; }
        public DbSet<Fish> Fishes { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<SpeciesPredator> SpeciesPredators { get; set; }

        public Repository(IContext context, IDataStore dataStore)
        {
            this._context = context;
            this._dataStore = dataStore;

            this.Tanks = this._context.Tanks;
            this.Fishes = this._context.Fishes;
            this.Species = this._context.Species;
            this.SpeciesPredators = this._context.SpeciesPredators;

            this.Tanks.AttachRange(this._dataStore.Tanks);
            this.Fishes.AttachRange(this._dataStore.Fishes);
            this.Species.AttachRange(this._dataStore.Species);
            this.SpeciesPredators.AttachRange(this._dataStore.SpeciesPredators);
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public void Dispose()
        {

        }
    }
}
