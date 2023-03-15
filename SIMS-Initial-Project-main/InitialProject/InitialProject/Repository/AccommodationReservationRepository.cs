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

        public static bool AddAccommodationReservation(AccommodationReservation accommodationReservation)
        {
            using var db = new UserContext();

            /*if(accommodationReservation.Accommodation.MinimumReservationDays > )
            {

            }*/

            if(accommodationReservation.Accommodation.GuestLimit < accommodationReservation.GuestNumber)
            {
                return false;
            }

            db.Add(accommodationReservation);
            db.SaveChanges();

            return true;
        }
    }
}
