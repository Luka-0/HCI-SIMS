﻿using InitialProject.Contexts;
using InitialProject.Model;
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
