using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Net;
using Newtonsoft.Json;
using Weather.Models;

namespace Weather.DataAdapters
{
    public class OpenWeatherDataAdapter
    {
        private static string apiKey = "672a2f587ad80683e9b348ed309893d6";
        private static string Request (string town) => "http://api.openweathermap.org/data/2.5/forecast?q=" + town + "&units=metric&appid=" + apiKey;
        private static string Image (string imageName) => "http://openweathermap.org/img/w/" + imageName + ".png";
        public static string GetWeatherData(string townName = "Moscow")
        {
            string temp, result = null;
            try
            {
                URL uRL = new URL(Request(townName));
                HttpURLConnection connection = (HttpURLConnection)uRL.OpenConnection();
                BufferedReader br = new BufferedReader(new InputStreamReader(connection.InputStream));
                while((temp = br.ReadLine()) != null)
                    result += temp;
                br.Close();
                connection.Disconnect();
            }
            catch (Exception ex)
            {
                Log.Debug("ExecuteRequest on OpenWeatherDataAdapter", ex.Message);
            }
            return result;
        }
    }
}