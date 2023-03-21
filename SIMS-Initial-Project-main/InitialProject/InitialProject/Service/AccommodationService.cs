using InitialProject.Contexts;
using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    class AccommodationService
    {
        public static void Save(Accommodation accommodation, string cityName, List<String> images)
        {
            //Saving new accommodation into databse
            AccommodationRepository.Save(accommodation);

            var db = new UserContext();
            var tempRecord = db.accommodation.Find(accommodation.Id);   //Try creating method in Accommodation repository to return the same thing

            //Updating foreign key value of new accommodation record
            tempRecord.Location = LocationService.GetBy(cityName);
            
            //saving all images refered to new accommodation.
            ImageService.Save(images, accommodation);

            db.SaveChanges();
        }

    }
}
