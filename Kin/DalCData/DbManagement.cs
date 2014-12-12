using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCData
{
    public class DbManagement
    {        
        public static bool AddTask(string _Title, string _Description, DateTime _DueDate, DateTime _Reminder)
        {
            try
            {
                using (var dbContext = new DalCEntities())
                {
                    dbContext.Database.Connection.Open();

                    var task = new Task
                    {
                        Title = _Title,
                        Description = _Description,
                        DueDate = _Reminder,
                        Reminder = _Reminder
                    };
                    
                    dbContext.Tasks.Add(task);
                    
                    var changesSaved = dbContext.SaveChanges();
                    return changesSaved >= 1;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public static List<List<string>> GetTasksByDay(int _Day, int _Month, int _Year) 
        {
            using (var dbContext = new DalCEntities())
            {                
                var getTasks = from t in dbContext.Tasks
                               where t.DueDate.Day == _Day &&
                               t.DueDate.Month == _Month &&
                               t.DueDate.Year == _Year
                               select t;

                List<List<string>> rtrn = new List<List<string>>();

                foreach (var t in getTasks)
                {
                    rtrn.Add(new List<string>( new string[]
                        {                                            
                            t.IdTask.ToString(),
                            t.Title,
                            t.Description,
                            t.DueDate.ToString("dddd"),
                            t.Reminder.ToString()
                        }
                    ));
                }
                
                return rtrn;
            }
        }
        public static List<List<string>> GetSuggestion(string _DayOfWeek, string _Weather)
        {
            using (var dbContext = new DalCEntities())
            {                                
                var getSuggestions = from s in dbContext.Suggestions
                                     where s.Weather.Weather1 == _Weather &&
                                     s.DayOfWeek.DayOfWeek1 == _DayOfWeek
                                     orderby s.Hits descending                                     
                                     select s;

                List<List<string>> rtrn = new List<List<string>>();

                foreach (var s in getSuggestions)
                {
                    rtrn.Add(new List<string>(new string[]
                        {  
                            s.IdSuggestion.ToString(),
                            s.SugestionTitle,
                            s.Suggestion1,
                            s.DayOfWeek.DayOfWeek1,
                            s.Weather.Weather1                           
                        }
                    ));
                }

                return rtrn;
            }
        }

        public static bool UpdateTask(int _Id, string _Title, string _Description, DateTime _DueDate)
        {
            using (var dbContext = new DalCEntities())
            {
                var findTask = from t in dbContext.Tasks
                               where t.IdTask == _Id
                               select t;

                foreach (var t in findTask)
                {
                    t.Title = _Title;
                    t.Description = _Description;
                    t.DueDate = _DueDate;                    
                }
                var changesSaved = dbContext.SaveChanges();
                return changesSaved >= 1;
            }
        }
        public static bool UpdateSuggestionHits(int _Id)
        {
            using (var dbContext = new DalCEntities())
            {
                var findSuggestion = from s in dbContext.Suggestions
                                     where s.IdSuggestion == _Id
                                     select s;

                foreach (var s in findSuggestion)
                {
                    s.Hits++;
                }
                var changesSaved = dbContext.SaveChanges();
                return changesSaved >= 1;
            }
        }

    }
}
