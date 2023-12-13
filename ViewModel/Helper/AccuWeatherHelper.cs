using MyWeatherApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MyWeatherApp.ViewModel.Helper
{
    public class AccuWeatherHelper
    {
        public const string API_KEY = "dpH34j8JRNbChNFaQcIYfnL7lqjsOKNV";
        public const string BASEURL = "http://dataservice.accuweather.com/";
        public const string AUTO_COMPLETE = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENT_CONDITIONS = "currentconditions/v1/{0}?apikey={1}";

        public static async Task<List<City>>GetCities(string query)
        {
            List<City> cities = new List<City>();
            string url = BASEURL + string.Format(AUTO_COMPLETE, API_KEY, query);
            using(HttpClient client = new HttpClient())
            {
                Debug.WriteLine($"Url:{url}");
                var response = await client.GetAsync(url);

                string json = await response.Content.ReadAsStringAsync();
                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }
            return cities;
        }

        public static async Task<CurrentConditions> GetCurrentWeatherConditions(string citykey)
        {
            CurrentConditions currentConditions = new CurrentConditions();
            string url = BASEURL + string.Format(CURRENT_CONDITIONS, citykey, API_KEY);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                currentConditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(json)).FirstOrDefault();
            }
            return currentConditions;
        }
    }
}
