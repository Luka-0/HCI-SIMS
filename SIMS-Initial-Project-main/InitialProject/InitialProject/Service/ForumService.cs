using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    class ForumService
    {
        private readonly IForumRepository _forumRepository;
        private AccommodationService AccommodationService;

        public ForumService(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
            this.AccommodationService = new(new AccommodationRepository());
        }

        public void Save(Forum forum)
        {
            _forumRepository.Save(forum);
        }

        public List<Forum> GetByUser(int id)
        {
            return _forumRepository.GetByUser(id);
        }

        public Forum GetByCity(string city)
        {
            return _forumRepository.GetByCity(city);
        }

        public void UpdateNumberOfSpecials(Forum forum)
        {
            _forumRepository.UpdateNumberOfSpecials(forum);
        }

        public void UpdateActivity(Forum forum)
        {
            _forumRepository.UpdateActivity(forum);
        }

        public List<Forum> GetAll() {

            return _forumRepository.GetAll();
        }

        public List<string> GetOpenForumNotification()
        {

            List<Location> locations = new();
            List<string> openForums = new();

            locations = AccommodationService.GetOwnerAccommodationLocations("owner");

            foreach (var l in locations)
            {

                foreach (var f in this.GetAll())
                {
                    if (l.City.Equals(f.Location.City))
                    {

                        openForums.Add(l.City);
                    }
                }
            }

            return openForums;

        }

        public List<Location> GetOwnerAccommodationLocations(string owner) { 
        
            return this.AccommodationService.GetOwnerAccommodationLocations("owner");
        }






    }
}
