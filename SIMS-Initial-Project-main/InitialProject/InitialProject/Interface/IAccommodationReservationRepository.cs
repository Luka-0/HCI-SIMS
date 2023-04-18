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
        public AccommodationReservation GetBy(int id);
        public List<AccommodationReservation> GetAllBetween(DateTime startingDate, DateTime endingDate, User owner);
        public void UpdateScheduledDatesBy(int id, DateTime newBegginingDate, DateTime newEndingDate);
        public List<AccommodationReservation> GetAllCancelled(User owner);

         // Stajic
        public void Save(AccommodationReservation accommodationReservation);
        public List<AccommodationReservation> GetByAccommodation(int id);
        public List<AccommodationReservation> GetBy(User user);
        public void Delete(AccommodationReservation accommodationReservation);
        public void LogicalyDelete(AccommodationReservation accommodationReservation);
        }
}
