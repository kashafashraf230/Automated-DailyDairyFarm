using DailyDairyAuto.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DailyDairyAuto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
                    var line = reader2.ReadLine();
                    DateTime dt = DateTime.Now;
                    Debug.WriteLine("current month" + dt.Month);
                    Debug.WriteLine("line" + line);

                    if (line == (dt.Month.ToString() + ","))
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

            string jsonMon = JsonConvert.SerializeObject(Months, Formatting.Indented);


            ViewData["monthly"] = monthlyTotal;
            ViewData["weekly"] = weeklyTotal;
            ViewData["MonthsList"] = jsonMon;
            ViewData["MonthlyMilk"] = MilkInt;
            ViewData["WeekList"] = Week.ToArray();
            ViewData["WeeklyMilk"] = MilkInt2;


            return View();
        }

        public IActionResult Login()
        { return View(); }

        public IActionResult ViewCattle()
        { return View(); }

        [HttpPost]
        public IActionResult LoginVerify()
        {
            String login = Request.Form["username"];
            String password = Request.Form["password"];

            if (login == "admin" && password == "admin")
                return RedirectToAction("Index");
            else
            {
                ViewBag.ErrMsg = "Invalid User Id/Password";
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public IActionResult HealthStatusPrediction()
        {
            String[] labels = new String[4];
            
            // Some hard coded values for testing of model
            
            /// 1
            /// 
            var sampleData1 = new MLModelCattle.ModelInput()
            {
                Body_temperature = 32,
                Breed_type = @"Normal Breed",
                Milk_production = 24.1F,
                Respiratory_rate = 37F,
                Walking_capacity = 11987F,
                Sleeping_duration = 4.2F,
                Body_condition_score = 2F,
                Heart_rate = 62F,
                Eating_duration = 3.9F,
                Lying_down_duration = 12.2F,
                Ruminating = 5.8F,
                Rumen_fill = 4F,
            };

            //Load model and predict output
            var result1 = MLModelCattle.Predict(sampleData1);
            labels[0] = result1.PredictedLabel;
            //ViewBag.healthStatus[0] = labels[0] ;
            Debug.WriteLine(labels[0]);


            //// 2
            ///

            var sampleData2 = new MLModelCattle.ModelInput()
            {
                Body_temperature = 60,
                Breed_type = @"Normal Breed",
                Milk_production = 9.1F,
                Respiratory_rate = 3F,
                Walking_capacity = 1987F,
                Sleeping_duration = 4.2F,
                Body_condition_score = 2F,
                Heart_rate = 23F,
                Eating_duration = 3.9F,
                Lying_down_duration = 12.2F,
                Ruminating = 5.8F,
                Rumen_fill = 4F,
            };

            //Load model and predict output
            var result2 = MLModelCattle.Predict(sampleData2);
            labels[1] = result2.PredictedLabel;
            //ViewBag.healthStatus[1] = labels[1] ;
            Debug.WriteLine(labels[1]);

            //// 3
            ///

            var sampleData3 = new MLModelCattle.ModelInput()
            {
                Body_temperature = 60,
                Breed_type = @"Normal Breed",
                Milk_production = 9.1F,
                Respiratory_rate = 3F,
                Walking_capacity = 1187F,
                Sleeping_duration = 1.2F,
                Body_condition_score = 2F,
                Heart_rate = 23F,
                Eating_duration = 3.9F,
                Lying_down_duration = 12.2F,
                Ruminating = 5.8F,
                Rumen_fill = 4F,
            };

            //Load model and predict output
            var result3 = MLModelCattle.Predict(sampleData3);
            labels[2] = result3.PredictedLabel;
            //ViewBag.healthStatus[1] = labels[1] ;
            Debug.WriteLine(labels[2]);

            //// 4
            ///

            var sampleData4 = new MLModelCattle.ModelInput()
            {
                Body_temperature = 32,
                Breed_type = @"Cross Breed",
                Milk_production = 24.1F,
                Respiratory_rate = 37F,
                Walking_capacity = 11987F,
                Sleeping_duration = 4.2F,
                Body_condition_score = 2F,
                Heart_rate = 62F,
                Eating_duration = 3.9F,
                Lying_down_duration = 12.2F,
                Ruminating = 5.8F,
                Rumen_fill = 4F,
            };

            //Load model and predict output
            var result4 = MLModelCattle.Predict(sampleData4);
            labels[3] = result4.PredictedLabel;
            //ViewBag.healthStatus[1] = labels[1] ;
            Debug.WriteLine(labels[3]);

            ViewBag.healthStatus = labels;
            return View("Alerts");

        }

        [HttpPost]

        public IActionResult CheckMilkQuality()
        {

            String[] labels = new String[4];

            //Load sample data
            var sampleData1 = new MLModelMilk.ModelInput()
            {
                PH = 6.6F,
                Temprature = 36F,
                Fat = 0F,
                Turbidity = 1F,
                Colour = 253F,
            };

            //Load model and predict output
            var result1 = MLModelMilk.Predict(sampleData1);
            labels[0] = result1.PredictedLabel;
            Debug.WriteLine(labels[0]);


            /// 2
            /// 

            var sampleData2 = new MLModelMilk.ModelInput()
            {
                PH = 9.6F,
                Temprature = 36F,
                Fat = 3F,
                Turbidity = 1F,
                Colour = 150F,
            };

            //Load model and predict output
            var result2 = MLModelMilk.Predict(sampleData2);
            labels[1] = result2.PredictedLabel;
            Debug.WriteLine(labels[1]);

            /// 3
            /// 

            var sampleData3 = new MLModelMilk.ModelInput()
            {
                PH = 5.0F,
                Temprature = 36F,
                Fat = 1F,
                Turbidity = 1F,
                Colour = 253F,
            };

            //Load model and predict output
            var result3 = MLModelMilk.Predict(sampleData3);
            labels[2] = result3.PredictedLabel;
            Debug.WriteLine(labels[2]);

            /// 4
            /// 

            var sampleData4 = new MLModelMilk.ModelInput()
            {
                PH = 6.6F,
                Temprature = 30F,
                Fat = 0F,
                Turbidity = 1F,
                Colour = 253F,
            };

            //Load model and predict output
            var result4 = MLModelMilk.Predict(sampleData4);
            labels[3] = result4.PredictedLabel;
            Debug.WriteLine(labels[3]);

            ViewBag.milkQuality = labels;

            return View("MilkAlerts");

        }
    }
}
