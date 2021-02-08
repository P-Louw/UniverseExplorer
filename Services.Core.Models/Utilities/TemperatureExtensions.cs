using Services.Core.DataModels.CelestialBodies;
using Services.Core.DataModels.Units;
using Services.Core.Models.DTO;

namespace Services.UniverseService
{
    public static class TemperatureExtensions
    {
        public static float? AverageTempSingle(this AstronomicalBody tmp, float? max, float? min) =>
            (max != 0 ,min != 0) switch
                {
                    (true, true) => max + min / 2,
                    (true,_) => max,
                    (_, true) => min,
                };
        
        public static int ObservedTemp(this AstronomicalBody parent, float? temperature) =>
            temperature > 0
                ? 1
                : temperature < 0
                    ? -1
                    : 0;
    }
}