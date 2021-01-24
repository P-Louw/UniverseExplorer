using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using Microsoft.EntityFrameworkCore;
using Services.Core.DataModels.CelestialBodies;
using Services.Core.DataModels.Units;
using Services.Core.Models.DTO;
using Services.UniverseService.Context;
using UtilsTemp = Services.UniverseService.TemperatureExtensions;

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
                // .AsEnumerable()
                .Where(p => p.Classification == "Dwarf planet")
                .OrderBy(d => d.KnownMoons);

        public int TotalMoons() =>
            _db.Planets.Select(g => g.KnownMoons).Sum();

        public IEnumerable<Planet> DwarfPlanetsSortedDiameter() =>
            _db.Planets.Where(p => p.Classification == "Dwarf planet")
                .OrderBy(v => v.Diameter);

        public double AverageMoonsPerDwarfPlanet() =>
            _db.Planets.Where(e =>
                    EF.Functions.Like(e.Classification, "Dwarf planet"))
                .Average(p => p.KnownMoons);

        public IEnumerable<TemperatureResults> AverageSurfaceTemps() =>
            _db.Planets.Where(v =>
                    v.SurfaceTemperature.Max != v.SurfaceTemperature.Min)
                .AsEnumerable()
                .GroupBy(e => e.Classification)
                .Select(e => new TemperatureResults()
                    {
                        Classification = e.Key,
                        AverageTemperature = e.Sum(v => v.SurfaceTemperature.Max +
                                                        v.SurfaceTemperature.Min) / 2
                    });


        public IEnumerable TotalBodyAmount() =>
            _db.PlanetarySystems.Where(d => d.ID == 1)
                .Select(sub => new
                    {
                        planetAmount = sub.Planets.Count,
                        cometAmount = sub.Comets.Count,
                        satelliteAmount = sub.Satellites.Count,
                        stars = sub.Stars.Count
                    })
                .AsEnumerable();


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
                // .AsEnumerable()
                .Where(p => !object.ReferenceEquals(p.measure.p1, p.measure.p2))
                .OrderBy(e => e.distance)
                .Select(res => new TwoPlanetDifference()
                    {
                        PlanetA = res.measure.p1,
                        PlanetB = res.measure.p2,
                        MeasuredDistance = res.distance
                    })
                .First();



    }
}