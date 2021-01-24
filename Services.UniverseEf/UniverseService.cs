using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Services.Core.DataModels.CelestialBodies;
using Services.Core.DataModels.Units;
using Services.Core.Models.DTO;
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
            _db.Planets.Where(p => "pt".All(c =>
                p.Name.Contains(c, StringComparison.OrdinalIgnoreCase)));

        public IEnumerable<Planet> PlanetsNameLengthDescending() =>
            _db.Planets.Select(p => p)
                .OrderByDescending(v => v.Name.Length);

        public IEnumerable<Planet> PlanetDistanceToSunAscending() =>
            _db.Planets.OrderBy(p => p.OrbitDistance);

        public IEnumerable<Planet> DwarfPlanetByMoonAmount() =>
            _db.Planets
                .AsEnumerable()
                .Where(p => p.Classification == "Dwarf planet")
                .OrderBy(d => d.KnownMoons);

        public int TotalMoons() =>
            // TODO: implement universe class to aggregate moon count.
            _db.Planets.Select(g => g.KnownMoons).Sum();

        public IEnumerable<Planet> DwarfPlanetsSortedDiameter() =>
            _db.Planets.Where(p => p.Classification == "Dwarf planet")
                .OrderBy(v => v.Diameter);

        public double AverageMoonsPerDwarfPlanet()
        {
            var inter = _db.Planets.Where(c =>
                    EF.Functions.Like(c.Classification, "Dwarf planet"))
                .Select(c => c.KnownMoons);

            var res = _db.Planets
                .Where(v => v.Classification == "Dwarf planet")
                .Average(p => p.KnownMoons);

            var sum = inter.Sum();

            var count = inter.Count();

            var prod = sum / count;
            return inter.Sum() / inter.Count();
        }

        public Dictionary<string, float?> AverageSurfaceTemps()
        {
            // TODO: remove dictionary, cleanup etc..
            /*var cached = _db.PlanetarySystems
                .Select(x => x)
                .Where(v => v.ID == 1);*/

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
                    {"Dwarf planet", 0},
                    {"Planet", 0},
                    {"Sun", 0}
                };
            var planets = _db.Planets;

            foreach (var temp in planets)
                {
                    var avgTemp = temp.SurfaceTemperature.Min.HasValue
                        ? (temp.SurfaceTemperature.Max + temp.SurfaceTemperature.Min) / 2
                        : temp.SurfaceTemperature.Max;

                    if (temp.Classification == "Dwarf planet")
                        resultDict[temp.Classification] += avgTemp;
                    else
                        resultDict["Planet"] += avgTemp;
                }

            resultDict["Sun"] += _db.PlanetarySystems
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


        public TwoPlanetDifference ClosestNeighbourPlanets() =>
            _db.Planets.SelectMany(p1 =>
                    _db.Planets, (p1, p2) => new
                    {
                        p1,
                        p2
                    })
                .Select(measure => new
                    {
                        measure,
                        distance = measure.p1.OrbitDistance - measure.p2.OrbitDistance
                    })
                .AsEnumerable()
                .Where(p => !object.ReferenceEquals(p.measure.p1, p.measure.p2))
                .OrderBy(e => e.distance)
                .Select(res => new TwoPlanetDifference()
                    {
                        PlanetA = res.measure.p1,
                        PlanetB = res.measure.p2,
                        MeasuredDistance = res.distance
                    })
                .First();


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