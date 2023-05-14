using InitialProject.Contexts;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    public interface IAccommodationReservationRepository
    {
        public List<AccommodationReservation> GetAllExpiredBy(int day, int month, int year, User owner);
        public AccommodationReservation GetBy(int id);
        public List<AccommodationReservation> GetAllBetween(DateTime startingDate, DateTime endingDate, User owner);
        public void UpdateScheduledDatesBy(int id, DateTime newBegginingDate, DateTime newEndingDate);
        public List<AccommodationReservation> GetAllCancelled(User owner);
        public List<AccommodationReservation> GetAllByDateInterval(Accommodation accommodation, DateTime start, DateTime end);
        public List<int> GetReservationYearsBy(Accommodation accommodation);
        public int GetCountBy(int year, Accommodation accommodation);
        public int GetCountBy(int year, int month, Accommodation accommodation);
        public int GetCancellationCountBy(int year, Accommodation accommodation);
        public int GetCancellationCountBy(int year, int month, Accommodation accommodation);
        public double GetOccupancyBy(int year, Accommodation accommodation);
        public double GetOccupancyBy(int year, int month, Accommodation accommodation);

         // Stajic
        public void Save(AccommodationReservation accommodationReservation);
        public List<AccommodationReservation> GetByAccommodation(int id);
        public List<AccommodationReservation> GetBy(User user);
        public AccommodationReservation GetByIdWithInclude(int id);
        public void Delete(AccommodationReservation accommodationReservation);
        public void LogicalyDelete(AccommodationReservation accommodationReservation);
        public List<AccommodationReservation> GetDuringLastYearBy(User user);
       
        }
}
