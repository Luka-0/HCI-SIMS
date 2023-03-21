using InitialProject.Contexts;
using InitialProject.Dto;
using InitialProject.Model;
﻿using InitialProject.Model;
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

        public static Location GetBy(string city)
        {
            return LocationRepository.GetBy(city);
        }

        public static Location getBy(int id)
        {
            return LocationRepository.getBy(id);
        }

        public static List<Location> GetAll()
        {
            return LocationRepository.GetAll();
        }

        public static void Save(Location location)
        {
            LocationRepository.Save(location);
        }

        public static Location getBy(String country, String city)
        {
            return LocationRepository.getBy(country, city);
        }

    }
}
