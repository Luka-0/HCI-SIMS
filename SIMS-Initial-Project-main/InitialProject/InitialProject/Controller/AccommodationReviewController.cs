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
    public class AccommodationReviewController
    {
        private readonly AccommodationReviewService AccommodationReviewService = new(new AccommodationReviewRepository());
        public List<AccommodationReviewDto> GetAllGradedBy(string ownerUsername)
        {
            //recenzije koje vlasnik moze da vidi - recenzije od gostiju koje je vlasnik ocenio
            List<AccommodationReviewDto> visibleReviews = new List<AccommodationReviewDto>();

            foreach (var review in AccommodationReviewService.GetAllGradedBy(ownerUsername))
            {

                visibleReviews.Add(new AccommodationReviewDto(review));
            }

            return visibleReviews;
        }

        public List<AccommodationReviewDto> GetAllBy(string ownerUsername)
        {

            //sve recenzije napravljene od strane gostiju za vlasnika i njegove smestaje
            List<AccommodationReviewDto> reviews = new List<AccommodationReviewDto>();

            foreach (var review in AccommodationReviewService.GetAllBy(ownerUsername))
            {
                reviews.Add(new AccommodationReviewDto(review));

            }

            return reviews;
        }

        public void DeclareOwners()
        {
            this.AccommodationReviewService.DeclareOwners();
        }

        public void DeclareOwner(string ownerUsername) {

            List<AccommodationReview> ownerReviews = new List<AccommodationReview>();
            ownerReviews = AccommodationReviewService.GetAllBy(ownerUsername);

            this.AccommodationReviewService.DeclareOwner(ownerReviews, ownerUsername);
        }

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
