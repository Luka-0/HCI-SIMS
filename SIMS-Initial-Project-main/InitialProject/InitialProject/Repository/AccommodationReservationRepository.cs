using InitialProject.Contexts;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Repository
{
    internal class AccommodationReservationRepository
    {
        public static List<AccommodationReservation> GetAllExpiredBy(int day, int month, int year)
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

            foreach (var reservation in expiredReservations) {

                reservation.Accommodation = GetAccommodation(reservation.Id);
                reservation.Guest = GetUser(reservation.Id);
            }
            return expiredReservations;
        }

        public static Accommodation GetAccommodation(int reservationId) {

            Accommodation accommodation = new Accommodation();

            using (var dbContext = new UserContext())
            {
                accommodation = (Accommodation)dbContext.accommodationReservation
                                 .Where(a => a.Id == reservationId).Select(a => a.Accommodation).First();
            }
            return accommodation;
        }

        public static User GetUser(int reservationId)
        {

            User guest = new User();

            using (var dbContext = new UserContext())
            {
                guest = (User)dbContext.accommodationReservation
                                 .Where(a => a.Id == reservationId).Select(a => a.Guest).First();
            }
            return guest;
        }
    }
}
