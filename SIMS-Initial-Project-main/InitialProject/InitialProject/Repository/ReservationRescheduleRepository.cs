using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class ReservationRescheduleRepository : IReservationRescheduleRepository
    {
        public ReservationRescheduleRepository() { }

        public void Save (ReservationReschedule reservationReschedule)
        {
            using var db = new UserContext();

            var existingLocation = db.location.Find(reservationReschedule.Reservation.Accommodation.Location.Id);
            var existingAccommodation = db.accommodation.Find(reservationReschedule.Reservation.Accommodation.Id);
            var existingAccommodationReservation = db.accommodationReservation.Find(reservationReschedule.Reservation.Id);

            reservationReschedule.Reservation.Accommodation.Location = existingLocation;
            reservationReschedule.Reservation.Accommodation = existingAccommodation;
            reservationReschedule.Reservation = existingAccommodationReservation;

            db.location.Attach(existingLocation);
            db.accommodation.Attach(existingAccommodation);
            db.accommodationReservation.Attach(existingAccommodationReservation);


            db.Add(reservationReschedule);
            db.SaveChanges();
        }
    }
}
