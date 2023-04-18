using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    interface ILocationRepository
    {
        public Location GetByCity(string city);
        public List<Location> GetByCountry(string country);
        public Location GetBy(string country, string city);
        public Location GetBy(int id);
        public List<Location> GetAll();
        public List<Location> GetAllDistinctByCountry();
        public void Save(Location location);

    }
}
