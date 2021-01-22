using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Services.Core.DataModels.CelestialBodies;
using Services.Core.DataModels.Units;
using Services.UniverseService.Context;

namespace Services.UniverseService
{
    public class UniverseService : IUniverseService
    {
        private readonly UniverseContext _db;

        public UniverseService(UniverseContext db) =>
            (_db) = (db);


        public IEnumerable<Planet> PlanetsOrderedAlphabetical() =>
            _db.Planets.Select(p => p)
                .OrderBy(v => v.Name)
                .ToList();

        public IEnumerable<Planet> PlanetsTempAboveZero() =>
            _db.Planets.Where(p => p.SurfaceTemperature.Max > 0)
                .Select(r => r);

        public IEnumerable<Planet> PlanetNameLetterConstraint() =>
            _db.Planets.AsEnumerable()
                .Where(p => "pt".All(c => p.Name.Contains(c)));

        public IEnumerable<Planet> PlanetsByNameDescending() =>
            _db.Planets.Select(p => p)
                .OrderByDescending(v => v.Name.Length);

        public IEnumerable<Planet> PlanetDistanceToSunAscending() =>
            _db.Planets.OrderBy(p => p.OrbitDistance);

        public IEnumerable<Planet> DwarfPlanetByMoonAmount() =>
            _db.Planets
                .Where(p => p.Classification == "Dwarf planet")
                .OrderBy(d => d.KnownMoons);

        public int TotalMoons() =>
            // TODO: implement universe class to aggregate moon count.
            _db.Planets.Select(g => g.KnownMoons)
                .Sum();

        public IEnumerable<Planet> DwarfPlanetsSortedDiameter() =>
            _db.Planets.Where(p => p.Classification == "Dwarf planet")
                .OrderBy(v => v.Diameter);

        public int AverageMoonsPerDwarfPlanet()
        {
            var inter = _db.Planets
                .Where(p => p.Classification == "Dwarf planet")
                .Select(c => c.KnownMoons);

            var res = _db.Planets
                .Where(v => v.Classification == "Dwarf planet")
                .Average(p => p.KnownMoons);

            return inter.Sum() / inter.Count();
        }

        public Dictionary<string, float?> AverageSurfaceTemps()
        {
            // TODO: remove dictionary, cleanup etc..
            var cached = _db.PlanetarySystems
                .Select(x => x)
                .Where(v => v.ID == 1);

            /*var k = from y in cached.Select(s => s.Planets)
                select (new
                    {
                        dwarfTemp = y.Where(v => v.Classification == "Dwarf planet")
                            .Select(v => v.SurfaceTemperature),
                        planetTemp = y.Where(v => v.Classification != "Dwarf planet")
                            .Select(v => v.SurfaceTemperature),
                    });*/
            /*into grp
            from q in grp.dwarfTemp
            from u in grp.planetTemp
            select new
                {
                    averageDwarfTmp = q.Min == 0 ? q.Max : (q.Max + q.Min / 2),
                    averagePlanetTmp = u.Min == 0 ? q.Max : (q.Max + q.Min / 2)
                };*/
            //var b = _db.Planets.Where(v => v.Name != "Dwarf planet");
            var resultDict = new Dictionary<string, float?>
                {
                    {"Dwarf planets", 0},
                    {"Planets", 0},
                    {"Sun", 0}
                };
            var planets = cached
                .Select(x => x.Planets)
                .FirstOrDefault();

            foreach (var temp in planets)
                {
                    var avgTemp = temp.SurfaceTemperature.Min.HasValue
                        ? (temp.SurfaceTemperature.Max + temp.SurfaceTemperature.Min) / 2
                        : temp.SurfaceTemperature.Max;

                    if (temp.Name == "Dwarf planet")
                        resultDict[temp.Name] += avgTemp;
                    else
                        resultDict["Planets"] += avgTemp;
                }

            resultDict["sun"] += cached
                .Select(v => v.Stars)
                .First().Select(s => s.CoreTemperature.Max).Sum();
            return resultDict;
        }


        public IEnumerable TotalBodyAmount() =>
            _db.PlanetarySystems.Where(d => d.ID == 1)
                .Select(sub => new
                    {
                        planetAmount = sub.Planets.Count,
                        cometAmount = sub.Comets.Count,
                        satelliteAmount = sub.Satellites.Count,
                        stars = sub.Stars.Count
                    }).AsEnumerable();


        public IEnumerable ClosestNeighbourPlanets()
        {
            var cachedEntities = _db.Planets;
            return cachedEntities
                .SelectMany(x =>
                        cachedEntities, (x, y) =>
                        new {x, y}
                )
                .Select(measure => new
                        {
                            measure, distance = Math.Abs(measure.x.OrbitDistance - measure.y.OrbitDistance)
                        }
                )
                .OrderBy(d => d.distance)
                .Select(entry => new
                    {
                        aPlanet = entry.measure.x,
                        otherPlanet = entry.measure.y,
                        shortDistance = entry.distance
                    });
        }

        public void TestMethod()
        {
            var a = _db.PlanetarySystems
                .Select(x => x.Planets)
                .FirstOrDefault();
            foreach (var plan in a)
                {
                    Console.WriteLine(plan);
                }
        }
    }
}