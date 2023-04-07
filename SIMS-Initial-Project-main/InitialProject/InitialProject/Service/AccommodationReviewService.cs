using InitialProject.Contexts;
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
    public  class AccommodationReviewService
    {
        private readonly IAccommodationReviewRepository IAccommodationReviewRepository;
        private GuestReviewService GuestReviewService;
        private UserService UserService;

        public AccommodationReviewService(IAccommodationReviewRepository iaccommodationReviewRepository) {

            this.IAccommodationReviewRepository = iaccommodationReviewRepository;
            this.GuestReviewService = new(new GuestReviewRepository());
            this.UserService = new(new UserRepository()); 
        }

        public List<AccommodationReview> GetAllGradedBy(string ownerUsername) {

            List<AccommodationReservation> gradedReservations = new List<AccommodationReservation>();

            //finished-->ogranici na ulogovanog vlasnika
            gradedReservations = GuestReviewService.GetGradedReservations(ownerUsername);

            List< AccommodationReview > accommodationReviews = new List<AccommodationReview>();

            foreach (var review in GetAllBy(ownerUsername)) {

                foreach (var reservation in gradedReservations) {

                    if (reservation.Id == review.Reservation.Id) {

                        accommodationReviews.Add(review);
                    }
                }   
            }

            return accommodationReviews;
        }


        public List<AccommodationReview> GetAllBy(string ownerUsername)
        {
            //finished-->napravi preko servisa
            User owner = UserService.GetBy(ownerUsername);

            return this.IAccommodationReviewRepository.GetAllBy(owner);
        }
    }
}
