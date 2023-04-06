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
    public class AccommodationReviewRepository : IAccommodationReviewRepository
    {
        public AccommodationReviewRepository() { }

        public void Save(AccommodationReview accommodationReview)
        {
            using var db = new UserContext();

            db.Add(accommodationReview);
            db.SaveChanges();
        }

        public List<AccommodationReview> GetBy(User user)
        {
            List<AccommodationReview> accommodationReviews = new();

            using(var db = new UserContext())
            {
                accommodationReviews = db.accommodationReview.Include(t => t.Reservation)
                                                             .Where(t => t.Reservation.Guest.Id == user.Id).ToList();
            }

            return accommodationReviews;
        }
    }
}
