using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterYourPlants
{
    public enum WaterState
    {
        idle = 0,rest = 2, stop = 3, start = 1
    }
    public class Plant
    {
        public string PlantId { get; set; }
        public string PlantState { get; set; }
       
        public DateTime StopTime { get; set; }
        public DateTime StartTime { get; set; }
        


    }
}
