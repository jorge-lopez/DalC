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
        public List<ForecastTenDays> Forecast { get; set; }

        public MainPageViewModel()
        {
            CurrentConditions = new List<WeatherCurrent>();
            Forecast = new List<ForecastTenDays>();
        }



        public static async Task<List<WeatherCurrent>> FetchCurrentConditions()
        {
            string url = "http://api.openweathermap.org/data/2.5/weather?q=Seoul,kr&units=metric";
            Task<string> jsonDataTask = AsyncFetchData(url);
            string jsonData = await jsonDataTask;

            WeatherCurrent weather = JsonConvert.DeserializeObject<WeatherCurrent>(jsonData.ToString());
            List<WeatherCurrent> lst = new List<WeatherCurrent>();

            lst.Add(weather);
            return lst;

        }
        public static async Task<List<ForecastTenDays>> FetchTenDayForecast()
        {
            string url = "http://api.openweathermap.org/data/2.5/forecast/daily?q=Seoul&cnt=10&mode=json&units=metric";
            Task<string> jsonDataTask = AsyncFetchData(url);
            string jsonData = await jsonDataTask;

            WeatherTenDayForecast weather = JsonConvert.DeserializeObject<WeatherTenDayForecast>(jsonData.ToString());
            List<ForecastTenDays> lst = new List<ForecastTenDays>();

            foreach (WeatherTenDayForecast.List day in weather.list)
            {
                lst.Add(new ForecastTenDays(day.weather, day.temp, day.dt));
            }
            return lst;

        }

        private static async Task<string> AsyncFetchData(string url)
        {
            HttpClient client = new HttpClient();
            string jsonResponse = await client.GetStringAsync(url);
            return jsonResponse;
        }


        public class ForecastTenDays
        {
            public List<WeatherTenDayForecast.Weather> Description { get; set; }
            public WeatherTenDayForecast.Temp Temperature { get; set; }
            public DateTime Dt { get; set; }
            public string DayOfTheWeek { get; set; }
            

            public ForecastTenDays(List<WeatherTenDayForecast.Weather> Description, WeatherTenDayForecast.Temp Temperature, int UnixTimeStamp)
            {
                this.Description = Description;
                this.Temperature = Temperature;
                this.Dt = UnixTimeStampToDateTime(UnixTimeStamp);

                this.DayOfTheWeek = Dt.ToString("dddd");                
            }
            public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
            {
                // Unix timestamp is seconds past epoch
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
                return dtDateTime;
            }
        }

    }
}
