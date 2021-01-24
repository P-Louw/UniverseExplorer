using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Services.Core.DataModels.Units;

namespace Services.Core.DataModels.CelestialBodies
{
    public class Planet : AstronomicalBody
    {
        public ulong OrbitDistance { get; set; }
        public ulong OrbitPeriod{ get; set; }
        public int KnownMoons { get; set; }
    }
}