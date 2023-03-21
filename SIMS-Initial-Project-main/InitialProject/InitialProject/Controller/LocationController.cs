using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    internal class LocationController
    {
        public static void AddNew(LocationDto record)
        {
            Location location  = new Location(record.City, record.Country);

            LocationService.Save(location);
        }


        public static List<LocationDto> Load()
        {
            List<LocationDto> locations = new List<LocationDto>();
            List<Location> records = LocationService.GetAll();

            foreach (Location record in records) {

                locations.Add(new LocationDto(record.City, record.Country));
            
            }
            return locations;
        }
    }
}
