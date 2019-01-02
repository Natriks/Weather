using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Weather.DataAdapters;
using Weather.Models;

namespace Weather
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ListView listData;
        List<TownWeather> towns = new List<TownWeather>();
        public DataBaseAdapter dbAdapter;
        TextView editName;
        TextView txtName, txtTemp;
        int selectedPosition;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            listData = FindViewById<ListView>(Resource.Id.listViewTowns);

            dbAdapter = new DataBaseAdapter();
            dbAdapter.CreateTable();

            editName = FindViewById<EditText>(Resource.Id.textInputEditText);
            editName.Click += delegate
            {
                Button buttonEdit = FindViewById<Button>(Resource.Id.buttonEdit);
                if(buttonEdit.Text == "")
                editName.Text = null;
            };

            BindButtons();

            LoadData();

            listData.ItemClick += ListData_ItemClick;

            if (towns.Count == 0)
            {
                TownWeather moskow = new TownWeather("Moscow");
                towns.Add(moskow);
                dbAdapter.Insert(moskow);
                new WeatherTask(this).Execute(moskow.Name);
                TownWeather peterburg = new TownWeather("Saint Petersburg");
                towns.Add(peterburg);
                dbAdapter.Insert(peterburg);
                new WeatherTask(this).Execute(peterburg.Name);
            }
        }

        private void ListData_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            for (int i = 0; i < listData.Count; i++)
            {
                if (e.Position == i)
                {
                    if(selectedPosition == i)
                    {
                        Intent intent = new Intent(this, typeof(ActivityInfo));
                        intent.PutExtra("name",towns[selectedPosition].Name);
                        intent.PutExtra("pressure", towns[selectedPosition].Pressure);
                        intent.PutExtra("humidity", towns[selectedPosition].Humidity);
                        intent.PutExtra("main", towns[selectedPosition].Main);
                        intent.PutExtra("description", towns[selectedPosition].Description);
                        intent.PutExtra("temp", towns[selectedPosition].Temp);
                        intent.PutExtra("temp_max", towns[selectedPosition].Temp_max);
                        intent.PutExtra("temp_min", towns[selectedPosition].Temp_min);
                        intent.PutExtra("sea_level", towns[selectedPosition].Sea_level);
                        intent.PutExtra("ground_level", towns[selectedPosition].Grnd_level);

                        intent.PutExtra("tommorow_temp", towns[selectedPosition].Tommorow_temp);
                        intent.PutExtra("tommorow_main", towns[selectedPosition].Tommorow_main);
                        intent.PutExtra("tommorow_description", towns[selectedPosition].Tommorow_description);

                        intent.PutExtra("after_tmr_temp", towns[selectedPosition].After_tmr_temp);
                        intent.PutExtra("after_tmr_main", towns[selectedPosition].After_tmr_main);
                        intent.PutExtra("after_tmr_description", towns[selectedPosition].After_tmr_description);

                        intent.PutExtra("after2_tmr_temp", towns[selectedPosition].After2_tmr_temp);
                        intent.PutExtra("after2_tmr_main", towns[selectedPosition].After2_tmr_main);
                        intent.PutExtra("after2_tmr_description", towns[selectedPosition].After2_tmr_description);


                        StartActivity(intent);
                    }
                    selectedPosition = i;
                    listData.GetChildAt(i).SetBackgroundColor(Color.SlateGray);
                }
                else
                    listData.GetChildAt(i).SetBackgroundColor(Color.Transparent);
            }

            txtName = e.View.FindViewById<TextView>(Resource.Id.textViewTownName);
            txtTemp = e.View.FindViewById<TextView>(Resource.Id.textViewTownTemp);
        }

        public void LoadData()
        {
            towns = dbAdapter.GetTableTowns();
            listData.Adapter = new ListViewAdapter(this, towns);
        }

        private void BindButtons()
        {
            Button buttonAdd = FindViewById<Button>(Resource.Id.buttonAdd);
            Button buttonEdit = FindViewById<Button>(Resource.Id.buttonEdit);
            Button buttonDelete = FindViewById<Button>(Resource.Id.buttonDelete);

            buttonAdd.Click += ButtonAdd_Click;
            buttonEdit.Click += ButtonEdit_Click;
            buttonDelete.Click += ButtonDelete_Click;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (towns.Find(t => t.Name == editName.Text) != null)
            {
                editName.Text = null;
                return;
            }
            TownWeather town = new TownWeather(editName.Text);
            dbAdapter.Insert(town);
            new WeatherTask(this).Execute(town.Name);
            editName.Text = null;
            LoadData();
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (txtName == null) return;
            if ((sender as Button).Text == "Edit")
            {
                (sender as Button).Text = "Accept";
                editName.Text = txtName.Text;
            }
            else
            {
                (sender as Button).Text = "Edit";
                dbAdapter.Delete(towns[selectedPosition]);
                TownWeather town = new TownWeather(editName.Text);
                dbAdapter.Insert(town);
                new WeatherTask(this).Execute(town.Name);
                txtName.Text = null;
                LoadData();
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            dbAdapter.Delete(towns[selectedPosition]);
            LoadData();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
	}
}

