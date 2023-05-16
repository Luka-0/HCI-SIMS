using InitialProject.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    public class Statistics
    {
       
        public string TimePeriod { get; set; }
        public int ReservationCount { get; set; }
        public int CancellationCount { get; set; }
        public int ReschedulingCount { get; set; }
        public int RenovationSuggestionCount { get; set; }
        public double Occupancy { get; set; }

        public Statistics(int reservationCount, string timePeriod,
                          int cancellationCount, int reschedulingCount,
                          int renovationSuggestionCount, double occupancy)
        {

            this.RenovationSuggestionCount = renovationSuggestionCount;
            this.CancellationCount = cancellationCount;
            this.ReschedulingCount = reschedulingCount;
            this.ReservationCount = reservationCount;
            this.Occupancy = occupancy;
            this.TimePeriod = timePeriod;
        }
    }
}
