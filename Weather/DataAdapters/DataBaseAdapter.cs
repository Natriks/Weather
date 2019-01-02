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
using SQLite;
using Weather.Models;

namespace Weather.DataAdapters
{
    public class DataBaseAdapter
    {
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        string name = "TownWeathers.db";
        public bool CreateTable()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(path, name)))
                {
                    connection.CreateTable<TownWeather>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException", ex.Message);
                return false;
            }
        }
        public bool GetTablesInfo()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(path, name)))
                {
                    TableMapping tableMapping =  connection.GetMapping<TownWeather>();
                    connection.Query<TownWeather>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA = 'TownsWeather'");
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException", ex.Message);
                return false;
            }
        }
        public bool DropTable()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(path, name)))
                {
                    connection.DropTable<TownWeather>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException", ex.Message);
                return false;
            }
        }
        public bool Insert(TownWeather town)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(path, name)))
                {
                    connection.Insert(town);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException", ex.Message);
                return false;
            }
        }
        public List<TownWeather> GetTableTowns()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(path, name)))
                {
                    return connection.Table<TownWeather>().ToList(); ;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException", ex.Message);
                return null;
            }
        }
        public bool Update(TownWeather town)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(path, name)))
                {
                    connection.Query<TownWeather>("UPDATE TownWeather set Temp=?,Temp_min=?,Temp_max=?,Pressure=?,Sea_level=?, Grnd_level=?, Humidity=?, Main=?, Description=?,Tommorow_temp=?,Tommorow_main=?,Tommorow_description=?,After_tmr_temp=?,After_tmr_main=?,After_tmr_description=?,After2_tmr_temp=?,After2_tmr_main=?,After2_tmr_description=? WHERE Id=?",
                        town.Temp, town.Temp_min, town.Temp_max, town.Pressure, town.Sea_level, town.Grnd_level, town.Humidity, town.Main, town.Description, town.Tommorow_temp, town.Tommorow_main, town.Tommorow_description, town.After_tmr_temp, town.After_tmr_main, town.After_tmr_description, town.After2_tmr_temp, town.After2_tmr_main, town.After2_tmr_description, town.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException", ex.Message);
                return false;
            }
        }
        public bool UpdateByName(TownWeather town)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(path, name)))
                {
                    connection.Query<TownWeather>("UPDATE TownWeather set Temp=?,Temp_min=?,Temp_max=?,Pressure=?,Sea_level=?, Grnd_level=?, Humidity=?, Main=?, Description=?,Tommorow_temp=?,Tommorow_main=?,Tommorow_description=?,After_tmr_temp=?,After_tmr_main=?,After_tmr_description=?,After2_tmr_temp=?,After2_tmr_main=?,After2_tmr_description=? WHERE Name=?",
                        town.Temp, town.Temp_min, town.Temp_max, town.Pressure, town.Sea_level, town.Grnd_level, town.Humidity, town.Main, town.Description, town.Tommorow_temp, town.Tommorow_main, town.Tommorow_description, town.After_tmr_temp, town.After_tmr_main, town.After_tmr_description, town.After2_tmr_temp, town.After2_tmr_main, town.After2_tmr_description, town.Name);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException", ex.Message);
                return false;
            }
        }
        public bool Delete(TownWeather town)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(path, name)))
                {
                    connection.Delete(town);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException", ex.Message);
                return false;
            }
        }
        public bool DeleteByName(string name)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(path, name)))
                {
                    connection.Query<TownWeather>("DELETE FROM TownWeather WHERE Name=?", name);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException", ex.Message);
                return false;
            }
        }
        public bool FindTownById(int id)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(path, name)))
                {
                    connection.Query<TownWeather>("SELECT * FROM TownWeather WHERE id=?", id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException", ex.Message);
                return false;
            }
        }
    }
}