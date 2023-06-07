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
    public class ForumOwnerRepository
    {

        public List<Location> GetOwnerAccommodationLocations(string owner)
        {
            List<Accommodation> accommodations = new();
            List<Location> locations = new();

            using (UserContext db = new())
            {
                accommodations = db.accommodation
                    .Include(t => t.Location)
                    .Include(t => t.Owner)
                    .Where(t => t.Owner.Username.Equals(owner))
                    .ToList();
            }

            foreach (var acc in accommodations) {
                if (!locations.Contains(acc.Location))
                { locations.Add(acc.Location); }
            }

            return locations;
        }


        public List<string> GetOpenForumNotification() {

            List<Location> locations = new();
            List<string> openForums = new();

            locations = GetOwnerAccommodationLocations("owner");

            foreach (var l in locations) {

                foreach (var f in this.GetAll())
                {
                    if (l.City.Equals(f.Location.City)) {

                        openForums.Add(l.City + ", by user: " + f.Guest.Username);
                    }
                }
            }

            return openForums;

        }


        public List<ForumOwnerVs> GetAll()
        {
            List<ForumOwnerVs> forums = new List<ForumOwnerVs>();

            using (UserContext db = new())
            {
                forums = db.ForumOwnerVs
                    .Include(t => t.Location)
                    .Include(t => t.Guest)
                    .ToList();
            }


            return forums;
        }
    }
}
