using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DalCData;
using System.Threading.Tasks;

namespace DalCService
{
    public class Service1 : IService
    {
        public bool AddTask(string Title, string Description, DateTime DueDate, DateTime Reminder)
        {
            try
            {
                return DbManagement.AddTask(Title, Description, DueDate, Reminder);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public List<List<string>> GetTasksByDay(int Day, int Month, int Year)
        {
            try
            {
                return DbManagement.GetTasksByDay(Day, Month, Year);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
    

        public List<List<string>> GetSuggestion(string DayOfWeek, string Weather)
        {
            try
            {
                return DbManagement.GetSuggestion(DayOfWeek, Weather);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public bool updateTask(int Id, string Title, string Description, DateTime DueDate)
        {
            try
            {
                return DbManagement.UpdateTask(Id, Title, Description, DueDate);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
        public bool updateSuggestionHits(int Id)
        {
            try
            {
                return DbManagement.UpdateSuggestionHits(Id);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
    }
}
