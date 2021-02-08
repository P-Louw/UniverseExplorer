using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            _db.Planets.OrderBy(v => v.Name)
                .ToList();

        
        public IEnumerable<Planet> PlanetsTempAboveZero() =>
            _db.Planets.Where(p => p.SurfaceTemperature.Max > 0)
                .Select(r => r);

        
        public IEnumerable<Planet> PlanetNameLetterConstraint() =>
            _db.Planets.AsEnumerable()
                .Where(p => "pt".All(c =>
                    p.Name.Contains(c, StringComparison.OrdinalIgnoreCase)));

        
        public IEnumerable<Planet> PlanetsNameLengthDescending() =>
            _db.Planets.OrderByDescending(v => v.Name.Length);

        
        public IEnumerable<Planet> PlanetDistanceToSunAscending() =>
            _db.Planets.OrderBy(p => p.OrbitDistance);

        
        public IEnumerable<Planet> DwarfPlanetByMoonAmount() =>
            _db.Planets
                .Where(p => EF.Functions.Like(p.Classification, "Dwarf planet"))
                .OrderBy(d => d.KnownMoons);


        public int TotalMoons() =>
            _db.Planets.Sum(m => m.KnownMoons);

        
        public IEnumerable<Planet> DwarfPlanetsSortedDiameter() =>
            _db.Planets.Where(p => EF.Functions.Like(p.Classification, "Dwarf planet"))
                .OrderBy(v => v.Diameter);

        
        public double AverageMoonsPerDwarfPlanet() =>
            _db.Planets.Where(e =>
                    EF.Functions.Like(e.Classification, "Dwarf planet"))
                .Average(p => p.KnownMoons);


        public Dictionary<string, List<TemperatureResults>> AverageSurfaceTemps() =>
            _db.Planets.Where(v =>
                    v.SurfaceTemperature.Max != v.SurfaceTemperature.Min)
                .AsEnumerable()
                .Select(e => new TemperatureResults()
                    {
                        Classification = e.Classification,
                        AverageTemperature = e.SurfaceTemperature.Min != 0
                            ? (e.SurfaceTemperature.Max + e.SurfaceTemperature.Min) / 2
                            : e.SurfaceTemperature.Max,
                        Max = e.SurfaceTemperature.Max,
                        Min = e.SurfaceTemperature.Min
                    })
                .GroupBy(c => c.Classification)
                .ToDictionary(
                    group => group.Key.ToString(),
                    results => results.ToList());


        public int TotalBodyAmount() =>
            _db.PlanetarySystems.Select(l => l.Planets.Count +
                                             l.Satellites.Count +
                                             l.Comets.Count)
                .FirstOrDefault();

        
        public TwoPlanetDifference ClosestNeighbourPlanets() =>
            _db.Planets.SelectMany(p1 =>
                    _db.Planets, (p1, p2) => new
                    {
                        p1,
                        p2
                    })
                .Where(p => p.p1.ID != p.p2.ID)
                .Select(measure => new
                    {
                        PlanetA = measure.p1,
                        PlanetB = measure.p2,
                        MeasuredDistance = Math.Abs(measure.p1.OrbitDistance - measure.p2.OrbitDistance)
                    })
                .OrderBy(e => e.MeasuredDistance)
                .Select(res => new TwoPlanetDifference()
                    {
                        PlanetA = res.PlanetA,
                        PlanetB = res.PlanetB,
                        MeasuredDistance = res.MeasuredDistance
                    })
                .First();
    }
}