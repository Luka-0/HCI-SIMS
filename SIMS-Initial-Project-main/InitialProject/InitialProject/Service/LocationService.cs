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
            return LocationRepository.GetByCity(city);
        }

        public static Location GetBy(int id)
        {
            return LocationRepository.GetBy(id);
        }

        public static List<Location> GetAll()
        {
            return LocationRepository.GetAll();
        }

        public static void Save(Location location)
        {
            LocationRepository.Save(location);
        }

        public static Location GetBy(String country, String city)
        {
            return LocationRepository.GetBy(country, city);
        }

    }
}
