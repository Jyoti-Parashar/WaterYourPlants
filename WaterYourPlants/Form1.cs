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





        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = "Start Watering"+"\n";
        }

        private async void newStart_Click(object sender, EventArgs e)
        {
            timer1.Start();
            lblStatus.Text = "Working......";
            var progress = new Progress<ProgressReport>();
            progress.ProgressChanged += (o, report) => {
                lblStatus.Text = string.Format("Processing.........{0}%", report.PercentComplete);
                if (checkBox1.Checked)
                {
                    progressBar1.Value = report.PercentComplete;
                    progressBar1.Update();
                }
                if (checkBox2.Checked)
                {
                    progressBar2.Value = report.PercentComplete;
                    progressBar2.Update();
                }
                if (checkBox3.Checked)
                {
                    progressBar3.Value = report.PercentComplete;
                    progressBar3.Update();
                }
                
            };
            await  ProcessData(list, progress);
            lblStatus.Text = "Done";
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            list.Add(new Plants() { PlantId = "P1", waterStatus = false });
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            list.Add(new Plants() { PlantId = "P2", waterStatus = false });
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            
                timer1.Stop();
                
                
            
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            list.Add(new Plants() { PlantId = "P3", waterStatus = false });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;
            
            if (seconds>=10 )
            {
              
                timer1.Stop();
                System.Windows.Forms.MessageBox.Show("Time is up!Press stop");
            }
            
        }
    }


    public class Plants
    {
        public string PlantId { get;  set; }
        public bool waterStatus { get;  set; }
        public DateTime startTime { get; set; }
       
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
//        Plants p1 = new Plants() { PlantId = "P1", waterStatus = false };
//        label2.Text = "watering";
//        CallProcess();
//        label2.Text = "finished";

//    }
//    if (checkBox2.Checked)
//    {
//        Plants p2 = new Plants() { PlantId = "P2", waterStatus = false };
//        label3.Text = "watering";
//        CallProcess();
//        label3.Text = "finished";
//    }


//}
