using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class TourService
    {
        TourRepository TourRepository = new TourRepository();
        public TourService() { }

        public List<Tour> GetAll()
        {
            return TourRepository.GetAll();
        }

        public List<Tour> GetBy(Location location)
        {
            return TourRepository.GetBy(location);
        }
    }
}
