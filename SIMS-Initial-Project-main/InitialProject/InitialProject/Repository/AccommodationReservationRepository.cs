using InitialProject.Contexts;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    class AccommodationReservationRepository
    {
        public AccommodationReservationRepository() { }

        public static void Add(AccommodationReservation accommodationReservation)
        {
            using UserContext db = new();

            db.ChangeTracker.TrackGraph(accommodationReservation, node =>
            node.Entry.State = !node.Entry.IsKeySet ? EntityState.Added : EntityState.Unchanged);

            db.SaveChanges();
        }

        public static List<AccommodationReservation> GetByAccommodation(int id)
        {
            List<AccommodationReservation> retVal = new();

            using(UserContext db = new())
            {
                retVal = db.accommodationReservation.Where(t => t.Id == id).ToList();
            }

            return retVal;
        }
    }
}
