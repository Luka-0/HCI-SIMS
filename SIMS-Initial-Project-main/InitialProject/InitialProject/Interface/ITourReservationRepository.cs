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
        public TourReservation GetById(int id);
        public List<TourReservation> GetByTour(Tour tour);
        public void Save(TourReservation reservation, Tour tour, User guest, int guestNumber);
    }
}
