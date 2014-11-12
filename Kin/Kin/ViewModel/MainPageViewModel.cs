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


        public static List<WeatherCurrent> FetchData()
        {
            List<WeatherCurrent> lst = AsyncFetchData().Result;

            return lst;

            //var task = Task.Run(async () =>
            //{
            //    return await AsyncFetchData();
            //});
            //return task.Result;
        }

        public async static Task<List<WeatherCurrent>> AsyncFetchData()
        //private static async void AsyncFetchData()
        {

            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            HttpResponseMessage response = await client.GetAsync("http://api.openweathermap.org/data/2.5/weather?q=Seoul,kr&units=metric");
            response.EnsureSuccessStatusCode();
            
            string jsonResponse = await response.Content.ReadAsStringAsync();
            WeatherCurrent weather = JsonConvert.DeserializeObject<WeatherCurrent>(jsonResponse);

            List<WeatherCurrent> lst = new List<WeatherCurrent>();

            lst.Add(weather);

            return lst;
        }
    }
}
