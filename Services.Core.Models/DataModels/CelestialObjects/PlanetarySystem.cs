using System.Collections.Generic;
using Services.Core.DataModels.CelestialBodies;

namespace Services.Core.DataModels.CelestialObjects
{
    public class PlanetarySystem : AstronomicalObject
    {
        public virtual ICollection<Planet> Planets { get; set; } = new List<Planet>();
        public virtual ICollection<Star> Stars { get; set; } = new List<Star>();
        public virtual ICollection<Satellite> Satellites { get; set; } = new List<Satellite>();
        public virtual ICollection<Comet> Comets { get; set; } = new List<Comet>();

    }
}