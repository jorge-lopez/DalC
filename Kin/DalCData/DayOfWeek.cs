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
    
    public partial class DayOfWeek
    {
        public DayOfWeek()
        {
            this.Suggestions = new HashSet<Suggestion>();
        }
    
        public int Id { get; set; }
        public string DayOfWeek1 { get; set; }
    
        public virtual ICollection<Suggestion> Suggestions { get; set; }
    }
}
