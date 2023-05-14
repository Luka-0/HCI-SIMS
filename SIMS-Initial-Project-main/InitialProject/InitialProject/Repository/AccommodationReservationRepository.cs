using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;

namespace InitialProject.Repository
{
    public class AccommodationReservationRepository : IAccommodationReservationRepository
    {
        public List<AccommodationReservation> GetAllExpiredBy(int day, int month, int year, User owner)
        {
            List<AccommodationReservation> expiredReservations = new List<AccommodationReservation>();

            using (var dbContext = new UserContext())
            {
                expiredReservations = dbContext.accommodationReservation
                                 .Where(ar => ((ar.EndingDate.Day >= (day - 5)) && (ar.EndingDate.Day <= day))
                                            && (ar.EndingDate.Month == month)
                                            && (ar.EndingDate.Year == year)
                                 )
                                 .Include(r=>r.Accommodation)
                                    .ThenInclude(r=>r.Owner).Where(t => t.Accommodation.Owner.Equals(owner))
                                 .Include(r=>r.Guest)
                                 .ToList();
            }

            return expiredReservations;
        }

        public AccommodationReservation GetBy(int id)
        {

            AccommodationReservation reservation = new AccommodationReservation();

            using (var dbContext = new UserContext())
            {
                reservation = (AccommodationReservation)dbContext.accommodationReservation
                                 .Where(a => a.Id == id).First();
            }
            return reservation;
        }

        // Stajic
        public void Save(AccommodationReservation accommodationReservation)
        {
            using UserContext db = new();

            db.ChangeTracker.TrackGraph(accommodationReservation, node =>
            node.Entry.State = !node.Entry.IsKeySet ? EntityState.Added : EntityState.Unchanged);

            db.SaveChanges();
        }

        public List<AccommodationReservation> GetByAccommodation(int id)
        {
            List<AccommodationReservation> reservations = new();

            using (UserContext db = new())
            {
                reservations = db.accommodationReservation.Where(t => t.Accommodation.Id == id).ToList();
            }

            return reservations;
        }

        public List<AccommodationReservation> GetBy(User user)
        {
            List<AccommodationReservation> reservations = new();

            using (UserContext db = new())
            {
                reservations = db.accommodationReservation.Include(t => t.Accommodation)
                                                          .ThenInclude(l => l.Location)
                                                          .Where(t => t.Guest.Id == user.Id).ToList();
            }

            return reservations;

        }

        public AccommodationReservation GetByIdWithInclude(int id)
        {
            AccommodationReservation reservation = null;

            using(UserContext db = new())
            {
                reservation = db.accommodationReservation.Where(t => t.Id == id)
                                                         .Include(t => t.Accommodation)
                                                         .ThenInclude(l => l.Location)
                                                         .Include(t => t.Guest)
                                                         .First();
            }

            return reservation;
        }

        public void Delete(AccommodationReservation accommodationReservation)
        {
            using var db = new UserContext();

            db.accommodationReservation.Remove(accommodationReservation);
            db.SaveChanges();
        }

        public void LogicalyDelete(AccommodationReservation accommodationReservation)
        {
            using var db = new UserContext();

            AccommodationReservation accommodationToDelete = GetBy(accommodationReservation.Id);

            var entityToUpdate = db.accommodationReservation.Find(accommodationReservation.Id);

            if (entityToUpdate != null)
            {
                entityToUpdate.Cancelled = true;
                db.SaveChanges();
            }
        }

        public List<AccommodationReservation> GetDuringLastYearBy(User user)
        {
            List<AccommodationReservation> reservations = new();

            using (UserContext db = new())
            {
                reservations = db.accommodationReservation.Where(t => t.Guest.Id == user.Id && t.BegginingDate > DateTime.Now.AddYears(-1))
                                                          .ToList();
            }

            return reservations;
        }



        //Aleksandra
        public List<AccommodationReservation> GetAllBetween(DateTime startingDate, DateTime endingDate, User owner) {

            List<AccommodationReservation> accommodationReservations = new List<AccommodationReservation>();

            using (var dbContext = new UserContext())
            {
                accommodationReservations = dbContext.accommodationReservation
                                            .Where(ar => ((startingDate >= ar.BegginingDate && startingDate <= ar.EndingDate) || (endingDate >= ar.BegginingDate && endingDate <= ar.EndingDate)) ||
                                                         (ar.BegginingDate >= startingDate && ar.BegginingDate <= endingDate) || (ar.EndingDate >= startingDate && (ar.EndingDate <= endingDate))
                                                   )
                                           .Include(r => r.Accommodation)
                                                .ThenInclude(r => r.Owner).Where(t => t.Accommodation.Owner.Equals(owner))
                                           .Include(r => r.Guest)
                                           .ToList();
            }
            return accommodationReservations;
        }

        public void UpdateScheduledDatesBy(int id, DateTime newBegginingDate, DateTime newEndingDate) {

            AccommodationReservation accommodationReservation = new();

            var db = new UserContext();
            accommodationReservation = db.accommodationReservation.Find(id);

            accommodationReservation.BegginingDate = newBegginingDate;
            accommodationReservation.EndingDate = newEndingDate;
            db.SaveChanges();
        }

        public List<AccommodationReservation> GetAllCancelled(User owner) {

            List<AccommodationReservation> cancelledReservations = new List<AccommodationReservation>();

            using (var dbContext = new UserContext())
            {
                cancelledReservations = dbContext.accommodationReservation
                                            .Where(ar => ar.Cancelled == true)
                                           .Include(r => r.Accommodation)
                                                .ThenInclude(r => r.Owner).Where(t => t.Accommodation.Owner.Equals(owner))
                                           .Include(r => r.Guest)
                                           .ToList();
            }
            return cancelledReservations;

        }

        public List<AccommodationReservation> GetAllByDateInterval(Accommodation accommodation, DateTime start, DateTime end) { 
        
        
            List<AccommodationReservation> accommodationReservations = new List<AccommodationReservation>();

                using (var dbContext = new UserContext())
                {
                    accommodationReservations = dbContext.accommodationReservation
                                                .Where(ar => ((start >= ar.BegginingDate && start <= ar.EndingDate) || (end >= ar.BegginingDate && end <= ar.EndingDate)) ||
                                                             (ar.BegginingDate >= start && ar.BegginingDate <= end) || (ar.EndingDate >= start && (ar.EndingDate <= end))
                                                       )
                                               .Include(r => r.Accommodation).ThenInclude(a=>a.Location)
                                               .Include(r => r.Guest)
                                               .Where(t => t.Accommodation.Equals(accommodation))
                                               .OrderBy(t=> t.BegginingDate)
                                               .ToList();
                }
                return accommodationReservations;
        
        }


        public List<int> GetReservationYearsBy(Accommodation accommodation)
        {
            List<int> reservationYears = new List<int>();

            using (var dbContext = new UserContext())
            {
                    reservationYears = dbContext.accommodationReservation
                                               .Include(r => r.Accommodation)
                                               .Where(r => r.Accommodation.Equals(accommodation))
                                               .Select(r => r.BegginingDate.Year).Distinct().ToList();
            }

            return reservationYears;
        }

        public int GetCountBy(int year, Accommodation accommodation) {

            List<AccommodationReservation> annualReservations = new List<AccommodationReservation>();

            using (var dbContext = new UserContext())
            {
                annualReservations = dbContext.accommodationReservation
                                           .Where(r => r.BegginingDate.Year == year)
                                           .Include(r => r.Accommodation)
                                           .Where(r => r.Accommodation.Equals(accommodation)).ToList();                                          
            }

            return annualReservations.Count;
        }

        public int GetCountBy(int year, int month, Accommodation accommodation) {

            List<AccommodationReservation> monthlyReservations = new List<AccommodationReservation>();

            using (var dbContext = new UserContext())
            {
                monthlyReservations = dbContext.accommodationReservation
                                           .Where(r => r.BegginingDate.Year == year && r.BegginingDate.Month == month)
                                           .Include(r => r.Accommodation)
                                           .Where(r => r.Accommodation.Equals(accommodation)).ToList();
            }

            return monthlyReservations.Count;
        }

        public int GetCancellationCountBy(int year, Accommodation accommodation)
        {

            List<AccommodationReservation> annualCanceledReservations = new List<AccommodationReservation>();

            using (var dbContext = new UserContext())
            {
                annualCanceledReservations = dbContext.accommodationReservation
                                            .Where(r => r.BegginingDate.Year == year)
                                            .Where(r=>r.Cancelled == true)
                                            .Include(r => r.Accommodation)
                                            .Where(r => r.Accommodation.Equals(accommodation)).ToList();
            }

            return annualCanceledReservations.Count;
        }

        public int GetCancellationCountBy(int year, int month, Accommodation accommodation)
        {

            List<AccommodationReservation> canceledReservations = new List<AccommodationReservation>();

            using (var dbContext = new UserContext())
            {
                canceledReservations = dbContext.accommodationReservation
                                            .Where(r => r.BegginingDate.Year == year && r.BegginingDate.Month == month)
                                            .Where(r => r.Cancelled == true)
                                            .Include(r => r.Accommodation)
                                            .Where(r => r.Accommodation.Equals(accommodation)).ToList();
            }

            return canceledReservations.Count;
        }

        public double GetOccupancyBy(int year, Accommodation accommodation)
        {

            List<AccommodationReservation> annualReservations = new List<AccommodationReservation>();

            using (var dbContext = new UserContext())
            {
                annualReservations = dbContext.accommodationReservation
                                           .Where(r => r.BegginingDate.Year == year)
                                           .Include(r => r.Accommodation)
                                           .Where(r => r.Accommodation.Equals(accommodation)).ToList();
            }

            TimeSpan duration = new TimeSpan(0,0,0,0);
            
            foreach(var reservation in annualReservations) {

                duration += (reservation.EndingDate - reservation.BegginingDate);
            
            }

            return Math.Round(Convert.ToDouble(duration.Days)/366 * 100,2);
        }


        public double GetOccupancyBy(int year, int month, Accommodation accommodation)
        {

            List<AccommodationReservation> monthlyReservations = new List<AccommodationReservation>();

            using (var dbContext = new UserContext())
            {
                monthlyReservations = dbContext.accommodationReservation
                                           .Where(r => r.BegginingDate.Year == year && r.BegginingDate.Month == month)
                                           .Include(r => r.Accommodation)
                                           .Where(r => r.Accommodation.Equals(accommodation)).ToList();
            }

            TimeSpan duration = new TimeSpan(0, 0, 0, 0);

            foreach (var reservation in monthlyReservations){

                duration += (reservation.EndingDate - reservation.BegginingDate);

            }

            return Math.Round(Convert.ToDouble(duration.Days) / 366 * 100, 2);
        }

    }
}
