using InitialProject.Enumeration;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    public class AccommodationDto
    {
        public string Title;
        public int GuestLimit;
        public AccommodationType Type;
        public Location Location;

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
