using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.View.Owner;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class RenovationRepository : IRenovationRepository
    {
        public void Save(Renovation renovation) {

            UpdateLastAccommodationRenovation(renovation);

            using (UserContext db = new())
            {
                db.ChangeTracker.TrackGraph(renovation, node =>
                node.Entry.State = !node.Entry.IsKeySet ? EntityState.Added : EntityState.Unchanged);

                db.SaveChanges();
            }
        }

        public void Delete(Renovation erasureRenovation)
        {
            using (UserContext db = new())
            {
                Accommodation accommodationRenovation = GetAccommodationBy(erasureRenovation);

                db.Remove(erasureRenovation);

                db.SaveChanges();

                UpdateLastAccommodationRenovation(GetLastRenovationBy(accommodationRenovation));

                db.SaveChanges();
            }
        }

        public List<Renovation> GetAllBy(string ownerUsername)
        {
            List<Renovation> renovations = new();

            using (UserContext db = new())
            {
                renovations = db.renovation
                                .Include(t => t.Accommodation)
                                .ThenInclude(t => t.Owner)
                                .Where(t => t.Accommodation.Owner.Username.Equals(ownerUsername))
                                .ToList();
            }
            return renovations;
        }

        public List<Renovation> GetAllBetweenBy(Accommodation accommodation, DateTime startingDate, DateTime endingDate)
        {

            List<Renovation> renovations = new List<Renovation>();

            using (var dbContext = new UserContext())
            {
                renovations = dbContext.renovation
                                           .Where(
                                                    reno => ((startingDate >= reno.Start && startingDate <= reno.End) || (endingDate >= reno.Start && endingDate <= reno.End)) ||
                                                            (reno.Start >= startingDate && reno.Start <= endingDate) || (reno.End >= startingDate && (reno.End <= endingDate))
                                                  )
                                           .Include(r => r.Accommodation)
                                                .ThenInclude(r => r.Owner)
                                           .Where(t => t.Accommodation.Id == (accommodation.Id))
                                           .ToList();
            }
            return renovations;
        }
        private void UpdateLastAccommodationRenovation(Renovation renovation) {

            List<Accommodation> accommodations = new();

            using (UserContext db = new())
            {
                accommodations = db.accommodation.Where(t => t.Id.Equals(renovation.Accommodation.Id))
                    .Include(t => t.Location)
                    .Include(t => t.Owner)
                    .ToList();

                accommodations.ForEach(t => t.LastRenovation = renovation.End);
                db.SaveChanges();
            }
        }

        private Accommodation GetAccommodationBy(Renovation existingRenovation) {

            Renovation renovation = new Renovation();

            using (UserContext db = new())
            {
                renovation = db.renovation
                                   .Where(t => t.Id.Equals(existingRenovation.Id))
                                   .Include(t=>t.Accommodation)
                                   .First();
            }


            return renovation.Accommodation;
        }


        private Renovation GetLastRenovationBy(Accommodation accommodation) {

            Renovation renovation = new Renovation();

            using (UserContext db = new())
            {
                renovation = db.renovation
                                   .Include(t => t.Accommodation)
                                   .Where(t => t.Accommodation.Id.Equals(accommodation.Id))
                                   .OrderBy(t => t.End).Last();
            }

            return renovation;
        }
    }
}
