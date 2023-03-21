using InitialProject.Contexts;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class TourReservationRepository
    {
       // public TourReservationRepository() { }

        public List<TourReservation> GetAll()
        {
            List<TourReservation> reservations = new List<TourReservation>();

            using (var dbContext = new UserContext())
            {
                reservations = dbContext.tourReservations
                                        .Include(t => t.Tour)
                                        .ToList();
            }
            return reservations;
        }

        public TourReservation GetById(int id)
        {
            TourReservation reservation = new TourReservation();

            using (var dbContext = new UserContext())
            {
                reservation = (TourReservation)dbContext.tourReservations
                                 .Where(t => t.Id == id);
            }
            return reservation;
        }

        //CountBy given tour/tourId
        public int CountReservationsBy(Tour tour)
        {
            int count = 0;

            using (var dbContext = new UserContext())
            {
                count = dbContext.tourReservations
                                 .Where(t => t.Tour.Id == tour.Id)
                                 .Count();
            }
            return count;
        }

        public int CountGuestsBy(Tour tour)
        {
            int count = 0;

            using (var dbContext = new UserContext())
            {
                count = dbContext.tourReservations
                                 .Where(t => t.Tour.Id == tour.Id)
                                 .Sum(t => t.GuestNumber);
            }
            return count;
        }

        public List<TourReservation> GetBy(Tour tour)
        {
            List<TourReservation> reservations = new List<TourReservation>();

            using (var dbContext = new UserContext())
            {
                reservations = dbContext.tourReservations
                                 .Where(t => t.Tour.Id == tour.Id)
                                 .ToList();
            }
            return reservations;
        }

        public bool IsReserved(Tour tour) 
        {
            List<TourReservation> reservations = GetAll();

            foreach(var reservation in reservations)
            {
                if(reservation.Tour.Id == tour.Id) 
                    return true;
            }
            return false;
        }









    }
}
