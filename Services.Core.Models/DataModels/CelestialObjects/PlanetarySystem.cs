using System.Collections.Generic;
using Services.Core.DataModels.CelestialBodies;

namespace Services.Core.DataModels.CelestialObjects
{
    public class PlanetarySystem : AstronomicalObject
    {
        public ICollection<Planet> Planets { get; set; } = new List<Planet>();
        public ICollection<Star> Stars { get; set; } = new List<Star>();
        public ICollection<Satellite> Satellites { get; set; } = new List<Satellite>();
        public ICollection<Comet> Comets { get; set; } = new List<Comet>();

    }
}