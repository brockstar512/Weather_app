using MyWeatherApp.Model;
using MyWeatherApp.ViewModel.Helper;
using MyWeatherApp.ViewModel.Helper.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyWeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        private string query;

        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        private CurrentConditions currentCondition;

        public CurrentConditions CurrentCondition
        {
            get { return currentCondition; }
            set
            {
                currentCondition = value;

                OnPropertyChanged("CurrentCondition");
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                if(selectedCity != null && !DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                { 
                    OnPropertyChanged("SelectedCity");
                    GetCurrentConditions();
                }
            }
        }

        public SearchCommand SearchCommand { get; set; }

        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                selectedCity = new City
                {
                    LocalizedName = "--"
                };

                currentCondition = new CurrentConditions
                {
                    WeatherText = "Partly cloudy",
                    HasPrecipitation = true,
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = "21"
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        private async void GetCurrentConditions()
        {

            Query = string.Empty;
            CurrentCondition = await AccuWeatherHelper.GetCurrentWeatherConditions(SelectedCity.Key);
            //Cities.Clear();
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);

            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
