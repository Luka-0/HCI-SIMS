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
    public class GuestReviewRepository : IGuestReviewRepository
    {
        public void Save(GuestReview guestReview)
        {
            using var db = new UserContext();

            db.Add(guestReview);
            db.SaveChanges();
        }


        public List<AccommodationReservation> GetGradedReservations(User owner)
        {
            List<AccommodationReservation> reservation = new List<AccommodationReservation>();

            using (var dbContext = new UserContext())
            {
                reservation = dbContext.guestReview
                    .Include(r=>r.Reservation)
                        .ThenInclude(r=>r.Accommodation).Where(t => t.Reservation.Accommodation.Owner.Equals(owner))
                    .Select(a => a.Reservation)
                    .ToList();
            }
            return reservation;
        }

        public List<GuestReview> GetAll(User user)
        {
            List<GuestReview> guestReviews = new();

            /*using var db = new UserContext();

            foreach(GuestReview g in db.guestReview)
            {
                if(g.Reservation != null && g.Reservation.Guest.Id == user.Id)
                {
                    guestReviews.Add(g);
                }
            }*/

            using(var dbContext = new UserContext())
            {
                guestReviews = dbContext.guestReview//.Where(t =>t.Reservation.Guest.Id == user.Id)
                                                    .Include(t => t.Reservation)
                                                        .ThenInclude(t => t.Accommodation)
                                                            .ThenInclude(t => t.Owner)
                                                    .ToList();
            }

            return guestReviews;
        }
    }
}
