using System.Collections.Generic;
using Services.Core.DataModels.CelestialObjects;
using Services.Core.DataModels.Units;

namespace Services.Core.DataModels.CelestialBodies
{
    public abstract class AstronomicalBody
    {
        public int ID { get; set; }
        public int PlanetarySystemID { get; set; }
        public string Name { get; set; }
        public string Classification { get; set; }
        public long Diameter { get; set; }
        public Temperature? SurfaceTemperature { get; set; }
        public PlanetarySystem System { get; set; }
    }
}