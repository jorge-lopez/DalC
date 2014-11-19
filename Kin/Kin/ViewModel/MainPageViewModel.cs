using Kin.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Kin.ViewModel
{
    class MainPageViewModel
    {
        public List<WeatherCurrent> CurrentConditions { get; set; }

        public MainPageViewModel()
        {
            CurrentConditions = new List<WeatherCurrent>();
        }


        public static async Task<List<WeatherCurrent>> FetchData()
        {

            Task<string> jsonDataTask = AsyncFetchData();
            string jsonData = await jsonDataTask;
            
            WeatherCurrent weather = JsonConvert.DeserializeObject<WeatherCurrent>(jsonData.ToString());
            List<WeatherCurrent> lst = new List<WeatherCurrent>();

            lst.Add(weather);
            return lst;

        }

        private static async Task<string> AsyncFetchData()
        {
            string url = "http://api.openweathermap.org/data/2.5/weather?q=Seoul,kr&units=metric";            
            HttpClient client = new HttpClient();
            string jsonResponse = await client.GetStringAsync(url);           
            return jsonResponse;
        }
    }
}
