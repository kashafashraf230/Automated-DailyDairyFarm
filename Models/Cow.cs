using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using DailyDairyAuto.Models;


namespace DailyDairyAuto.Models
{
    public class Cow
    {
        private int _id { get; set; }
        private float temperature { get; set; }
        private string breed { get; set; }
        private float milk_production { get; set; }
        private float respiration { get; set; }
        private int walking_capacity { get; set; }
        private float sleep_duration { get; set; }
        private int body_condition { get; set; }
        private int heart_rate { get; set; }
        private float eating_duration { get; set; }
        private float lying_duration { get; set; }
        private float ruminating { get; set; }
        private int rumen_fill { get; set; }


        public Cow healthFileReader()
        {
            string text = File.ReadAllText(@"C:\Users\DELL\Documents\DailyDairyAuto\wwwroot\realtime.csv");
            Debug.WriteLine(text);

            string[] fileData = text.Split(',');

            Cow cow = new Cow();
            cow._id = int.Parse(fileData[0]);
            cow.temperature= float.Parse(fileData[1]);
            cow.breed= fileData[2];
            cow.milk_production = float.Parse(fileData[3]);
            cow.respiration = int.Parse(fileData[4]);
            cow.walking_capacity = int.Parse(fileData[5]);
            cow.sleep_duration = float.Parse(fileData[6]);
            cow.body_condition = int.Parse(fileData[7]);
            cow.heart_rate = int.Parse(fileData[8]);
            cow.eating_duration = float.Parse(fileData[9]);
            cow.lying_duration = float.Parse(fileData[10]);
            cow.ruminating= float.Parse(fileData[11]);
            cow.rumen_fill = int.Parse(fileData[12]);

            return cow;

        }


    }
}