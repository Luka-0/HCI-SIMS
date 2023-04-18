using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class LocationRepository : ILocationRepository
    {
        public LocationRepository() { }

        public Location GetByCity(string city)
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

        public List<Location> GetByCountry(string country)
        {
            List<Location> locations = new();

            using (var db = new UserContext())
            {
                locations = db.location.Where(t => t.Country.Equals(country)).ToList();
            }

            return locations;
        }

        public Location GetBy(string country, string city)
        {
            using (var db = new UserContext())
            {

                Location location = (Location)db.location
                    .Where(l => l.Country == country && l.City == city)
                    .FirstOrDefault();

                return location;
            }
        }

        public Location GetBy(int id)
        {
            Location location = new Location();

            using (var dbContext = new UserContext())
            {
                location = (Location)dbContext.location
                                    .Where(l => l.Id == id);
            }
            return location;
        }

        public List<Location> GetAll()
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

        public List<Location> GetAllDistinctByCountry() // ne radi sa Distinct ni DistinctBy pa radim rucno
        {
            List<Location> locations = new();
            var db = new UserContext();
            bool hasFound;

            foreach(Location location in db.location)
            {
                hasFound = false;
                foreach(Location l in locations)
                {
                    if (l.Country.Equals(location.Country)) 
                    {
                        hasFound = true;
                        break;
                    }
                }

                if (!hasFound)
                {
                    locations.Add(location);
                }

            }

            return locations;
        }

        public void Save(Location location)
        {
            using var db = new UserContext();

            db.Add(location);
            db.SaveChanges();
        }
    }
}
