using System.Collections.Generic;
using System.Collections.Specialized;
using NUnit.Framework;
using Services.Core.DataModels.CelestialBodies;
using Services.Core.DataModels.CelestialObjects;
using Services.Core.DataModels.Units;

namespace Tests.Services.UniverseEf.Seed
{
    public static class ValidSimpleData
    {

        public static PlanetarySystem TestStarSystem() =>
            new PlanetarySystem
                {
                    ID = 1,
                    Planets = SimpleDataA(),
                    Comets = new List<Comet>(),
                    Stars = new List<Star>(),
                    Satellites = new List<Satellite>()
                };
        
        /// <summary>
        /// For 'AverageMoonsDwarfPlanet' should return 5 precisely 4,5714285714285714285714285714286.
        /// </summary>
        /// <returns><see cref="List"/> with type <see cref="Planet"/></returns>
        public static List<Planet> DwarfPlanetSimpleData() =>
            new List<Planet>
                {
                    new Planet {KnownMoons = 5, Classification = "Dwarf planet",},
                    new Planet {KnownMoons = 7, Classification = "Dwarf planet",},
                    new Planet {KnownMoons = 8, Classification = "Dwarf planet",},
                    new Planet {KnownMoons = 4, Classification = "Dwarf planet",},
                    new Planet {KnownMoons = 6, Classification = "Dwarf planet",},
                    new Planet {KnownMoons = 2, Classification = "Dwarf planet",},
                    new Planet {KnownMoons = 0, Classification = "Dwarf planet"},
                };
        
        
        public static List<string> ExpectedAlphabetical = new List<string>
            {
                "PlanetA",
                "PlanetB",
                "PlanetC",
                "PlanetD",
                "PlanetE",
                "PlanetF",
                "PlanetG"
            };
        
        public static List<float> ExpectedAverageTempDwarf() => new List<float>
        {
            {25F},
            {7.5F},
            {2.5F},
            {24F},
            {-21F},
            {-13F},
            {10F}
        };

        /// <summary>
        /// Valid data partial models.
        /// <list type="Bullet">
        /// <item>
        /// <description>Total moons - 67</description>
        /// </item>
        /// <item>
        /// <item>
        /// <description>Total dwarf planet moons - 32</description>
        /// </item>
        /// <description>Total planets - 7</description>
        /// </item>
        /// <item>
        /// <description>Total dwarf planets - 7</description>
        /// </item>
        /// <item>
        /// <description>Positive temp - 3</description>
        /// </item>
        /// <item>
        /// <description>Negative temp - 4</description>
        /// </item>
        /// <item>
        /// <description> Unmeasured temp - 1</description>
        /// </item>
        /// <item>
        /// <description></description>
        /// </item>
        /// </list>
        /// </summary>
        public static List<Planet> SimpleDataA() =>
            new List<Planet>
                {
                    new Planet {KnownMoons = 5, Classification = "Dwarf planet", Name = "DwarfA",
                        SurfaceTemperature = new Temperature {Min = 0, Max = 10}, PlanetarySystemID = 1, OrbitDistance = 210,},
                    new Planet {KnownMoons = 7, Classification = "Dwarf planet",Name = "DwarfB",
                        SurfaceTemperature = new Temperature {Min = -31, Max = 5}, PlanetarySystemID = 1 , OrbitDistance = 110,},
                    new Planet {KnownMoons = 8, Classification = "Dwarf planet",Name = "DwarfC",
                        SurfaceTemperature = new Temperature {Min = 0, Max = -21},PlanetarySystemID = 1, OrbitDistance = 120,},
                    new Planet {KnownMoons = 4, Classification = "Dwarf planet",Name = "DwarfD",
                        SurfaceTemperature = new Temperature {Min = 0, Max = 24}, PlanetarySystemID = 1, OrbitDistance = 150,},
                    new Planet {KnownMoons = 6, Classification = "Dwarf planet",Name = "DwarfE",
                        SurfaceTemperature = new Temperature {Min = -5, Max = 10}, PlanetarySystemID = 1, OrbitDistance = 190,},
                    new Planet {KnownMoons = 2, Classification = "Dwarf planet",Name = "DwarfF",
                        SurfaceTemperature = new Temperature {Min = -5, Max = 20}, PlanetarySystemID = 1, OrbitDistance = 89,},
                    new Planet {KnownMoons = 0, Classification = "Dwarf planet", Name = "DwarfG",
                        SurfaceTemperature = new Temperature {Min = 0, Max = 25}, PlanetarySystemID = 1,OrbitDistance = 55, },
                    new Planet
                        {
                            PlanetarySystemID = 1,
                            OrbitDistance = 1,
                            OrbitPeriod = 87,
                            KnownMoons = 4,
                            Name = "PlanetA",
                            Classification = "Planet",
                            Diameter = 47,
                            SurfaceTemperature = new Temperature {Min = -1, Max = 10}
                        },
                    new Planet
                        {
                            PlanetarySystemID = 1,
                            OrbitDistance = 3,
                            OrbitPeriod = 87,
                            KnownMoons = 2,
                            Name = "PlanetB",
                            Classification = "Planet",
                            Diameter = 47,
                            SurfaceTemperature = new Temperature {Min = -1, Max = 0}
                        },
                    new Planet
                        {
                            PlanetarySystemID = 1,
                            OrbitDistance = 59,
                            OrbitPeriod = 87,
                            KnownMoons = 9,
                            Name = "PlanetC",
                            Classification = "Planet",
                            Diameter = 47,
                            SurfaceTemperature = new Temperature {Min = 25, Max = 10}
                        },
                    new Planet
                        {
                            PlanetarySystemID = 1,
                            OrbitDistance = 7,
                            OrbitPeriod = 87,
                            KnownMoons = 5,
                            Name = "PlanetD",
                            Classification = "Planet",
                            Diameter = 47,
                            SurfaceTemperature = new Temperature {Min = 0, Max = 0}
                        },
                    new Planet
                        {
                            PlanetarySystemID = 1,
                            OrbitDistance = 9,
                            OrbitPeriod = 87,
                            KnownMoons = 0,
                            Name = "PlanetE",
                            Classification = "Planet",
                            Diameter = 47,
                            SurfaceTemperature = new Temperature {Min = -35, Max = 0}
                        },
                    new Planet
                        {
                            PlanetarySystemID = 1,
                            OrbitDistance = 15,
                            OrbitPeriod = 87,
                            KnownMoons = 8,
                            Name = "PlanetF",
                            Classification = "Planet",
                            Diameter = 47,
                            SurfaceTemperature = new Temperature {Min = -9, Max = 0}
                        },
                    new Planet
                        {
                            PlanetarySystemID = 1,
                            OrbitDistance = 23,
                            OrbitPeriod = 87,
                            KnownMoons = 7,
                            Name = "PlanetG",
                            Classification = "Planet",
                            Diameter = 47,
                            SurfaceTemperature = new Temperature {Min = 1, Max = 10}
                        }
                };

        /// <summary>
        /// Test data to search for planet names containing characters p and t case insensitive
        /// and name length.
        /// Set contains 4 names matching pt constraint.
        /// </summary>
        /// <returns></returns>
        public static List<Planet> NameConstraintData() =>
            new List<Planet>
                {
                    new Planet
                        {
                            Name = "dskwyFjwfgPmT",
                            Classification = "Planet",
                        },
                    new Planet
                        {
                            Name = "tdfjjDlgkp",
                            Classification = "Planet",
                        },
                    new Planet
                        {
                            Name = "dfsgPnt",
                            Classification = "Planet",
                        },
                    new Planet
                        {
                            Name = "hfgjdT",
                            Classification = "Planet",
                        },
                    new Planet
                        {
                            Name = "pttww",
                            Classification = "Planet",
                        },
                    new Planet
                        {
                            Name = "sdfgsdPP",
                            Classification = "Planet",
                        },
                    new Planet
                        {
                            Name = "MyWorld",
                            Classification = "Planet",
                        },
                    new Planet
                        {
                            Name = "Yod",
                            Classification = "Planet",
                        },
                    new Planet
                        {
                            Name = "Eu",
                            Classification = "Planet",
                        },
                };
    }
}