using AquariumTest.DataStores;
using AquariumTest.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AquariumTest.DataContexts
{
    public class Context : DbContext, IContext
    {
        private readonly IDataStore _dataStore;

        public DbSet<Tank> Tanks { get; set; }
        public DbSet<Fish> Fishes { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<SpeciesPredator> SpeciesPredators { get; set; }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tank>().HasIndex(x => x.Id);
            modelBuilder.Entity<Tank>().HasMany(x => x.Fishes).WithOne(x => x.Tank).HasForeignKey(x => x.TankId);

            modelBuilder.Entity<Fish>().HasIndex(x => x.Id);
            modelBuilder.Entity<Fish>().HasOne(x => x.Species).WithMany();
            modelBuilder.Entity<Fish>().HasOne(x => x.Tank).WithMany();

            modelBuilder.Entity<Species>().HasIndex(x => x.Id);
            modelBuilder.Entity<Species>().HasMany(x => x.Predators).WithOne();

            modelBuilder.Entity<SpeciesPredator>().HasOne(x => x.Species).WithMany(x => x.Predators);
            modelBuilder.Entity<SpeciesPredator>().HasOne(x => x.Predator).WithMany();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyDatabase");
            base.OnConfiguring(optionsBuilder);
        }

        public override void Dispose()
        {
            var fishes = this.Fishes.ToList();
            var tanks = this.Tanks.ToList();
            var species = this.Species.ToList();
            var speciesPredators = this.SpeciesPredators.ToList();

            foreach (var speciesPredator in speciesPredators)
            {
                speciesPredator.Species = null;
                speciesPredator.Predator = null;
            }

            foreach (var speciese in species)
            {
                speciese.Predators.Clear();
            }

            foreach (var fish in fishes)
            {
                fish.Species = null;
                fish.Tank = null;
            }

            foreach (var tank in tanks)
            {
                tank.Fishes.Clear();
            }

            if (this._dataStore != null)
            {
                this._dataStore.SpeciesPredators = speciesPredators;
                this._dataStore.Species = species;
                this._dataStore.Fishes = fishes;
                this._dataStore.Tanks = tanks;
            }

            base.Dispose();
        }
    }
}