using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    public interface ITourReviewRepository
    {
        public List<TourReview> GetAll();
        public TourReview GetById(int id);
        public void Save(TourReview review);
        public List<TourReview> GetManyByTour(int id);
        public void Invalidate(int id);
     
    }
}
