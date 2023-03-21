using InitialProject.Enumeration;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    class AccommodationDto
    {
        private string Title;
        private int GuestLimit;
        private AccommodationType Type;
        private Location Location;

        public AccommodationDto() { }

        public AccommodationDto(string title, int guestLimit, AccommodationType type, Location location)
        {
            Title = title;
            GuestLimit = guestLimit;
            Type = type;
            Location = location;
        }
    }
}
