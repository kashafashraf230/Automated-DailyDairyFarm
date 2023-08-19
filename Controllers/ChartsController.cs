using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using DailyDairyAuto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DailyDairyAuto.Controllers
{
    public class ChartsController : Controller
    {
        //GET: Charts
        private readonly ILogger<ChartsController> _logger;

        public ChartsController(ILogger<ChartsController> logger)
        {
            _logger = logger;
        }
        public IActionResult MilkCharts()
        {

            int monthlyTotal = 0;
            List<string> Months = new List<string>();
            List<string> Milk = new List<string>();
            List<int> MilkInt = new List<int>();
            using (var reader = new StreamReader("wwwroot/MonthlyMilk.csv"))

            {


                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    Months.Add(values[0]);
                    Milk.Add(values[1]);
                }
                for (int i = 0; i < Milk.Count(); i++)
                {
                    monthlyTotal += int.Parse(Milk[i]);
                    MilkInt.Add(int.Parse(Milk[i]));
                }
            }

            int weeklyTotal = 0;
            List<string> Week = new List<string>();
            List<string> Milk2 = new List<string>();
            List<int> MilkInt2 = new List<int>();
            using (var reader2 = new StreamReader("wwwroot/WeeklyMilk.csv"))

            {


                while (!reader2.EndOfStream)
                {
                    var line1 = reader2.ReadLine();
                    DateTime dt = DateTime.Now;
                    Debug.WriteLine("current month " + dt.Month);
                    Debug.WriteLine("line " + line1);

                   

                    if (line1 == dt.Month.ToString()+',')
                    {
                        Debug.WriteLine("in the if block");

                        while (!reader2.EndOfStream)
                        {
                            var linenew = reader2.ReadLine();
                            var values2 = linenew.Split(',');
                            Debug.WriteLine("in the loop");

                            Week.Add(values2[0]);
                            Milk2.Add(values2[1]);
                        }


                    }

                }
                for (int i = 0; i < Milk2.Count(); i++)
                {
                    weeklyTotal += int.Parse(Milk2[i]);
                    MilkInt2.Add(int.Parse(Milk2[i]));
                }
            }


            List<string> Daily = new List<string>();
            List<string> Milk3 = new List<string>();
            List<int> MilkInt3 = new List<int>(); 
            using (var reader3 = new StreamReader("wwwroot/DailyMilk.csv"))

            {


                while (!reader3.EndOfStream)
                {
                    var line = reader3.ReadLine();

                    var values = line.Split(',');

                    Daily.Add(values[0]);
                    Milk3.Add(values[1]);
                }
                for (int i = 0; i < Milk3.Count(); i++)
                {

                    MilkInt3.Add(int.Parse(Milk3[i]));
                }

            }


            // annual milk production

            List<string> years = new List<string>();
            List<string> Milk4 = new List<string>();
            List<float> MilkInt4 = new List<float>();
            using (var reader = new StreamReader("wwwroot/AnnualMilk.csv"))

            {


                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    years.Add(values[0]);
                    Milk4.Add(values[1]);
                }
                for (int i = 0; i < Milk4.Count(); i++)
                {
                    
                    MilkInt4.Add(float.Parse(Milk4[i]));
                }
            }

            string jsonMon = JsonConvert.SerializeObject(Months, Formatting.Indented);

            string jsonWeek = JsonConvert.SerializeObject(Week , Formatting.Indented);

            string jsonDaily = JsonConvert.SerializeObject(Daily, Formatting.Indented);

            string jsonYear = JsonConvert.SerializeObject(years, Formatting.Indented);

            ViewData["monthly"] = monthlyTotal;
            ViewData["weekly"] = weeklyTotal;
            ViewData["MonthsList"] = jsonMon;
            ViewData["MonthlyMilk"] = MilkInt;
            ViewData["WeekList"] = jsonWeek;
            ViewData["WeeklyMilk"] = MilkInt2;
            ViewData["DailyList"] = jsonDaily;
            ViewData["DailyMilk"] = MilkInt3;
            ViewData["YearList"] = jsonYear;
            ViewData["YearlyMilk"] = MilkInt4;

            return View();
        }
    }
}