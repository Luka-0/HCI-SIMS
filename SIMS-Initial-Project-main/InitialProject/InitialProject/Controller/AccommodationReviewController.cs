using InitialProject.Dto;
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
    internal class AccommodationReviewController
    {
        private readonly AccommodationReviewService AccommodationReviewService = new(new AccommodationReviewRepository());
        public List<AccommodationReviewDto> GetAllGradedBy(string ownerUsername)
        {
            List<AccommodationReview> reviews = new List<AccommodationReview>();
            List<AccommodationReviewDto> processedReviews = new List<AccommodationReviewDto>();

            reviews = this.AccommodationReviewService.GetAllGradedBy(ownerUsername);

            foreach (var review in reviews) {

                processedReviews.Add(new AccommodationReviewDto(review));
            }

            return processedReviews;
        }
        private readonly AccommodationReviewService AccommodationReviewService = new (new AccommodationReviewRepository());

        public void Save(AccommodationReview accommodationReview)
        {
            AccommodationReviewService.Save(accommodationReview);
        }

        public List<AccommodationReview> GetBy(User user)
        {
            return AccommodationReviewService.GetBy(user);
        }
    }
}
