//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DalCData
{
    using System;
    using System.Collections.Generic;
    
    public partial class Suggestion
    {
        public int IdSuggestion { get; set; }
        public int IdDayOfWeek { get; set; }
        public int IdWeather { get; set; }
        public string Suggestion1 { get; set; }
        public string SugestionTitle { get; set; }
        public Nullable<int> Hits { get; set; }
    
        public virtual DayOfWeek DayOfWeek { get; set; }
        public virtual Weather Weather { get; set; }
    }
}
