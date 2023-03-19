using InitialProject.Contexts;
using InitialProject.Model;
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
            using var db = new UserContext();

            db.Add(accommodationReservation);
            db.SaveChanges();
        }

        public static List<AccommodationReservation> GetByAccommodationId(int id)
        {
            List<AccommodationReservation> retVal = new();

            using var db = new UserContext();
            foreach (AccommodationReservation ar in db.accommodationReservation)
            {
                if (ar.Accommodation.Id == id)
                {
                    retVal.Add(ar);
                }
            }

            return retVal;
        }
    }
}
