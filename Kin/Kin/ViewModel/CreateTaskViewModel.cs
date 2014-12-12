using Kin.Model;
using Kin.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kin.ViewModel
{
    class CreateTaskViewModel
    {          
        public List<ForecastTenDays> Forecast { get; set; }
        public List<Suggestion> SuggestionsList { get; set; }

        public CreateTaskViewModel()
        {            
            Forecast = new List<ForecastTenDays>();
            SuggestionsList = new List<Suggestion>();
        }

        public static async Task<List<Suggestion>> FetchSuggestion(string Day, string Weather)
        {
            try
            {
                ServiceClient SClient = new ServiceClient();

                List<Suggestion> lst = new List<Suggestion>();
                List<List<string>> temp = new List<List<string>>();

                temp = await SClient.GetSuggestionAsync(Day, Weather);

                foreach (List<string> item in temp)
                {
                    lst.Add(new Suggestion(item));  
                }

                await SClient.CloseAsync();
                return lst;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static async Task<bool> AddNewTask(string Title, string Description, DateTime DueDate, DateTime Reminder)
        {
            ServiceClient SClient = new ServiceClient();
            bool b = await SClient.AddTaskAsync(Title, Description, DueDate, Reminder);
            await SClient.CloseAsync();
            return b;

        }
    }
}
