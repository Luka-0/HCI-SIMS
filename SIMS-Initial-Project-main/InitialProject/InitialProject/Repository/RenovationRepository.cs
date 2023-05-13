using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
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

            using UserContext db = new();

            db.ChangeTracker.TrackGraph(renovation, node =>
            node.Entry.State = !node.Entry.IsKeySet ? EntityState.Added : EntityState.Unchanged);

            db.SaveChanges();
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
    }
}
