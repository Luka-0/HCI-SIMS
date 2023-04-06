using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Repository
{
    public class AccommodationReservationRepository : IAccommodationReservationRepository
    {
        public List<AccommodationReservation> GetAllExpiredBy(int day, int month, int year)
        {
            List<AccommodationReservation> expiredReservations = new List<AccommodationReservation>();

            using (var dbContext = new UserContext())
            {
                expiredReservations = dbContext.accommodationReservation
                                 .Where(ar => ((ar.EndingDate.Day >= (day - 5)) && (ar.EndingDate.Day <= day))
                                            && (ar.EndingDate.Month == month)
                                            && (ar.EndingDate.Year == year)
                                 )
                                 .ToList();
            }

            foreach (var reservation in expiredReservations)
            {

                reservation.Accommodation = GetAccommodation(reservation.Id);
                reservation.Guest = GetUser(reservation.Id);
            }
            return expiredReservations;
        }

        public Accommodation GetAccommodation(int reservationId)
        {

            Accommodation accommodation = new Accommodation();

            using (var dbContext = new UserContext())
            {
                accommodation = (Accommodation)dbContext.accommodationReservation
                                 .Where(a => a.Id == reservationId).Select(a => a.Accommodation).First();
            }
            return accommodation;
        }

        public User GetUser(int reservationId)
        {

            User guest = new User();

            using (var dbContext = new UserContext())
            {
                guest = (User)dbContext.accommodationReservation
                                 .Where(a => a.Id == reservationId).Select(a => a.Guest).First();
            }
            return guest;
        }

        public AccommodationReservation GetBy(int id)
        {

            AccommodationReservation reservation = new AccommodationReservation();

            using (var dbContext = new UserContext())
            {
                reservation = (AccommodationReservation)dbContext.accommodationReservation
                                 .Where(a => a.Id == id).First();
            }
            return reservation;
        }

        // Stajic
        public void Save(AccommodationReservation accommodationReservation)
        {
            using UserContext db = new();

            db.ChangeTracker.TrackGraph(accommodationReservation, node =>
            node.Entry.State = !node.Entry.IsKeySet ? EntityState.Added : EntityState.Unchanged);

            db.SaveChanges();
        }

        public List<AccommodationReservation> GetByAccommodation(int id)
        {
            List<AccommodationReservation> reservations = new();

            using (UserContext db = new())
            {
                reservations = db.accommodationReservation.Where(t => t.Accommodation.Id == id).ToList();
            }

            return reservations;
        }

        public List<AccommodationReservation> GetBy(User user)
        {
            List<AccommodationReservation> reservations = new();

            using (UserContext db = new())
            {
                reservations = db.accommodationReservation.Include(t => t.Accommodation)
                                                          .Include(t => t.Accommodation.Location)
                                                          .Where(t => t.Guest.Id == user.Id).ToList();
            }

            return reservations;

        }
    }
}
