using InitialProject.Contexts;
using InitialProject.Dto;
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

        public static List<Location> getAll()
        {
            return LocationRepository.getAll();
        }

        public static void Save(Location location)
        {
            LocationRepository.Save(location);
        }

        public  Location getBy(String country, String city)
        {
            return LocationRepository.getBy(country, city);
        }

    }
}
