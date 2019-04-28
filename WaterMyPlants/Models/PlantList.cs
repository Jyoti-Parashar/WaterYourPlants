using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WaterMyPlants.Models
{
    public class PlantList
    {
        public enum State
        {
            idle = 0, watering = 1, rest = 2, stop = 3, start = 4
        }
        public List<MyPlant> GetPlantList()
        {
            List<MyPlant> myPlants = new List<MyPlant>();
            myPlants.Add(new MyPlant() { PlantId = "P1", WaterState=(int)State.idle, StartTime=DateTime.Now,StopTime = DateTime.Today });
            myPlants.Add(new MyPlant() { PlantId = "P2", WaterState = (int)State.idle,StartTime=DateTime.Now,StopTime = DateTime.Today});
            return myPlants;
        }

        public MyPlant GetPlantById(string Id)
        {
            MyPlant plant = new MyPlant();
            plant= new PlantList().GetPlantList().Where(x => x.PlantId==Id).First();
            int a = GetPlantState(plant);
            return plant;
        }

        public int GetPlantState(MyPlant plant)
        {
            int waterState = 0;
            plant.StartTime = System.DateTime.Now;
            var diff = plant.StartTime.Subtract(plant.StopTime);
            var diff2 = plant.StartTime.Subtract(plant.StopTime.AddSeconds(30));
            if (diff.Hours > 6)
            {
                plant.WaterState = (int)State.idle;
                plant.WaterState = (int)State.start;
                waterState = plant.WaterState;
               
            }
            else if (diff2.Seconds > 30)
            {
                //timer1.Start();
                plant.WaterState = (int)State.rest;
                plant.WaterState = (int)State.start;
                waterState = plant.WaterState;
            }
            else
            {
                plant.WaterState = (int)State.rest;
                waterState = plant.WaterState;
            }


            return waterState;
            
        }
    }
    
}