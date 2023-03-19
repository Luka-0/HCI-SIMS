using InitialProject.Dto;
using InitialProject.Model;
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
        public static void Save(NewGuestReviewDto guestReviewDto) {

            GuestReview review = new GuestReview(guestReviewDto.Tidiness, guestReviewDto.Obedience, guestReviewDto.Comment);
            
            GuestReviewService.Save(review, guestReviewDto.ReservationId);
        
        }

        public static List<ExpiredReservationDto> LoadExpiredReservations()
        {

            List<AccommodationReservation> reservations = GuestReviewService.GetNotGradedExpiredReservations();

            List<ExpiredReservationDto> expiredReservations = new List<ExpiredReservationDto>();

            foreach (AccommodationReservation r in reservations)
            {

                ExpiredReservationDto item = new ExpiredReservationDto(r);

                expiredReservations.Add(item);
            }

            return expiredReservations;
        }

    }
}
