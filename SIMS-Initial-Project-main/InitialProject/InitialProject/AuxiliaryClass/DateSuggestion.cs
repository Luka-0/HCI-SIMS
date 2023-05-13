using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace InitialProject.AuxiliaryClass
{
    public class DateSuggestion
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DateSuggestion() { 
        
        
        }

        public DateSuggestion(DateTime start, DateTime end){
            
            this.Start = start;
            this.End = end;
        }
    }
}
