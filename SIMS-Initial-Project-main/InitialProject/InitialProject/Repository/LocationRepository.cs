using InitialProject.Contexts;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class LocationRepository
    {
        public static Location getBy(string city)
        {
            using (var db = new UserContext())
            {
                foreach (Location location in db.location)
                {
                    if (location.City.Equals(city))
                    {
                        return location;
                    }
                }
            }
            return null;
        }

        public static Location getBy(string country, string city)
        {
            using (var db = new UserContext())
            {
                foreach (Location location in db.location)
                {
                    if (location.Country.Equals(country) && location.City.Equals(city))
                    {
                        return location;
                    }
                    
                }
            }

            return null;

        public static Location getBy(int id)
        {
            Location location = new Location();

            using (var dbContext = new UserContext())
            {
                location = (Location)dbContext.location
                                 .Where(l => l.Id == id);
            }
            return location;
        }

        public static Location getBy(string city, string country)
        {
            Location location = new Location();

            using (var dbContext = new UserContext())
            {
                location = dbContext.location
                                 .Where(l => l.Country == country && l.City == city).First();
            }
            return location;
        }

        public static List<Location> getAll()
        {
            List<Location> locations = new List<Location>();

            using (var db = new UserContext())
            {
                foreach (Location location in db.location)
                {
                    locations.Add(location);
                }
            }
            return locations;
        }

        public static void Save(Location location)
        {
            using var db = new UserContext();

            db.Add(location);
            db.SaveChanges();
        }
    }
}
