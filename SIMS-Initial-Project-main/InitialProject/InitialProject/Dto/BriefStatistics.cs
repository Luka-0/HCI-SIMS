using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    public class BriefStatistics
    {
        public Accommodation Accommodation { get; set; }
        public double AverageReservationCount { get; set; }
        public double AverageOccupancy { get; set; }

        public BriefStatistics()
        {

        }

        public BriefStatistics(Accommodation acc, double avgCnt, double avgOccupancy)
        {
            this.Accommodation = acc;
            this.AverageReservationCount = avgCnt;
            this.AverageOccupancy = avgOccupancy;
        }
    }
}
