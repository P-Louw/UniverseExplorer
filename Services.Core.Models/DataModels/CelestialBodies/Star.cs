using System.Collections.Generic;
using Services.Core.DataModels.Units;

namespace Services.Core.DataModels.CelestialBodies
{
    public class Star : AstronomicalBody
    {
     
        public int AmountOrbitPlanet { get; set; }
        public Temperature CoreTemperature { get; set; }
        public ulong Age { get; set; }
    }
}

