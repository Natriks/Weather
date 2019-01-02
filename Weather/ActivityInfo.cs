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

namespace Weather
{
    [Activity(Label = "ActivityInfo")]
    public class ActivityInfo : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_info);

            TextView txtName = FindViewById<TextView>(Resource.Id.textViewName);
            TextView txtMain = FindViewById<TextView>(Resource.Id.textViewMain);
            TextView txtDesc = FindViewById<TextView>(Resource.Id.textViewDesc);
            TextView txtTemp = FindViewById<TextView>(Resource.Id.textViewTemp);
            TextView txtMin_temp = FindViewById<TextView>(Resource.Id.textViewMin_temp);
            TextView txtMax_temp = FindViewById<TextView>(Resource.Id.textViewMax_temp);
            TextView txtPressure = FindViewById<TextView>(Resource.Id.textViewPressure);
            TextView txtHumidity = FindViewById<TextView>(Resource.Id.textViewHumidity);
            TextView txtSeaLevel = FindViewById<TextView>(Resource.Id.textViewSeaLevel);
            TextView txtGroundLevel = FindViewById<TextView>(Resource.Id.textViewGroundLevel);

            LinearLayout linearLayout = FindViewById<LinearLayout>(Resource.Id.horizontalLinearLayout);
            
            txtName.Text = Intent.GetStringExtra("name");
            txtMain.Text = Intent.GetStringExtra("main");
            txtDesc.Text = Intent.GetStringExtra("description");
            txtTemp.Text = "Temperature: " + Intent.GetDoubleExtra("temp", 0).ToString() + "°C";
            txtMax_temp.Text = "Max Temperature: " + Intent.GetDoubleExtra("temp_max", 0).ToString() + "°C";
            txtMin_temp.Text = "Min Temperature: " + Intent.GetDoubleExtra("temp_min", 0).ToString() + "°C";
            txtPressure.Text = "Pressure: " + Intent.GetDoubleExtra("pressure", 0).ToString();
            txtHumidity.Text = "Humidity: " + Intent.GetIntExtra("humidity", 0).ToString();
            txtSeaLevel.Text = "Sea Level: " + Intent.GetDoubleExtra("sea_level", 0).ToString();
            txtGroundLevel.Text = "Ground Level: " + Intent.GetDoubleExtra("ground_level", 0).ToString();

            linearLayout.AddView(getDayLayout(
                DateTime.Now.AddDays(1).ToString("dd.MM"),
                Intent.GetDoubleExtra("tommorow_temp", 0).ToString(),
                Intent.GetStringExtra("tommorow_description").ToString()
                ));
            linearLayout.AddView(getDayLayout(
                DateTime.Now.AddDays(2).ToString("dd.MM"),
                Intent.GetDoubleExtra("after_tmr_temp", 0).ToString(),
                Intent.GetStringExtra("after_tmr_description").ToString()
                ));
            linearLayout.AddView(getDayLayout(
                DateTime.Now.AddDays(3).ToString("dd.MM"),
                Intent.GetDoubleExtra("after2_tmr_temp", 0).ToString(),
                Intent.GetStringExtra("after2_tmr_description").ToString()
                ));
        }

        LinearLayout getDayLayout(String date, String temp, String desc)
        {
            LinearLayout l = new LinearLayout(this);
            l.Orientation = Orientation.Vertical;
            LinearLayout.LayoutParams lparams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent, 1);
            l.LayoutParameters = lparams;

            TextView txtDay = new TextView(this);
            lparams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
            lparams.Gravity = GravityFlags.CenterHorizontal;
            lparams.RightMargin = 25;
            lparams.LeftMargin = 25;
            txtDay.TextSize = 20;
            txtDay.LayoutParameters = lparams;
            txtDay.Text = date;

            TextView txtTemp = new TextView(this);
            txtTemp.TextSize = 15;
            txtTemp.LayoutParameters = lparams;
            txtTemp.Text = temp + "°C";

            TextView txtDesc = new TextView(this);
            txtDesc.TextSize = 15;
            txtDesc.LayoutParameters = lparams;
            txtDesc.Text = desc;

            l.AddView(txtDay);
            l.AddView(txtTemp);
            l.AddView(txtDesc);
            return l;
        }
    }
}