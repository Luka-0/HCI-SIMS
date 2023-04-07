using InitialProject.Enumeration;
using InitialProject.View;
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

        public string CityName { get; set; }

        public List<String> Images { get; set; }

        public string OwnerUsername { get; set; }

        public NewAccommodationDto(string title, int guestNumber, AccommodationType type, int minReservationDays, int cancellationDeadline, string city, List<String> urls, string username) { 
           
            Title = title;
            GuestLimit = guestNumber;
            Type = type;
            MinimumReservationDays = minReservationDays;
            CancellationDeadline = cancellationDeadline;
            CityName = city;
            Images = urls;
            OwnerUsername = username;
        }

    }
}
