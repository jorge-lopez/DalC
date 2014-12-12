using Kin.Model;
using Kin.ServiceReference1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public List<ScheduledTasks> ScheduledTasksList { get; set; }

        public MainPageViewModel()
        {
            CurrentConditions = new List<WeatherCurrent>();
            Forecast = new List<ForecastTenDays>();
            ScheduledTasksList = new List<ScheduledTasks>();
        }



        public static async Task<List<WeatherCurrent>> FetchCurrentConditions()
        {
            List<WeatherCurrent> lst = new List<WeatherCurrent>();
            try
            {
                string url = "http://api.openweathermap.org/data/2.5/weather?q=Seoul,kr&units=metric&APPID=14c1c90e316c1e415a9f1e0a8137eefb";
                Task<string> jsonDataTask = AsyncFetchData(url);
                string jsonData = await jsonDataTask;

                WeatherCurrent weather = JsonConvert.DeserializeObject<WeatherCurrent>(jsonData.ToString());
                lst.Add(weather);
            }
            catch(Exception ex)
            { }
            return lst;

        }
        public static async Task<List<ForecastTenDays>> FetchTenDayForecast()
        {
            List<ForecastTenDays> lst = new List<ForecastTenDays>();
            try
            {
                string url = "http://api.openweathermap.org/data/2.5/forecast/daily?q=Seoul&cnt=10&mode=json&units=metric&APPID=14c1c90e316c1e415a9f1e0a8137eefb";
                Task<string> jsonDataTask = AsyncFetchData(url);
                string jsonData = await jsonDataTask;

                WeatherTenDayForecast weather = JsonConvert.DeserializeObject<WeatherTenDayForecast>(jsonData.ToString());
                for (int i = 0; i < weather.list.Count; i++)
                {
                    var day = weather.list[i];

                    ForecastTenDays temp = new ForecastTenDays(day.weather, day.temp, day.dt, weather.city.country, weather.city.name, i);

                    if (temp.Dt.Ticks >= DateTime.Today.Ticks)
                        lst.Add(temp);
                }
            }
            catch (Exception ex)
            { }
            return lst;
        }
        public static async Task<List<ScheduledTasks>> FetchTasks()
        {
            try
            {
                ServiceClient SClient = new ServiceClient();

                List<ScheduledTasks> lst = new List<ScheduledTasks>();

                

                for (int i = 0; i < 6; i++)
                {
                    List<Tasks> temp = new List<Tasks>();
                    var tempDate = DateTime.Today.AddDays(i);
                    List<List<string>> results = await SClient.GetTasksByDayAsync(tempDate.Day,tempDate.Month,tempDate.Year);

                    for (int j = 0; j < results.Count; j++)
                    {
                        var item = results[j];
                        temp.Add(new Tasks(item, i));
                    }
                    lst.Add(new ScheduledTasks(temp));
                }
                await SClient.CloseAsync();
                return lst;
            }
            catch(Exception e)
            {
                return null;
            }

        }
     

        private static async Task<string> AsyncFetchData(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                string jsonResponse = await client.GetStringAsync(url);
                return jsonResponse;
            }
            catch (Exception)
            {

                return "";
            }

        }

        public class ScheduledTasks
        {
            public List<Tasks> Content { get; set; }
            
            public ScheduledTasks(List<Tasks> DayContent){
                this.Content = DayContent;
            }
        }


    }
}
