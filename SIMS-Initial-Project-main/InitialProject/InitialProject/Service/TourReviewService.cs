using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Update;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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


        public bool CheckIfEligible(List<TourReservation> reservations)
        {
            List<TourReview> reviews = new List<TourReview>();
            foreach (TourReservation reservation in reservations)
            {
                reviews.AddRange(_tourReviewRepository.GetManyByTour(reservation.Tour.Id));

            }
            if (CheckAverage(reviews))
            {
                return true;
            }
            return false;
        }

        private bool CheckAverage(List<TourReview> reviews)
        {

            double totalScore= 0;
            int count = 0;
            if (reviews.Count == 0)
            {
                return false ;
            }
            foreach (TourReview review in reviews)
            {
                if (review.Valid == true)
                {
                    double average = (review.AmusementScore + review.GuideKnowledge + review.GuideLanguage) / 3.0;
                     totalScore += average;
                     count++;
                }
            }

            if (totalScore / count > 4.0)
            {
                return true;
            }
            return false;
        }
    }
}
