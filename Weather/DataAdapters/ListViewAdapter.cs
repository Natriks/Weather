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
using Java.Lang;
using Weather.Models;

namespace Weather.DataAdapters
{
    public class ViewHolder:Java.Lang.Object
    {
        public TextView txtName { get; set; }
        public TextView txtTemp { get; set; }
    }
    class ListViewAdapter:BaseAdapter
    {
        private Activity activity;
        private List<TownWeather> towns;

        public ListViewAdapter(Activity activity, List<TownWeather> towns)
        {
            this.activity = activity;
            this.towns = towns;
        }

        public override int Count => towns.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return towns[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.listViewDataTemplate, parent, false);
            TextView txtName = view.FindViewById<TextView>(Resource.Id.textViewTownName);
            TextView txtTemp = view.FindViewById<TextView>(Resource.Id.textViewTownTemp);

            txtName.Text = towns[position].Name;
            txtTemp.Text = towns[position].Temp.ToString() + "°C";
            return view;
        }
    }
}