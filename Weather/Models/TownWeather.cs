using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Weather.Models
{
    public class TownWeather
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Temp { get; set; }

        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public double Pressure { get; set; }
        public double Sea_level { get; set; }
        public double Grnd_level { get; set; }
        public int Humidity { get; set; }

        public string Main { get; set; }
        public string Description { get; set; }

        public double Tommorow_temp { get; set; }
        public string Tommorow_main { get; set; }
        public string Tommorow_description { get; set; }

        public double After_tmr_temp { get; set; }
        public string After_tmr_main { get; set; }
        public string After_tmr_description { get; set; }

        public double After2_tmr_temp { get; set; }
        public string After2_tmr_main { get; set; }
        public string After2_tmr_description { get; set; }

        public TownWeather()
        {

        }
        public TownWeather(string name)
        {
            Name = name;
        }
        public TownWeather(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}