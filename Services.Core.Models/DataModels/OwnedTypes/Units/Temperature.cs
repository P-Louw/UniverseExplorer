using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Services.Core.DataModels.Units
{
    public class Temperature
    {
        public Temperature()
        {
        }
        public Temperature(Temperature temp) =>
            (Max, Min) = (temp.Max, temp.Min);
        
        public float? Max { get; set; }
        public float? Min { get; set; }

      
    }
}