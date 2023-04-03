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
        public List<TourReservation> GetAll()
        {
            List<TourReservation> reservations = new List<TourReservation>();

            using (var dbContext = new UserContext())
            {
                reservations = dbContext.tourReservation
                                        .Include(t => t.Tour)
                                        .ThenInclude(l => l.Location)
                                        .ToList();
            }
            return reservations;
        }

        public TourReservation GetById(int id)
        {
            TourReservation reservation = new TourReservation();

            using (var dbContext = new UserContext())
            {
                reservation = (TourReservation)dbContext.tourReservation
                                 .Where(t => t.Id == id);
            }
            return reservation;
        }

        public List<TourReservation> GetByTour(Tour tour)
        {
            List<TourReservation> reservations = new List<TourReservation>();

            using (var dbContext = new UserContext())
            {
                reservations = dbContext.tourReservation
                                 .Include(t => t.Tour)
                                 .Where(t => t.Tour.Id == tour.Id)
                                 .ToList();
            }
            return reservations;
        }

        public void Save(TourReservation reservation, Tour tour, User guest, int guestNumber)
        {
            var db = new UserContext();

            var existingTour = db.tour.Find(tour.Id);
            var existingLocation = db.location.Find(tour.Location.Id);
            var existingGuest = db.users.Find(guest.Id);

            reservation.Tour = existingTour;
            reservation.Tour.Location = existingLocation;
            reservation.BookingGuest = existingGuest;
            reservation.GuestNumber = guestNumber;

            db.tour.Attach(existingTour);
            db.location.Attach(existingLocation);
            db.users.Attach(existingGuest);

            db.Add(reservation);

            db.SaveChanges();
        }




    }
}
