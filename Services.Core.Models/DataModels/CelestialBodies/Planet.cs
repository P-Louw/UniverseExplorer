using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Services.Core.DataModels.Units;

namespace Services.Core.DataModels.CelestialBodies
{
    public class Planet : AstronomicalBody
    {
        public long OrbitDistance { get; set; }
        public long OrbitPeriod{ get; set; }
        public int KnownMoons { get; set; }
    }
}