using InitialProject.Contexts;
using InitialProject.Dto;
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
    public class GuestReviewService
    {
        private AccommodationReservationService AccommodationReservationService;
        private IGuestReviewRepository IGuestReviewRepository;

        public GuestReviewService(IGuestReviewRepository iGuestReviewRepository) {

            this.IGuestReviewRepository = iGuestReviewRepository;
            this.AccommodationReservationService = new(new AccommodationReservationRepository());
        }

        public void Save(GuestReview review, int reservationId) {

            IGuestReviewRepository.Save(review);

            var db = new UserContext();
            var record = db.guestReview.Find(review.Id);   

            record.Reservation = AccommodationReservationService.GetBy(reservationId);

            db.SaveChanges();
        }

        public List<AccommodationReservation> GetGradedReservations()
        {

            return IGuestReviewRepository.GetGradedReservations();
        }

        public List<AccommodationReservation> GetNotGradedExpiredReservations() {

            DateTime todaysDate = DateTime.UtcNow.Date;

            List<AccommodationReservation> expiredReservations = AccommodationReservationService.getAllExpiredlBy(todaysDate);

            List<AccommodationReservation> gradedReservations = IGuestReviewRepository.GetGradedReservations();


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
