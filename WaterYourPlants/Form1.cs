using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterYourPlants;

namespace WaterYourPlant
{
    public partial class Form1 : Form
    {
        #region Declaration

        
        public enum waterstate
        {
            idle = 0, rest = 2, stop = 3, start = 1
        }
        
        public Form1()
        {
            InitializeComponent();
        }
        #endregion
        #region Form Load Events


        private void Form1_Load(object sender, EventArgs e)
        {
            List<Plant> list = new List<Plant>();
            list.Add(new Plant() { PlantId = "Rose", PlantState = Enum.GetName(typeof(waterstate), 0), StartTime=DateTime.Now,StopTime=DateTime.Today});
            list.Add(new Plant() { PlantId = "Lily", PlantState = Enum.GetName(typeof(waterstate), 0), StartTime = DateTime.Now,StopTime=DateTime.Today});
            list.Add(new Plant() { PlantId = "Tulip", PlantState = Enum.GetName(typeof(waterstate), 0), StartTime = DateTime.Now, StopTime = DateTime.Today });
            list.Add(new Plant() { PlantId = "Daisy", PlantState = Enum.GetName(typeof(waterstate), 0), StartTime = DateTime.Now, StopTime = DateTime.Today });
            dataGridView1.DataSource = list.OrderBy(o => o.PlantId).ToList();
            dataGridView1.Rows[0].Selected = true;
        }
        #endregion

        #region Common Function

      
        private string SetPlantWaterState(Plant plant)
        {
            string waterState = "";
            plant.StartTime = System.DateTime.Now;
            var diff = plant.StartTime.Subtract(plant.StopTime);
            if (diff.Hours > 6)
            {
                plant.PlantState = Enum.GetName(typeof(waterstate), 0);
                plant.PlantState= Enum.GetName(typeof(waterstate), 1);
                waterState = plant.PlantState;
            }
            else if(diff.TotalSeconds>30)
            {
                plant.PlantState = Enum.GetName(typeof(waterstate), 2);
                plant.PlantState = Enum.GetName(typeof(waterstate), 1);
                waterState = plant.PlantState;
            }
            return waterState;
        }

        private static async Task<string> WaterYourPlant(string id)
        {
            string s = "";

            await Task.Delay(10000); // 10 second delay

            s = ("Watering of "+id+ " Finished after 10 Seconds");
            return s;
        }
     
        #endregion
        #region Button Events
        private void btnStop_Click(object sender, EventArgs e)
        {
            List<Plant> list = new List<Plant>(); ;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Plant item = row.DataBoundItem as Plant;
                list.Add(item);
            }

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Plant plant = row.DataBoundItem as Plant;
                plant.StopTime = DateTime.Now;
                plant.PlantState = Enum.GetName(typeof(waterstate), 2);
                var itemToRemove = list.SingleOrDefault(r => r.PlantId == plant.PlantId);
                list.Remove(itemToRemove);
                list.Add(plant);
            }
            dataGridView1.DataSource = list.OrderBy(o => o.PlantId).ToList(); ;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string s = "";
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    Plant plant = row.DataBoundItem as Plant;
                    if (plant != null && SetPlantWaterState(plant).Equals
                        (Enum.GetName(typeof(waterstate), 1)))
                    {
                        s = "Start Watering......";
                        lblStatus.Text = s;
                        s = await WaterYourPlant(plant.PlantId);
                    }
                    else if (plant != null && SetPlantWaterState(plant).Equals
                        (Enum.GetName(typeof(waterstate), 2)))
                    {
                        s = "Wait for 30 sec......";
                    }
                    lblStatus.Text = s;

                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please Select a Plant","Message");
            }
           
            lblStatus.Text = s;
        }

        #endregion
    }
}



