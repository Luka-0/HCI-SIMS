using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class AccommodationReviewService
    {
        private readonly IAccommodationReviewRepository IAccommodationReviewRepository;
        private GuestReviewService GuestReviewService;
        private UserService UserService;
        private AccommodationService AccommodationService;

        public AccommodationReviewService(IAccommodationReviewRepository iaccommodationReviewRepository)
        {

            this.IAccommodationReviewRepository = iaccommodationReviewRepository;
            this.GuestReviewService = new(new GuestReviewRepository());
            this.UserService = new(new UserRepository());
            this.AccommodationService = new(new AccommodationRepository());
        }

        public List<AccommodationReview> GetAllGradedBy(string ownerUsername)
        {

            List<AccommodationReservation> gradedReservations = new List<AccommodationReservation>();

            //finished-->ogranici na ulogovanog vlasnika
            gradedReservations = GuestReviewService.GetGradedReservations(ownerUsername);

            List<AccommodationReview> accommodationReviews = new List<AccommodationReview>();

            foreach (var review in GetAllBy(ownerUsername))
            {

                foreach (var reservation in gradedReservations)
                {

                    if (reservation.Id == review.Reservation.Id)
                    {

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

        public void DeclareOwners()
        {

            foreach (var owner in this.UserService.GetOwners())
            {

                DeclareOwner(this.GetAllBy(owner.Username), owner.Username);
            }
        }
        public void DeclareOwner(List<AccommodationReview> accommodationReviews, string ownerUsername)
        {
            this.UserService.UpdateStatusBy(ownerUsername, IsSuperOwner(accommodationReviews));
            this.AccommodationService.UpdateClassBy(ownerUsername, IsSuperOwner(accommodationReviews));
        }

        private bool IsSuperOwner(List<AccommodationReview> accommodationReviews)
        {

            double gradeSum = 0;
            double gradeCount = 0;
            double average = 0;

            foreach (var review in accommodationReviews)
            {
                gradeSum += (review.Correctness + review.Tidiness);
                gradeCount++;
            }

            average = gradeSum / gradeCount;
            //real limit by specification is 50, using 4 for running tests
            if (average >= 9.5 && gradeCount >= 50)
            {
                return true;
            }

            return false;
        }


        public void Save(AccommodationReview accommodationReview)
        {
            IAccommodationReviewRepository.Save(accommodationReview);
        }
        public List<AccommodationReview> GetBy(User user)
        {
            return IAccommodationReviewRepository.GetBy(user);
        }


    }
}


