using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class TourReviewService
    {
        TourReviewRepository tourReviewRepository = new TourReviewRepository();

        public List<TourReview> GetAll()
        {
            return tourReviewRepository.GetAll();
        }

        public TourReview GetById(int id)
        {
            return tourReviewRepository.GetById(id); 
        }




    }
}
