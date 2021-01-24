using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Services.Core.DataModels.Units;

namespace Services.Core.Models.DTO
{
    public class TemperatureResults
    {
        public string Classification;
        public float? Max;
        public float? Min;
        public float? AverageTemperature; 

        /*public TemperatureResults(Action<Temperature> inst)
        {
            
        }*/

        /*public static float? Average(this Temperature f, float? max, float? min) =>
            (ObservedTemp(max), ObservedTemp(min)) switch
                {
                    (1, 0) => max,
                    (0, 1) => min,
                    (1, 1) => max + min / 2
                };

        public static int ObservedTemp(float? temperature) =>
            temperature > 0
                ? 1
                : temperature < 0
                    ? -1
                    : 0;*/
    }
}