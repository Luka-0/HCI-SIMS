using InitialProject.Contexts;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class TourRepository
    {
        public TourRepository() { }

        public  List<Tour> GetAll()
        {
            List<Tour> Tours = new List<Tour>();

            using (var dbContext = new UserContext())
            {
                Tours = dbContext.tour.ToList();
            }
            return Tours;
        }

        public List<Tour> GetBy(Location location)
        {
            List<Tour> Tours = new List<Tour>();

            using (var dbContext = new UserContext())
            {
                Tours = dbContext.tour
                                 .Where(t => t.Location == location)
                                 .ToList();
            }
            return Tours;
        }

    }
}
