using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DalCService 
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        bool AddTask(string Title, string Description, DateTime DueDate, DateTime Reminder);

        [OperationContract]
        List<List<string>> GetTasksByDay(int Day, int Month, int Year);

        [OperationContract]
        List<List<string>> GetSuggestion(string DayOfWeek, string Weather);

        [OperationContract]
        bool updateTask(int Id, string Title, string Description, DateTime DueDate);

        [OperationContract]
        bool updateSuggestionHits(int Id);
    }
}
