using InitialProject.Dto;
using InitialProject.Interface;
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
    public class LocationController
    {
        private readonly LocationService LocationService = new (new LocationRepository());

        public void AddNew(LocationDto record)
        {
            Location location  = new Location(record.City, record.Country);

            LocationService.Save(location);
        }


        public List<LocationDto> Load()
        {
            List<LocationDto> locations = new List<LocationDto>();
            List<Location> records = LocationService.GetAll();

            foreach (Location record in records) {

                locations.Add(new LocationDto(record.City, record.Country));
            
            }
            return locations;
        }

        public List<Location> GetAll()
        {
            return LocationService.GetAll();
        }

        public Location GetByCity(string city)
        {
            return LocationService.GetByCity(city);
        }

        public List<Location> getByCountry(string country)
        {
            return LocationService.GetByCountry(country);
        }

        public Location GetBy(int id)
        {
            return LocationService.GetBy(id);
        }

        public Location GetBy(String country, String city)
        {
            return LocationService.GetBy(country, city);
        }
    }
}
