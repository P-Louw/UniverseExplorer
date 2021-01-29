using System;
using System.Collections.Generic;
using Services.Core.DataModels.CelestialBodies;
using Services.Core.DataModels.Units;

namespace Services.Core.Models.DTO
{
    public class TemperatureReport
    {
        public Dictionary<string ,IEnumerable<Temperature>> CategoryTemperatures { get; set; }

        public TemperatureReport()
        {
            
        }

        public TemperatureReport(Func<Dictionary<string,List<TemperatureResults>>> adder)
        {
        }
        
    }
}