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
        private TourReservationRepository repository = new TourReservationRepository();

        public List<TourReservation> GetAll()
        {
            return repository.GetAll();
        }

        public List<TourReservation> GetBy(Tour tour)
        {
            return repository.GetBy(tour);
        }

        public bool IsReserved(Tour tour)
        {
            return repository.IsReserved(tour);
        }

        public TourReservation GetById(int id)
        {
            return repository.GetById(id);
        }

        public int CountGuestsBy(Tour tour)
        {
            return repository.CountGuestsBy(tour);
        }

        public bool IsFull(Tour tour)
        {
            if(tour.GuestLimit == CountGuestsBy(tour))
                return true;
            else 
                return false;
        }

        public int GetAvailableSpace(Tour tour)
        {
            return tour.GuestLimit - CountGuestsBy(tour);
        }
    }
}
