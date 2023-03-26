using InitialProject.Contexts;
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
        private TourReservationRepository tourReservationRepository = new TourReservationRepository();
        private TourRepository tourRepository = new TourRepository();
        public List<TourReservation> GetAll()
        {
            return tourReservationRepository.GetAll();
        }

        public List<TourReservation> GetByTour(Tour tour)
        {
            return tourReservationRepository.GetByTour(tour);
        }

        public TourReservation GetById(int id)
        {
            return tourReservationRepository.GetById(id);
        }

        public void Save(TourReservation reservation, Tour tour, User guest, int guestNumber)
        {
            tourReservationRepository.Save(reservation, tour, guest, guestNumber);
        }

        //TODO ispravi
        public bool IsReserved(Tour tour)
        {
            List<TourReservation> reservations = tourReservationRepository.GetByTour(tour);
            if(reservations.Find(r => r.Tour.Id == tour.Id) != null)
            {
                return true;
            }
            return false;
        }

        public int CountGuestsOnTour(Tour tour)
        {
            List<TourReservation> reservations = tourReservationRepository.GetByTour(tour);
            return reservations.Where(r => r.Tour.Id == tour.Id).Sum(r => r.GuestNumber);
        }

        public int CountTourReservations(Tour tour)
        {
            return tourReservationRepository.GetByTour(tour).Count();
        }

        public bool IsFull(Tour tour)
        {
            if(tour.GuestLimit == CountGuestsOnTour(tour))
                return true;
            else 
                return false;
        }

        public int GetAvailableSpace(Tour tour)
        {
            return tour.GuestLimit - CountGuestsOnTour(tour);
        }

        
    }
}
