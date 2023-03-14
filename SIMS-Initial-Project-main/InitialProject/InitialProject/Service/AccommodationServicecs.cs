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
    class AccommodationServicecs
    {
        public static void Save(NewAccommodationDto accommodationDto)
        {
            Accommodation accommodation = new Accommodation(accommodationDto.Title, accommodationDto.GuestLimit, accommodationDto.Type, accommodationDto.MinimumReservationDays, accommodationDto.CancellationDeadline);
            
            AccommodationRepository.Save(accommodation);

            var db = new UserContext();
            var tempRecord = db.accommodation.Find(accommodation.Id);

            tempRecord.Location = LocationRepository.getBy(accommodationDto.CityName);

            db.SaveChanges();
        }

    }
}
