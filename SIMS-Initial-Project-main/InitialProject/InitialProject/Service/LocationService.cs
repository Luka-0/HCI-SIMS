using InitialProject.Contexts;
using InitialProject.Dto;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    class LocationService
    {
        private readonly ILocationRepository ILocationRepository;

        public LocationService(ILocationRepository iLocationRepository)
        {
            ILocationRepository = iLocationRepository;
        }

        public Location GetByCity(string city)
        {
            return ILocationRepository.GetByCity(city);
        }

        public List<Location> GetByCountry(string country)
        {
            return ILocationRepository.GetByCountry(country);
        }

        public Location GetBy(int id)
        {
            return ILocationRepository.GetBy(id);
        }

        public List<Location> GetAll()
        {
            return ILocationRepository.GetAll();
        }

        public List<Location> GetAllDistinctByCountry()
        {
            return ILocationRepository.GetAllDistinctByCountry();
        }


        public void Save(Location location)
        {
            ILocationRepository.Save(location);
        }

        public Location GetBy(String country, String city)
        {
            return ILocationRepository.GetBy(country, city);
        }

    }
}
