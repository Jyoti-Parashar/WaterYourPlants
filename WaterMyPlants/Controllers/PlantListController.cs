using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaterMyPlants.Models;

namespace WaterMyPlants.Controllers
{
    public class PlantListController : Controller
    {
        // GET: PlantList
        public ActionResult Index()
        {
            List<MyPlant> plantList = new PlantList().GetPlantList();
            return View(plantList);
        }
        public ActionResult Start(string id)
        {
           MyPlant plant = new PlantList().GetPlantById( id);
           return View();
        }
    }
}