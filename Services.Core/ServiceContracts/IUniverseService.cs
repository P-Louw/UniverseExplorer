using System.Collections;
using System.Collections.Generic;
using Services.Core.DataModels.CelestialBodies;

namespace Services.UniverseService
{
    public interface IUniverseService
    {
        IEnumerable<Planet> PlanetsOrderedAlphabetical();   // 2
        IEnumerable<Planet> PlanetsTempAboveZero();         // 3
        IEnumerable<Planet> PlanetNameLetterConstraint();   // 4
        IEnumerable<Planet> PlanetsByNameDescending();      // 5
        IEnumerable<Planet> PlanetDistanceToSunAscending(); // 6
        IEnumerable<Planet> DwarfPlanetByMoonAmount();      // 7
        int TotalMoons();                                   // 8
        IEnumerable<Planet> DwarfPlanetsSortedDiameter();   // 9
        int AverageMoonsPerDwarfPlanet();                   // 10
        Dictionary<string, float?> AverageSurfaceTemps();   // 11
        IEnumerable TotalBodyAmount();                      // 12
        IEnumerable ClosestNeighbourPlanets();      // 13
        void TestMethod();
    }
}