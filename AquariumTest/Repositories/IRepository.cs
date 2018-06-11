using AquariumTest.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AquariumTest.Repositories
{
    public interface IRepository : IDisposable
    {
        DbSet<Tank> Tanks { get; set; }
        DbSet<Fish> Fishes { get; set; }
        DbSet<Species> Species { get; set; }
        DbSet<SpeciesPredator> SpeciesPredators { get; set; }

        void SaveChanges();
    }
}