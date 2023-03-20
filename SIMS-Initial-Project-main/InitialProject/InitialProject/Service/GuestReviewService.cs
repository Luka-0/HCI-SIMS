using InitialProject.Contexts;
using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class GuestReviewService
    {
        public static void Save(GuestReview review, int reservationId) {

            GuestReviewRepository.Save(review);

            var db = new UserContext();
            var record = db.guestReview.Find(review.Id);   

            record.Reservation = AccommodationReservationService.GetBy(reservationId);

            db.SaveChanges();
        }

        public static List<AccommodationReservation> GetGradedReservations()
        {

            return GuestReviewRepository.GetGradedReservations();
        }

        public static List<AccommodationReservation> GetNotGradedExpiredReservations() {

            DateTime todaysDate = DateTime.UtcNow.Date;

            List<AccommodationReservation> expiredReservations = AccommodationReservationService.getAlExpiredlBy(todaysDate);

            List<AccommodationReservation> gradedReservations = GuestReviewService.GetGradedReservations();


            List<AccommodationReservation> nonGradedExpired = new List<AccommodationReservation>();

            foreach (AccommodationReservation r in expiredReservations)
            {
                bool exists = false;
                foreach (AccommodationReservation g in gradedReservations) {

                    if (r.Id == g.Id) {
                        exists = true; 
                    }
                }

                if (!exists) {
                    nonGradedExpired.Add(r);
                }
            }
            return nonGradedExpired;
        }
    }
}
