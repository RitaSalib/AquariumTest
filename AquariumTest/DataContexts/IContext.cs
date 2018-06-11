using AquariumTest.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AquariumTest.DataContexts
{
    public interface IContext : IDisposable
    {
        DbSet<Tank> Tanks { get; set; }
        DbSet<Fish> Fishes { get; set; }
        DbSet<Species> Species { get; set; }
        DbSet<SpeciesPredator> SpeciesPredators { get; set; }

        int SaveChanges();
    }
}