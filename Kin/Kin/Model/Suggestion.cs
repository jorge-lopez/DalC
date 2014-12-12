using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kin.Model
{
    class Suggestion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desription { get; set; }
        public string DayOfWeek { get; set; }
        public string Weather { get; set; }

        public Suggestion(List<string> Attributes)
        {
            this.Id = int.Parse(Attributes[0]);
            this.Title = Attributes[1];
            this.Desription = Attributes[2];
            this.DayOfWeek = Attributes[3];
            this.Weather = Attributes[4];

        }

    }
}
