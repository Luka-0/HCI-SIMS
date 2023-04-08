using InitialProject.Contexts;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    interface IAccommodationReservationRepository
    {
        public List<AccommodationReservation> GetAllExpiredBy(int day, int month, int year, User owner);
        //public Accommodation GetAccommodation(int reservationId);
        //public User GetUser(int reservationId);
        public AccommodationReservation GetBy(int id);

        // Stajic
        public void Save(AccommodationReservation accommodationReservation);
        public List<AccommodationReservation> GetByAccommodation(int id);
        public List<AccommodationReservation> GetBy(User user);

    }
}
