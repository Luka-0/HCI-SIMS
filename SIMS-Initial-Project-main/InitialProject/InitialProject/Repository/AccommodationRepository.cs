using InitialProject.Contexts;
using InitialProject.Enumeration;
using InitialProject.Migrations;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

            using(UserContext db = new())
            {
                retVal = db.accommodation.ToList();
            }

            return retVal;
        }

        public static List<Accommodation> GetBy(string name)
        {
            List<Accommodation> retVal = new();

            using(UserContext db = new())
            {
                retVal = db.accommodation.Where(t => t.Title.Equals(name)).ToList();
            }

            return retVal;
        }

        public static List<Accommodation> GetBy(Location location)
        {
            List<Accommodation> retVal = new();

            using(UserContext db = new())
            {
                retVal = db.accommodation.Where(t => t.Location == location).ToList();
            }

            return retVal;
        }

        public static List<Accommodation> GetByCity(string city)
        {
            List<Accommodation> retVal = new();

            using(var db = new UserContext())
            {

                retVal = db.accommodation.Where(t => t.Location.City.Equals(city)).ToList();
            }

            return retVal;
        }

        public static List<Accommodation> GetBy(AccommodationType accommodationType)
        {
            List<Accommodation> retVal = new();

            using(UserContext db = new())
            {
                retVal = db.accommodation.Where(t => t.Type == accommodationType).ToList();
            }

            return retVal;
        }

        public static List<Accommodation> GetByGuestNumber(int guestNumber)
        {
            List<Accommodation> retVal = new();

            using(UserContext db = new())
            {
                retVal = db.accommodation.Where(t => t.GuestLimit >= guestNumber).ToList();
            }

            return retVal;
        }

        public static List<Accommodation> GetByReservationDays(int reservationDays)
        {
            List<Accommodation> retVal = new();

            using(UserContext db = new())
            {
                retVal = db.accommodation.Where(t => t.MinimumReservationDays <= reservationDays).ToList();
            }

            return retVal;
        }
    }
}
