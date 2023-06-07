using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Interface;

namespace InitialProject.Controller
{
    public class ForumController
    {
        private readonly ForumService ForumService = new(new ForumRepository());
        private readonly AccommodationReservationService AccommodationReservationService = new(new AccommodationReservationRepository());

        public void Save(Forum forum)
        {
            ForumService.Save(forum);
        }

        public List<Forum> GetByUser(int id)
        {
            return ForumService.GetByUser(id);
        }

        public Forum GetByCity(string city)
        {
            return ForumService.GetByCity(city);
        }

        public bool IsSpecialComment(Forum forum, User user)
        {
            List<Location> locations = AccommodationReservationService.GetReservationLocationsByUser(user.Id);

            foreach(Location l in locations)
            {
                if (forum.Location.Id == l.Id) return true;
            }

            return false;
        }

        public void UpdateNumberOfSpecials(Forum forum)
        {
            ForumService.UpdateNumberOfSpecials(forum);
        }

        public void UpdateActivity(Forum forum)
        {
            ForumService.UpdateActivity(forum);
        }
    }
}
