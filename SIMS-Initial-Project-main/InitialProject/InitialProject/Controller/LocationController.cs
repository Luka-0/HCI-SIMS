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

        public void AddNew(LocationDto location){

            LocationService.Save(new Location(location.City, location.Country));
        }


        public List<LocationDto> Load()
        {
            List<LocationDto> locations = new List<LocationDto>();
            List<Location> existingLocations = LocationService.GetAll();

            foreach (Location location in existingLocations) {

                locations.Add(new LocationDto(location.City, location.Country));
            
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

        public List<Location> GetByCountry(string country)
        {
            return LocationService.GetByCountry(country);
        }

        public List<Location> GetAllDistinctByCountry()
        {
            return LocationService.GetAllDistinctByCountry();
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
