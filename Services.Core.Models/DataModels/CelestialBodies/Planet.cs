using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Services.Core.DataModels.Units;

namespace Services.Core.DataModels.CelestialBodies
{
    public class Planet : AstronomicalBody
    {
        public double OrbitDistance { get; set; }
        public double OrbitPeriod{ get; set; }
        public int KnownMoons { get; set; }
    }
}