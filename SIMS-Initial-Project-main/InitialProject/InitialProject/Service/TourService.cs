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
        public TourRepository repository = new TourRepository();
       

        public List<Tour> GetAll()
        {
            return repository.GetAll();
        }

        public Tour GetById(int id)
        {
            return repository.GetById(id);
        }
        public List<Tour> GetBy(Location location)
        {
            return repository.GetBy(location);
        }

        public List<Tour> GetBy(TimeOnly duration)
        {
            return repository.GetBy(duration);
        }

        public List<Tour> GetBy(string language)
        {
            return repository.GetBy(language);
        }

        public List<Tour> GetBy(int guestNumber)
        {
            return repository.GetBy(guestNumber);
        }

        

    }
}
