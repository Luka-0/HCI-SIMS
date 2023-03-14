using InitialProject.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    public class NewAccommodationDto
    {
        public string Title { get; set; }
        public int GuestLimit { get; set; }

        public AccommodationType Type { get; set;  }

        public int MinimumReservationDays { get; set; }

        public int CancellationDeadline { get; set; }



    }
}
