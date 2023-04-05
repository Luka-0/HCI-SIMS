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
    public class TourReviewRepository:ITourReviewRepository
    {
        public List<TourReview> GetAll()
        {
            List<TourReview> reviews = new List<TourReview>();

            using (var dbContext = new UserContext())
            {
                reviews = dbContext.tourReview.Include(t => t.Reservation).ToList();
            }
            return reviews;
        }

        public TourReview GetById(int id)
        {
            TourReview review = new TourReview();
            using (var dbContext = new UserContext())
            {
                review = (TourReview)dbContext.tourReview
                                              .Include(t => t.Reservation)
                                              .Where(t => t.Id == id);      
            }
            return review;
        }

        public void Save(TourReview review)
        {
            using var db = new UserContext();
            db.Add(review);
            db.SaveChanges();
        }


    }
}
