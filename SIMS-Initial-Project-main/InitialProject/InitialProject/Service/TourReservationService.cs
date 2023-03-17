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
        private static TourReservationRepository repository;
        public TourReservationService() { }

        public List<TourReservation> GetAll()
        {
            return repository.GetAll();
        }

        public TourReservation GetById(int id)
        {
            return repository.GetById(id);
        }

        public int CountBy(Tour tour)
        {
            return repository.CountBy(tour);
        }

        public bool IsFull(Tour tour)
        {
            if(tour.GuestLimit == CountBy(tour))
                return true;
            else 
                return false;
        }

        public int GetAvaliableSpace(Tour tour)
        {
            return tour.GuestLimit - CountBy(tour);
        }
    }
}
