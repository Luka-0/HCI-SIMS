using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class ReservationReschedulingRequestRepository : IReservationReschedulingRequestRepository
    {
        public ReservationReschedulingRequestRepository() { }

        public void Save (ReservationReschedulingRequest reservationReschedulingRequest)
        {
            using var db = new UserContext();

            var existingLocation = db.location.Find(reservationReschedulingRequest.Reservation.Accommodation.Location.Id);
            var existingAccommodation = db.accommodation.Find(reservationReschedulingRequest.Reservation.Accommodation.Id);
            var existingAccommodationReservation = db.accommodationReservation.Find(reservationReschedulingRequest.Reservation.Id);

            reservationReschedulingRequest.Reservation.Accommodation.Location = existingLocation;
            reservationReschedulingRequest.Reservation.Accommodation = existingAccommodation;
            reservationReschedulingRequest.Reservation = existingAccommodationReservation;

            db.location.Attach(existingLocation);
            db.accommodation.Attach(existingAccommodation);
            db.accommodationReservation.Attach(existingAccommodationReservation);


            db.Add(reservationReschedulingRequest);
            db.SaveChanges();
        }

        public List<ReservationReschedulingRequest> GetAllBy(User owner) {

            List<ReservationReschedulingRequest> reservationReschedulingRequests = new();

            using (UserContext db = new())
            {
                reservationReschedulingRequests = db.reservationReschedulingRequest.
                    Include(t => t.Reservation)
                        .ThenInclude(t => t.Accommodation)
                            .ThenInclude(t => t.Owner).Where(t => t.Reservation.Accommodation.Owner.Equals(owner))
                    .Include(t => t.Reservation)
                        .ThenInclude(t => t.Guest).ToList();
            }

            return reservationReschedulingRequests;

        }
    }
}
