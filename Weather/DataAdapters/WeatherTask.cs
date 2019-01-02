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
using Newtonsoft.Json;
using Weather.Models;

namespace Weather.DataAdapters
{
    public class WeatherTask : AsyncTask<string, Java.Lang.Void, string>
    {
        private MainActivity activity;
        private OpenWeatherData weatherData;

        public WeatherTask(MainActivity activity)
        {
            this.activity = activity;
            this.weatherData = new OpenWeatherData();
        }

        protected override void OnPreExecute()
        {
            base.OnPreExecute();
        }

        protected override string RunInBackground(params string[] @params)
        {
            string townName = @params[0];

            return OpenWeatherDataAdapter.GetWeatherData(townName);

        }

        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);
            if (result == null)
                return;
            weatherData = JsonConvert.DeserializeObject<OpenWeatherData>(result);

            TownWeather tw = new TownWeather()
            {
                Name = weatherData.city.name,
                Temp = weatherData.list[0].main.temp,
                Temp_min = weatherData.list[0].main.temp_min,
                Temp_max = weatherData.list[0].main.temp_max,
                Pressure = weatherData.list[0].main.pressure,
                Humidity = weatherData.list[0].main.humidity,
                Sea_level = weatherData.list[0].main.sea_level,
                Grnd_level = weatherData.list[0].main.grnd_level,
                Main = weatherData.list[0].weather[0].main,
                Description = weatherData.list[0].weather[0].description,

                Tommorow_temp = weatherData.list[1].main.temp,
                Tommorow_main = weatherData.list[1].weather[0].main,
                Tommorow_description = weatherData.list[1].weather[0].description,

                After_tmr_temp = weatherData.list[2].main.temp,
                After_tmr_main = weatherData.list[2].weather[0].main,
                After_tmr_description = weatherData.list[2].weather[0].description,

                After2_tmr_temp = weatherData.list[3].main.temp,
                After2_tmr_main = weatherData.list[3].weather[0].main,
                After2_tmr_description = weatherData.list[3].weather[0].description,
            };

            activity.dbAdapter.UpdateByName(tw);

            activity.LoadData();
        }
    }
}