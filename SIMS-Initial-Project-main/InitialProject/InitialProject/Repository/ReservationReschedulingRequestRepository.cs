using InitialProject.Contexts;
using InitialProject.Enumeration;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Service;
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

        public void Save(ReservationReschedulingRequest reservationReschedulingRequest)
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

        public void UpdateStateBy(int id, RequestState requestState)
        {
            ReservationReschedulingRequest  reservationReschedulingRequest = new();

            var db = new UserContext();
            reservationReschedulingRequest = db.reservationReschedulingRequest.Find(id);  

            reservationReschedulingRequest.State = requestState;
            db.SaveChanges();

        }

        public void UpdateCommentBy(int id, string comment)
        {
            ReservationReschedulingRequest reservationReschedulingRequest = new();

            var db = new UserContext();
            reservationReschedulingRequest = db.reservationReschedulingRequest.Find(id);

            reservationReschedulingRequest.Comment = comment;
            db.SaveChanges();

        }

        public void UpdateWasNotifiedBy(int id, bool wasNotified)
        {
            ReservationReschedulingRequest reservationReschedulingRequest = new();

            var db = new UserContext();
            reservationReschedulingRequest = db.reservationReschedulingRequest.Find(id);

            reservationReschedulingRequest.WasNotified = wasNotified;
            db.SaveChanges();
        }

        public ReservationReschedulingRequest GetBy(int id)
        {
            ReservationReschedulingRequest reservationReschedulingRequest = new();

            using (UserContext db = new())
            {
                reservationReschedulingRequest = (ReservationReschedulingRequest)db.reservationReschedulingRequest
                                                 .Include(t => t.Reservation)
                                                 .Where(t => t.Id.Equals(id)).First();
            }
            return reservationReschedulingRequest;
        }

        public List<ReservationReschedulingRequest> GetAllByUser(int id)
        {
            List<ReservationReschedulingRequest> reservationReschedulingRequests = new();

            using (UserContext db = new())
            {
                reservationReschedulingRequests = db.reservationReschedulingRequest.
                    Include(t => t.Reservation)
                        .ThenInclude(t => t.Accommodation)
                    .Include(t => t.Reservation)
                        .ThenInclude(t => t.Guest).Where(t => t.Reservation.Guest.Id == id).ToList();
            }

            return reservationReschedulingRequests;
        }
    }
}
