using Services.Core.DataModels.CelestialBodies;

namespace Services.Core.Models.DTO
{
    public class TwoPlanetDifference
    {
        public Planet PlanetA { get; set; }
        public Planet PlanetB { get; set; }
        public ulong MeasuredDistance { get; set; }
    }
}