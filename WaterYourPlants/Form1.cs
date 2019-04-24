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

namespace WaterYourPlants
{
    public partial class Form1 : Form
    {
        List<Plants> list = new List<Plants>();
        int seconds,minutes,hours;
        public Form1()
        {
            InitializeComponent();
        }

        private Task ProcessData(List<Plants> list, IProgress<ProgressReport> progress)
        {
            int index = 1;
            int totalProgress = list.Count;
            var progressReport = new ProgressReport();
            return Task.Run(() =>
            {
                for (int i = 0; i < totalProgress; i++)
                {
                    progressReport.PercentComplete = ((index++ * 100) / totalProgress);
                    progress.Report(progressReport);

                    System.Threading.Thread.Sleep(10000);
                }
            }
            );
        }



        private static async Task<string> waterYourPlants(string id)
        {
            string s = "";
            
            await Task.Delay(10000); // 10 second delay

            s=("Watering of Plants() Finished after 10 Seconds");
            return s;
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            //label2.Text = "Start Watering"+"\n";
        }

        private async void newStart_Click(object sender, EventArgs e)
        {
            timer1.Start();
           
            foreach (var item in list)
            {
                if (item.state.Equals((int)waterstate.idle)|| item.state.Equals((int)waterstate.stop))
                {
                    string s = "";
                    //item.waterStatus = true;
                    item.startTime = System.DateTime.Now;
                    var diff = item.startTime.Subtract(item.stopTime);
                    if (diff.Seconds>30  && item.startTime > item.stopTime)
                    {
                        item.state = (int)waterstate.watering;
                        s = "Start Watering......";
                        lblStatus.Text = s;
                        s = await waterYourPlants(item.PlantId);
                        lblStatus.Text = s;
                    }
                    else
                    {
                        item.state = (int)waterstate.rest;
                        lblStatus.Text = "Wait for 30 sec";
                    }
                }

            }
            
            //var progress = new Progress<ProgressReport>();
            //progress.ProgressChanged += (o, report) => {
            //    lblStatus.Text = string.Format("Processing.........{0}%", report.PercentComplete);
            //    if (checkBox1.Checked)
            //    {
            //        progressBar1.Value = report.PercentComplete;
            //        progressBar1.Update();
            //    }
            //    if (checkBox2.Checked)
            //    {
            //        progressBar2.Value = report.PercentComplete;
            //        progressBar2.Update();
            //    }
            //    if (checkBox3.Checked)
            //    {
            //        progressBar3.Value = report.PercentComplete;
            //        progressBar3.Update();
            //    }

            //};
            //await  ProcessData(list, progress);


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            list.Add(new Plants() { PlantId = "P1", state=(int)waterstate.idle });
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            list.Add(new Plants() { PlantId = "P2" });
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

            timer1.Stop();
            foreach (var item in list)
            {
                item.stopTime = DateTime.Now;
                //item.waterStatus = false;
                item.state = (int)waterstate.stop;
            }



        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            list.Add(new Plants() { PlantId = "P3" });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //seconds++;
            
            //if (seconds>=10 )
            //{
              
            //    timer1.Stop();
            //    System.Windows.Forms.MessageBox.Show("Time is up!Press stop");
            //}
            
        }
    }

    public enum waterstate
    {
        idle = 0, watering = 1, rest = 2, stop = 3,start=4
    }
    public class Plants
    {
        public string PlantId { get;  set; }
        public int state { get; set; }
       // public bool waterStatus { get;  set; }
        public DateTime startTime { get; set; }
        public  DateTime stopTime { get; set; }
       
    }
    
}
//public static Task LongProcess()
//{
//    return Task.Run(() =>
//    {
//        System.Threading.Thread.Sleep(15000);
//    });
//}

//public async void CallProcess()
//{
//    await LongProcess();


//}

//private void btnStart_Click(object sender, EventArgs e)
//{

//    if (checkBox1.Checked)
//    {
//        Plants p1 = new Plants() { PlantId = "P1" };
//        label2.Text = "watering";
//        CallProcess();
//        label2.Text = "finished";

//    }
//    if (checkBox2.Checked)
//    {
//        Plants p2 = new Plants() { PlantId = "P2" };
//        label3.Text = "watering";
//        CallProcess();
//        label3.Text = "finished";
//    }


//}
