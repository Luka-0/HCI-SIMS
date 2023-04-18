using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class TourReservationRepository:ITourReservationRepository
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

      /*  public TourReservation GetById(int id)
        {
            TourReservation reservation = new TourReservation();

            using (var dbContext = new UserContext())
            {
                reservation = (TourReservation)dbContext.tourReservation
                    .Where(t => t.Id == id);
                //.SingleOrDefault(); Ja(Pavle) mislim da sad dodao ovo na gresku, ali nisam sig proveri to
            }
            return reservation;
        }
        */
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

        public List<TourReservation> GetByGuest(User user)
        {
            List<TourReservation> reservations = new List<TourReservation>();

            using (var dbContext = new UserContext())
            {
                reservations = dbContext.tourReservation
                                 .Include(t => t.Tour)
                                 .Where(t => t.BookingGuest.Id == user.Id)
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

        public List<TourReservation> GetByTour(int id)
        {
            using (var db = new UserContext())
            {
                List<TourReservation> reservations = new List<TourReservation>();
                reservations = db.tourReservation.Where(r => r.Tour.Id == id)
                        .Include(r => r.Tour)
                        .Include(r => r.ArrivalPoint)
                        .Include(r => r.BookingGuest)
                        .Include(r => r.Voucher)
                        .ToList();

                return reservations;
            }
        }

        public void SetArrivalKeyPoint(TourKeyPoint keyPoint, int id)
        {
            using (var db = new UserContext())
            {
                var tempRecord = db.tourReservation.Find(id);
                tempRecord.ArrivalPoint = keyPoint;
                db.SaveChanges();
            }
        }

        public TourReservation GetById(int id)
        {
            using (var db = new UserContext())
            {
                
                TourReservation reservation =  db.tourReservation
                    .Where(t => t.Id == id)
                    .Include(t => t.BookingGuest)
                    .Include(t  => t.ArrivalPoint)
                    .FirstOrDefault();

                return reservation;
            }
        }

        public void Delete(List<TourReservation> reservations)
        {
            using (var db = new UserContext())
            {
                foreach (TourReservation reservation in reservations)
                {
                    db.tourReservation.Remove(reservation);
                }
                db.SaveChangesAsync();
            }
            
        }


    }
}
