using InitialProject.Contexts;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Microsoft.EntityFrameworkCore;
using InitialProject.Interface;

namespace InitialProject.Repository
{
    public class TourRepository : ITourRepository
    {
        //public TourRepository() { }


    /*public Location getLocationByTourId(int id)
    {
        Location location = new Location();
        using (var db = new UserContext())
        {
            location = db.tours.Where(a => a.Id == id).Select(a => a.Location).First();
            return location;
        }
    }
    */
    
        public Tour Save(Tour tour)
        {
            using var db = new UserContext();
            db.Add(tour);
            db.SaveChanges();
        
            return db.tour.Find(tour.Id);
            //Try creating method in Accommodation repository to return the same thing
        }

        public List<Tour> GetAll()
        {
            List<Tour> Tours = new List<Tour>();

            using (var dbContext = new UserContext())
            {
                Tours = dbContext.tour.Where(t => t.Location != null).Include(t => t.Location).ToList();

            }
            return Tours;
        }

        public Tour GetById(int id)
        {
            Tour tour = new Tour();

            using (var dbContext = new UserContext())
            {
                tour = (Tour)dbContext.tour
                                .Include(t => t.Location)
                                 .Where(t => t.Id == id);
            }
            return tour;
        }

        public List<Tour> GetByLocation(Location location)
        {
            List<Tour> Tours = new List<Tour>();

            using (var dbContext = new UserContext())
            {
                Tours = dbContext.tour
                                 .Include(t => t.Location)   
                                 .Where(t => t.Location == location)
                                 .ToList();
            }
            return Tours;
        }

        public List<Tour> GetByDuration(TimeSpan duration)
        {
            List<Tour> Tours = new List<Tour>();

            using (var dbContext = new UserContext())
            {
                Tours = dbContext.tour
                                 .Include(t => t.Location)
                                 .Where(t => t.Duration == duration)
                                 .ToList();
            }
            return Tours;
        }

        public List<Tour> GetByLanguage(string language)
        {
            List<Tour> Tours = new List<Tour>();

            using (var dbContext = new UserContext())
            {
                Tours = dbContext.tour
                                 .Include(t => t.Location)
                                 .Where(t => t.Language == language)
                                 .ToList();
            }
            return Tours;
        }

        public List<Tour> GetByGuestLimit(int guestLimit)
        {
            List<Tour> Tours = new List<Tour>();

            using (var dbContext = new UserContext())
            {
                Tours = dbContext.tour
                                 .Include(t => t.Location)
                                 .Where(t => t.GuestLimit == guestLimit)
                                 .ToList();
            }
            return Tours;
        }



    }
}
