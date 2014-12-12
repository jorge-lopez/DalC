using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kin.Model
{
    class ForecastTenDays
    {
        public List<WeatherTenDayForecast.Weather> Description { get; set; }
        public WeatherTenDayForecast.Temp Temperature { get; set; }
        public DateTime Dt { get; set; }
        public string DayOfTheWeek { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Index { get; set; }

        public ForecastTenDays(List<WeatherTenDayForecast.Weather> Description, WeatherTenDayForecast.Temp Temperature,
            int UnixTimeStamp, string Country, string City, int Index)
        {
            this.Description = Description;
            this.Temperature = RoundTemperature(Temperature);
            this.Dt = UnixTimeStampToDateTime(UnixTimeStamp);
            this.DayOfTheWeek = Dt.ToString("dddd");
            this.Country = Country;
            this.City = City;
            this.Index = Index;
        }
        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public WeatherTenDayForecast.Temp RoundTemperature(WeatherTenDayForecast.Temp Temperature)
        {
            Temperature.max = Math.Round(Temperature.max);
            Temperature.min = Math.Round(Temperature.min);
            Temperature.day = Math.Round(Temperature.day);

            return Temperature;
        }
    }
}
