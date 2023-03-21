using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class LocationService
    {
        public static Location getBy(string city)
        {
            return LocationRepository.getBy(city);
        }

        public static Location getBy(string city, string country)
        {
            return LocationRepository.getBy(city, country);
        }

        public static Location getBy(int id)
        {
            return LocationRepository.getBy(id);
        }

        public static List<Location> getAll()
        {
            return LocationRepository.getAll();
        }

        public static void Save(Location location)
        {
            LocationRepository.Save(location);
        }
    }
}
