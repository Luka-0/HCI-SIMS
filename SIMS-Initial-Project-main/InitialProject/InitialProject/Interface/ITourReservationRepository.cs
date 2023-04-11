using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    public interface ITourReservationRepository
    {
        public List<TourReservation> GetAll();
        public List<TourReservation> GetByTour(Tour tour);
        public List<TourReservation> GetByGuest(User user);
        public void Save(TourReservation reservation, Tour tour, User guest, int guestNumber);

        public List<TourReservation> GetByTour(int id);
        public void SetArrivalKeyPoint(TourKeyPoint keyPoint, int id);

        public TourReservation GetById(int id);
        public void Delete(List<TourReservation> reservations);
    }
}
