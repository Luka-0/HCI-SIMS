using InitialProject.Interface;
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
        private readonly ITourReviewRepository _tourReviewRepository;

        public TourReviewService(ITourReviewRepository repository)
        {
            _tourReviewRepository = repository;
        }

        public List<TourReview> GetAll()
        {
            return _tourReviewRepository.GetAll();
        }

        public TourReview GetById(int id)
        {
            return _tourReviewRepository.GetById(id);  
        }

        public void Save(TourReview tourReview)
        {
            _tourReviewRepository.Save(tourReview);
        }

        public List<TourReview> GetManyByTour(int id)
        {
           return _tourReviewRepository.GetManyByTour(id);
        }


        public void Invalidate(int id)
        {
            _tourReviewRepository.Invalidate(id);
        }

        
    }
}
