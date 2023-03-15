using InitialProject.Contexts;
using InitialProject.Enumeration;
using InitialProject.Migrations;
using InitialProject.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Repository
{
    public class AccommodationRepository
    {
        public AccommodationRepository() { }

        public static void Save(Accommodation accommodation)
        {
            using var db = new UserContext();

            db.Add(accommodation);
            db.SaveChanges();

        }

        public static List<Accommodation> GetAll()
        {
            List<Accommodation> retVal = new();

            using var db = new UserContext();
            foreach (Accommodation accommodation in db.accommodation)
            {
                retVal.Add(accommodation);
            }

            if(retVal == null)  //TODO
            {

            }

            return retVal;
        }

        public static List<Accommodation> GetByName(string name)
        {
            List<Accommodation> retVal = new();

            using var db = new UserContext();
            foreach(Accommodation accommodation in db.accommodation)
            {
                if (accommodation.Title.Contains(name))
                {
                    retVal.Add(accommodation);
                }
            }

            if(retVal == null)  // TODO
            {

            }

            return retVal;
        }

        public static List<Accommodation> GetBy(Location location)
        {
            List<Accommodation> retVal = new();

            using var db = new UserContext();
            foreach (Accommodation accommodation in db.accommodation)
            {
                if (accommodation.Location.Equals(location))    //TODO: proveriti da li se radi sa equals ili ==
                {
                    retVal.Add(accommodation);
                }
            }

            if (retVal == null)  // TODO
            {

            }

            return retVal;
        }

        public static List<Accommodation> GetByType(AccommodationType accommodationType)
        {
            List<Accommodation> retVal = new();

            using var db = new UserContext();
            foreach (Accommodation accommodation in db.accommodation)
            {
                if (accommodation.Type == accommodationType)
                {
                    retVal.Add(accommodation);
                }
            }

            if (retVal == null) //TODO
            {

            }

            return retVal;
        }

        public static List<Accommodation> GetByGuestNumber(int guestNumber)
        {
            List<Accommodation> retVal = new();

            using var db = new UserContext();
            foreach(Accommodation accommodation in db.accommodation)
            {
                if(accommodation.GuestLimit >= guestNumber)
                {
                    retVal.Add(accommodation);
                }
            }

            if (retVal == null) //TODO
            {

            }

            return retVal;
        }

        public static List<Accommodation> GetByReservationDays(int reservationDays)
        {
            List<Accommodation> retVal = new();

            using var db = new UserContext();
            foreach(Accommodation accommodation in db.accommodation)
            {
                if(accommodation.MinimumReservationDays < reservationDays)
                {
                    retVal.Add(accommodation);
                }
            }

            if(retVal == null)  //TODO
            {

            }

            return retVal;
        }
    }
}
