using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WaterMyPlants.Models
{
    public class MyPlant
    {
        public string PlantId { get; set; }
        public int WaterState { get; set; }

        public DateTime StopTime { get; set; }
        public DateTime StartTime { get; set; }
    }
}