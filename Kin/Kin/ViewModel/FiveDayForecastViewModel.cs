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
    class FiveDayForecastViewModel
    {
        public WeatherFiveDayForecast BestFitted { get; set; }
        public static string[] WeatherConditions = { "Thunderstorm", "Drizzle", "Rain", "Snow", "Clouds" };

        private static string jsonData;

        public FiveDayForecastViewModel()
        {
            BestFitted = new WeatherFiveDayForecast();
        }


        public static async Task<WeatherFiveDayForecast> SearchBestFitted(string WeatherCondition)
        {

            if (jsonData == null || jsonData == "")
            {
                Task<string> jsonDataTask = AsyncFetchData();
                jsonData = await jsonDataTask;
            }            

            WeatherFiveDayForecast weather = JsonConvert.DeserializeObject<WeatherFiveDayForecast>(jsonData.ToString());

            weather.list = weather.list.Where(x => x.weather.ElementAt(0).main == WeatherCondition).ToList();
            return weather;

        }
        public static async Task<WeatherFiveDayForecast> Forecast()
        {

            if (jsonData == null || jsonData == "")
            {
                Task<string> jsonDataTask = AsyncFetchData();
                jsonData = await jsonDataTask;
            }

            WeatherFiveDayForecast weather = JsonConvert.DeserializeObject<WeatherFiveDayForecast>(jsonData.ToString());
                
            weather.list = weather.list.Where(x => x.weather.ElementAt(0).main == "hola").ToList();
            return weather;

        }

        private static async Task<string> AsyncFetchData()
        {
            string url = "http://api.openweathermap.org/data/2.5/forecast?q=Seoul";
            HttpClient client = new HttpClient();
            string jsonResponse = await client.GetStringAsync(url);
            return jsonResponse;
        }





    }
}
