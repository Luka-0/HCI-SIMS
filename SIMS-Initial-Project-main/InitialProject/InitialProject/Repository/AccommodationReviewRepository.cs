using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    internal class AccommodationReviewRepository : IAccommodationReviewRepository
    {
        public List<AccommodationReview> GetAllBy(User owner)
        {
            List<AccommodationReview> accommodationReviews = new List<AccommodationReview> ();

            using (UserContext db = new())
            {
                accommodationReviews = db.accommodationReview.
                    Include(t => t.Reservation)
                        .ThenInclude(t=> t.Accommodation)
                            .ThenInclude(t=>t.Owner).Where(t => t.Reservation.Accommodation.Owner.Equals(owner))
                    .Include(t=>t.Reservation)
                        .ThenInclude(t=>t.Guest).ToList();
            }

            return accommodationReviews;
        }
    }
}
