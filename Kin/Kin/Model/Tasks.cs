using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kin.Model
{
    class Tasks
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public string Suggestion { get; set; }
        public string DayOfWeek { get; set; }
        public string Weather { get; set; }
        public int index { get; private set; }

        public Tasks(List<string> Attributes, int index)
        {
            this.Id = int.Parse(Attributes[0]);
            this.Title = Attributes[1];
            this.Suggestion = Attributes[2];
            this.DayOfWeek = Attributes[3];
            this.Weather = Attributes[4];
            this.index = index;
        }
    }
}
