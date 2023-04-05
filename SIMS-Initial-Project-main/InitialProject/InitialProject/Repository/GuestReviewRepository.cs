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
    public class GuestReviewRepository : IGuestReviewRepository
    {
        public void Save(GuestReview guestReview)
        {
            using var db = new UserContext();

            db.Add(guestReview);
            db.SaveChanges();
        }


        public List<AccommodationReservation> GetGradedReservations()
        {
            List<AccommodationReservation> reservation = new List<AccommodationReservation>();

            using (var dbContext = new UserContext())
            {
                reservation = (List<AccommodationReservation>)dbContext.guestReview.Select(a => a.Reservation).ToList();
            }
            return reservation;
        }
    }
}
