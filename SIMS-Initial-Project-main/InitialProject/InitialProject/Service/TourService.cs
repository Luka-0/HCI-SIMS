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

        public TourService() { }

        public static List<Tour> GetAll()
        {
            return TourRepository.GetAll();
        }

        public static Tour GetById(int id)
        {
            return TourRepository.GetById(id);
        }
        public static List<Tour> GetBy(Location location)
        {
            return TourRepository.GetBy(location);
        }

        public static List<Tour> GetBy(TimeOnly duration)
        {
            return TourRepository.GetBy(duration);
        }

        public static List<Tour> GetBy(string language)
        {
            return TourRepository.GetBy(language);
        }

        public static List<Tour> GetBy(int guestNumber)
        {
            return TourRepository.GetBy(guestNumber);
        }

        

    }
}
