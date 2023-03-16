using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class TourReservationService
    {
        public TourReservationService() { }

        public static List<TourReservation> GetAll()
        {
            return TourReservationRepository.GetAll();
        }

        public static TourReservation GetById(int id)
        {
            return TourReservationRepository.GetById(id);
        }

        public static int CountBy(Tour tour)
        {
            return TourReservationRepository.CountBy(tour);
        }

        public static bool IsFull(Tour tour)
        {
            if(tour.GuestLimit == CountBy(tour))
                return true;
            else 
                return false;
        }

        public static int GetAvaliableSpace(Tour tour)
        {
            return tour.GuestLimit - CountBy(tour);
        }
    }
}
