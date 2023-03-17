using InitialProject.Contexts;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class TourReservationRepository
    {
        public TourReservationRepository() { }

        public List<TourReservation> GetAll()
        {
            List<TourReservation> reservations = new List<TourReservation>();

            using (var dbContext = new UserContext())
            {
                reservations = dbContext.tourReservations.ToList();
            }
            return reservations;
        }

        public TourReservation GetById(int id)
        {
            TourReservation reservation = new TourReservation();

            using (var dbContext = new UserContext())
            {
                reservation = (TourReservation)dbContext.tourReservations
                                 .Where(t => t.Id == id);
            }
            return reservation;
        }

        //CountBy given tour/tourId
        public int CountBy(Tour tour)
        {
            int count = 0;

            using (var dbContext = new UserContext())
            {
                count = dbContext.tourReservations
                                 .Where(t => t.Id == tour.Id)
                                 .Count();
            }
            return count;
        }











    }
}
