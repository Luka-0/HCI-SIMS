using InitialProject.Contexts;
using InitialProject.Enumeration;
using InitialProject.Interface;
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
    public class AccommodationRepository : IAccommodationRepository
    {
        public AccommodationRepository() { }

        public void Save(Accommodation accommodation)
        {
            using var db = new UserContext();

            db.Add(accommodation);
            db.SaveChanges();

        }
        public void UpdateClassBy(User owner, string accommodationClass)
        {
            List<Accommodation> ownerAccommodations = new();

            using (UserContext db = new())
            {
                ownerAccommodations = db.accommodation
                    .Include(t => t.Location)
                    .Include(t=> t.Owner)
                    .Where(t => t.Owner.Equals(owner))
                    .ToList();

                ownerAccommodations.ForEach(t=>t.Class = accommodationClass);
                db.SaveChanges();
            }
        }

        public List<Accommodation> GetAll()
        {
            List<Accommodation> accommodations = new();

            using(UserContext db = new())
            {
                accommodations = db.accommodation.Include(t => t.Location).ToList();
            }

            return accommodations;
        }

        public List<Accommodation> GetBy(string name)
        {
            List<Accommodation> accommodations = new();

            using(UserContext db = new())
            {
                accommodations = db.accommodation.Include(t => t.Location).Where(t => t.Title.Contains(name)).ToList();
            }

            return accommodations;
        }

        public List<Accommodation> GetBy(Location location)
        {
            List<Accommodation> accommodations = new();

            using(UserContext db = new())
            {
                accommodations = db.accommodation.Include(t => t.Location).Where(t => t.Location == location).ToList();
            }

            return accommodations;
        }

        public List<Accommodation> GetByCity(string city)
        {
            List<Accommodation> accommodations = new();

            using(var db = new UserContext())
            {

                accommodations = db.accommodation.Include(t => t.Location).Where(t => t.Location.City.Equals(city)).ToList();
            }

            return accommodations;
        }

        public List<Accommodation> GetBy(AccommodationType accommodationType)
        {
            List<Accommodation> accommodations = new();

            using(UserContext db = new())
            {
                accommodations = db.accommodation.Include(t => t.Location).Where(t => t.Type == accommodationType).ToList();
            }

            return accommodations;
        }

        public List<Accommodation> GetByGuestNumber(int guestNumber)
        {
            List<Accommodation> accommodations = new();

            using(UserContext db = new())
            {
                accommodations = db.accommodation.Include(t => t.Location).Where(t => t.GuestLimit >= guestNumber).ToList();
            }

            return accommodations;
        }

        public List<Accommodation> GetByReservationDays(int reservationDays)
        {
            List<Accommodation> accommodations = new();

            using(UserContext db = new())
            {
                accommodations = db.accommodation.Include(t => t.Location).Where(t => t.MinimumReservationDays <= reservationDays).ToList();
            }

            return accommodations;
        }
    }
}
