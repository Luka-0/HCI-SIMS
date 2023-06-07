using InitialProject.Dto;
using InitialProject.Enumeration;
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
    public class GuestReviewController
    {
        private GuestReviewService guestReviewService = new(new GuestReviewRepository());

        public void Save(NewGuestReviewDto guestReviewDto) {

            GuestReview review = new GuestReview(guestReviewDto.Tidiness, guestReviewDto.Obedience, guestReviewDto.Comment);

            guestReviewService.Save(review, guestReviewDto.ReservationId);
        
        }

        public List<ExpiredReservationDto> LoadExpiredReservations(string ownerUsername)
        {

            List<AccommodationReservation> reservations = guestReviewService.GetNotGradedExpiredReservations(ownerUsername);

            List<ExpiredReservationDto> expiredReservations = new List<ExpiredReservationDto>();

            foreach (AccommodationReservation r in reservations)
            {

                ExpiredReservationDto item = new ExpiredReservationDto(r);

                expiredReservations.Add(item);
            }

            return expiredReservations;
        }

        public List<GuestReview> GetAll(User user)
        {
            return guestReviewService.GetAll(user);
        }

        public double GetAverageTidinessByUser(User user)
        {
            return guestReviewService.GetAverageTidinessByUser(user);
        }

        public double GetAverageObedienceByUser(User user)
        {
            return guestReviewService.GetAverageObedienceByUser(user);
        }

        public double GetAverageByAccommodationType(AccommodationType type, User user)
        {
            return guestReviewService.GetAverageByAccommodationType(type, user);
        }
    }
}
