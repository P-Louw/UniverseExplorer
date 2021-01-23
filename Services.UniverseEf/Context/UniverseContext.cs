using System;
using Microsoft.EntityFrameworkCore;
using Services.Core.DataModels;
using Services.Core.DataModels.CelestialBodies;
using Services.Core.DataModels.CelestialObjects;
using Services.Core.DataModels.Units;
using Services.UniverseService.Extensions.Seed;

namespace Services.UniverseService.Context
{
    public class UniverseContext : DbContext
    {
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Comet> Comets { get; set; }
        public DbSet<Satellite> Satellites { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<PlanetarySystem> PlanetarySystems { get; set; }

        public UniverseContext(DbContextOptions<UniverseContext> options) :
            base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer(
                        "Server=localhost,1433;Database=PlanetDb;User=sa;Password=Pa55w0rd");
                }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlanetarySystem>()
                .HasKey(p => p.ID);
            modelBuilder.Entity<Planet>()
                .HasKey(p => p.ID);
            modelBuilder.Entity<Star>()
                .HasKey(p => p.ID);
            modelBuilder.Entity<Satellite>()
                .HasKey(p => p.ID);


            modelBuilder.Entity<Comet>(entity =>
                {
                    entity.OwnsOne<Temperature>(t => t.SurfaceTemperature);
                    entity.HasOne<PlanetarySystem>(p => p.System)
                        .WithMany(s => s.Comets)
                        .HasForeignKey(k => k.PlanetarySystemID);
                });
            modelBuilder.Entity<Satellite>(entity =>
                {
                    entity.OwnsOne<Temperature>(t => t.SurfaceTemperature);
                    entity.HasOne<PlanetarySystem>(p => p.System)
                        .WithMany(s => s.Satellites)
                        .HasForeignKey(k => k.PlanetarySystemID);
                });
            modelBuilder.Entity<Planet>(entity =>
                {
                    entity.OwnsOne<Temperature>(t => t.SurfaceTemperature);
                    entity.HasOne<PlanetarySystem>(p => p.System)
                        .WithMany(s => s.Planets)
                        .HasForeignKey(k => k.PlanetarySystemID);
                });
            modelBuilder.Entity<Star>(entity =>
                {
                    entity.OwnsOne<Temperature>(t => t.SurfaceTemperature);
                    entity.HasOne<PlanetarySystem>(p => p.System)
                        .WithMany(s => s.Stars)
                        .HasForeignKey(k => k.PlanetarySystemID);
                });

            modelBuilder.SeedSunPlanetsCoded();
            modelBuilder.SeedSunPlanetsJson();




            //base.OnModelCreating(modelBuilder);
        }
    }
}