using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class TourReviewController
    {
        private TourReviewService tourReviewService = new TourReviewService(new TourReviewRepository());

        public List<TourReview> GetAll()
        {
            return tourReviewService.GetAll();
        }

        public TourReview GetById(int id)
        {
            return tourReviewService.GetById(id);
        }

        public void Save(TourReview review)
        {
            tourReviewService.Save(review);
        }

        public List<TourReview> GetManyByTour(int id)
        {
            return tourReviewService.GetManyByTour(id);
        }


        public void Invalidate(int id)
        {
            tourReviewService.Invalidate(id);
        }
    }
}
