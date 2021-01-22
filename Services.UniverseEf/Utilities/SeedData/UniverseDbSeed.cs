using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Services.Core.DataModels;
using Services.Core.DataModels.CelestialBodies;
using Services.Core.DataModels.CelestialObjects;
using Services.Core.DataModels.Units;

namespace Services.UniverseService
{
    public static class UniverseDbSeed
    {
        public static ModelBuilder SeedSunPlanetsJson(this ModelBuilder builder)
        {
            var tokenDict = ImportBuilder.SeedDataJTokens();
            var planets = tokenDict.Children()["Planet"].Values<Planet>();
            var k = tokenDict.Children()["Planet"];
            var star = tokenDict.Children().Values<Star>();
                    /*builder.Entity<Star>(entity =>
                        {
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
                                        });
                                });
                            
                            foreach (var entry in star)
                                {
                                    entity.HasData(
                                        new Star
                                            {
                                                ID = entry.ID,
                                                PlanetarySystemID = 1,
                                                Name = entry.Name,
                                                Classification = entry.Classification,
                                                Diameter = entry.Diameter,
                                                AmountOrbitPlanet = entry.AmountOrbitPlanet,
                                                Age = entry.Age,
                                            });
                                    entity.OwnsOne<Temperature>(t => t.CoreTemperature)
                                        .HasData(new
                                            {
                                                StarID = entry.ID,
                                                Max = (float?) entry.CoreTemperature.Max
                                            });
                                    entity.OwnsOne<Temperature>(s => s.SurfaceTemperature)
                                        .HasData(new
                                            {
                                                StarID = entry.ID,
                                                Max = (float?) entry.SurfaceTemperature.Max
                                            });
                                }
                   
                        });*/
                    
            foreach (var planet in k.Values())
                {
                    builder.Entity<Planet>(entity =>
                        {
                            entity.HasData(
                                new Planet
                                    {
                                        ID = planet["id"].Value<int>(),
                                        PlanetarySystemID = 1,
                                        Name = planet["came"].Value<string>(),
                                        Classification = planet["classification"].Value<string>(),
                                        Diameter = planet["diameter"].Value<long>(),
                                        KnownMoons = planet["knownMoons"].Value<int>(),
                                        OrbitDistance = planet["ordBitDistance"].Value<double>(),
                                        OrbitPeriod = planet["orbitPeriod"].Value<double>(),
                                    });
                            entity.OwnsOne<Temperature>(t => t.SurfaceTemperature)
                                .HasData(new
                                    {
                                        PlanetID = planet["id"].Value<int>(),
                                        Max = (float?) planet["surfaceTemperature"]["Max"].Value<float?>(),
                                        Min = (float?) planet["surfaceTemperature"]["Min"].Value<float?>()
                                    });
                        }); 
                }

            return builder;
        }
        
        public static ModelBuilder SeedSunPlanetsCoded(this ModelBuilder builder)
        {
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
                                /*entity.HasMany<Planet>()
                                    .WithOne(p => p.System)
                                    .HasForeignKey(k => k.PlanetarySystemID);*/
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
                                OrbitPeriod = 88.0
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
                                OrbitDistance = 149.6,
                                OrbitPeriod = 365.2,
                            });
                    entity.OwnsOne<Temperature>(t => t.SurfaceTemperature)
                        .HasData(new
                            {
                                PlanetID = 2,
                                Max = (float?) 59,
                                Min = (float?) -88
                            });
                });

            builder.Entity<Star>(entity =>
                {
                  
                });


            return builder;
        }
    }
}