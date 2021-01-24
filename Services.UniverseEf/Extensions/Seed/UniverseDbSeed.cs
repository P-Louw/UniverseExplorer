using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Services.Core.DataModels;
using Services.Core.DataModels.CelestialBodies;
using Services.Core.DataModels.CelestialObjects;
using Services.Core.DataModels.Units;

namespace Services.UniverseService.Extensions.Seed
{
    public static class UniverseDbSeed
    {
        /// <summary>
        /// Seed EF db with a json file consisting of arrays of:
        /// <list type="bullet">
        /// <item><description><see cref="Planet"/></description></item>
        /// <item><description> <see cref="Star"/></description></item>
        /// <item><description><see cref="PlanetarySystem"/></description></item>
        /// </list>
        /// </summary>
        /// <param name="builder"><see cref="ModelBuilder"/> exposed during OnModelCreating.</param>
        /// <returns><see cref="ModelBuilder"/></returns>
        public static ModelBuilder SeedSunPlanetsJson(this ModelBuilder builder)
        {
            var tokenDict = ImportBuilder.SeedDataJTokens();
            var planets = tokenDict["Planet"];
            var stars = tokenDict["Star"];

            builder.Entity<PlanetarySystem>(entity =>
                    {
                        builder.Entity<PlanetarySystem>(entity =>
                            {
                                entity.HasData(
                                    new PlanetarySystem
                                        {
                                            ID = 1,
                                            Name = "Solar system",
                                            Age = 4600000000
                                        });
                                entity.HasMany<Planet>()
                                    .WithOne(p => p.System)
                                    .HasForeignKey(k => k.PlanetarySystemID);
                            });
                    }
            );
            builder.Entity<Planet>(entity =>
                {
                    foreach (var entry in planets)
                        {
                            entity.HasData(
                                new Planet
                                    {
                                        ID = (int) entry.SelectToken("id"),
                                        PlanetarySystemID = 1,//(int) entry.SelectToken("planetarySystemID"),
                                        Name = (string) entry.SelectToken("name"),
                                        Classification = (string) entry.SelectToken("classification"),
                                        Diameter = (long) entry.SelectToken("diameter"),
                                        KnownMoons = (int) entry.SelectToken("knownMoons"),
                                        OrbitDistance = (ulong) entry.SelectToken("orbitDistance"),
                                        OrbitPeriod = (ulong) entry.SelectToken("orbitPeriod"),
                                    });
                            entity.OwnsOne<Temperature>(t => t.SurfaceTemperature)
                                .HasData(new
                                    {
                                        PlanetID = (int) entry.SelectToken("id"),
                                        Max = (float?) entry.SelectToken("surfaceTemperature.max"),
                                        Min = (float?) entry.SelectToken("surfaceTemperature.min")
                                    });
                        }
                });
            builder.Entity<Star>(entity =>
                {
                    foreach (var entry in stars)
                        {
                            entity.HasData(
                                new Star
                                    {
                                        Name = (string) entry.SelectToken("name"),
                                        ID = (int) entry.SelectToken("id"),
                                        Age = (ulong) entry.SelectToken("age"),
                                        PlanetarySystemID = (int) entry.SelectToken("planetarySystemID"),
                                        Classification = (string) entry.SelectToken("classification"),
                                        Diameter = (long) entry.SelectToken("diameter"),
                                        AmountOrbitPlanet = (int) entry.SelectToken("amountOrbitPlanet")
                                    });
                            entity.OwnsOne<Temperature>(t => t.CoreTemperature)
                                .HasData(new
                                    {
                                        StarID = (int) entry.SelectToken("id"),
                                        Max = (float?) entry.SelectToken("coreTemperature.max"),
                                        Min = (float?) entry.SelectToken("coreTemperature.min")
                                    });
                            entity.OwnsOne<Temperature>(t => t.SurfaceTemperature)
                                .HasData(new
                                    {
                                        StarID = (int) entry.SelectToken("id"),
                                    });
                        }
                });

            return builder;
        }

        
        /// <summary>
        /// Seed 2 <see cref="Planet"/> and a <see cref="PlanetarySystem"/> for minor tests EF.
        /// </summary>
        /// <param name="builder"> <see cref="ModelBuilder"/> exposed during OnModelCreating.</param>
        /// <returns><see cref="ModelBuilder"/></returns>
        public static ModelBuilder SeedSunPlanetsCoded(this ModelBuilder builder)
        {
            builder.Entity<PlanetarySystem>(entity =>
                    {
                        builder.Entity<PlanetarySystem>().HasData(
                                new PlanetarySystem
                                    {
                                        ID = 1,
                                        Name = "Solar system",
                                        Age = 4600000000
                                    }); 
                    }
            );
            builder.Entity<Planet>(entity =>
                {
                    entity.HasData(
                        new Planet
                            {
                                ID = 1,
                                PlanetarySystemID = 1,
                                Name = "Mercury",
                                Classification = nameof(Planet),
                                Diameter = 4879,
                                KnownMoons = 0,
                                OrbitDistance = 57900000,
                                OrbitPeriod = (ulong) 88.0
                            });
                    entity.OwnsOne<Temperature>(t => t.SurfaceTemperature)
                        .HasData(new
                            {
                                PlanetID = 1,
                                Max = (float?) 427,
                                Min = (float?) -173
                            });
                });
            builder.Entity<Planet>(entity =>
                {
                    entity.HasData(
                        new Planet
                            {
                                ID = 2,
                                PlanetarySystemID = 1,
                                Name = "Earth",
                                Classification = nameof(Planet),
                                Diameter = 12756000000,
                                KnownMoons = 1,
                                OrbitDistance = (ulong) 149.6,
                                OrbitPeriod = (ulong) 365.2,
                            });
                    entity.OwnsOne<Temperature>(t => t.SurfaceTemperature)
                        .HasData(new
                            {
                                PlanetID = 2,
                                Max = (float?) 59,
                                Min = (float?) -88
                            });
                });
            builder.Entity<Star>(entity => { });

            return builder;
        }
    }
}