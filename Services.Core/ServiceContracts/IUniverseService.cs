using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Services.Core.DataModels.CelestialBodies;
using Services.Core.Models.DTO;

namespace Services.UniverseService
{
    public interface IUniverseService
    {
        IEnumerable<Planet> PlanetsOrderedAlphabetical();   // 2  [x]
        IEnumerable<Planet> PlanetsTempAboveZero();         // 3  [x]
        IEnumerable<Planet> PlanetNameLetterConstraint();   // 4  [x]
        IEnumerable<Planet> PlanetsNameLengthDescending();  // 5  [x]
        IEnumerable<Planet> PlanetDistanceToSunAscending(); // 6  [x]..
        IEnumerable<Planet> DwarfPlanetByMoonAmount();      // 7  [x]
        int TotalMoons();                                   // 8  [x]
        IEnumerable<Planet> DwarfPlanetsSortedDiameter();   // 9  [x]
        double AverageMoonsPerDwarfPlanet();                   // 10 [x]..
        Dictionary<string, List<TemperatureResults>> AverageSurfaceTemps();   // 11 [x]..
        int TotalBodyAmount();                      // 12 [x]..
        TwoPlanetDifference ClosestNeighbourPlanets();      // 13 []
    }
}