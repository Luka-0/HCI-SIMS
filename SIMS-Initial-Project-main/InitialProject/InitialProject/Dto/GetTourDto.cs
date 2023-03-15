using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    public class GetTourDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Location Location { get; set; }  
        public string Language { get; set; }
        public int GuestLimit { get; set; }
        public TimeOnly Duration { get; set; }
        public DateTime StartDateAndTime { get; set; }
        public List<TourKeyPoint> TourKeyPoints { get; set; }
        public List<Image> Images { get; set; }

        public GetTourDto(string name, string description, Location location, string language, int guestLimit, TimeOnly duration, DateTime startDateAndTime, List<TourKeyPoint> tourKeyPoints, List<Image> images)
        {
            Name = name;
            Description = description;
            Location = location;
            Language = language;
            GuestLimit = guestLimit;
            Duration = duration;
            StartDateAndTime = startDateAndTime;
            TourKeyPoints = tourKeyPoints;
            Images = images;
        }

        public override string ToString()
        {
            return $"{Name}\n{Description}\n{Location}\n";
        }

    }
}
