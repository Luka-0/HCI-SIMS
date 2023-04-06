using InitialProject.Contexts;
using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Service
{
    class AccommodationService
    {
        private readonly IAccommodationRepository iAccommodationRepository;
        LocationController LocationController = new();

        public AccommodationService(IAccommodationRepository iAccommodationRepository)
        {
            this.iAccommodationRepository = iAccommodationRepository;
        }

        public void Save(Accommodation accommodation, string cityName, List<String> images)
        {
            //Saving new accommodation into databse
            this.iAccommodationRepository.Save(accommodation);

            var db = new UserContext();
            var tempRecord = db.accommodation.Find(accommodation.Id);   //Try creating method in Accommodation repository to return the same thing

            //Updating foreign key value of new accommodation record
            tempRecord.Location = LocationController.GetByCity(cityName);
            
            //saving all images refered to new accommodation.
            ImageService.Save(images, accommodation);

            db.SaveChanges();
        }

        // Stajic
        public void Save(Accommodation accommodation)
        {
            iAccommodationRepository.Save(accommodation);
        }

        public List<Accommodation> GetAll()
        {
            return iAccommodationRepository.GetAll();
        }

        public List<Accommodation> GetBy(string name)
        {
            return iAccommodationRepository.GetBy(name);
        }

        public List<Accommodation> GetBy(Location location)
        {
            return iAccommodationRepository.GetBy(location);
        }

        public List<Accommodation> GetByCity(string city)
        {
            return iAccommodationRepository.GetByCity(city);
        }

        public List<Accommodation> GetBy(AccommodationType accommodationType)
        {
            return iAccommodationRepository.GetBy(accommodationType);
        }

        public List<Accommodation> GetByGuestNumber(int guestNumber)
        {
            return iAccommodationRepository.GetByGuestNumber(guestNumber);
        }

        public List<Accommodation> GetByReservationDays(int reservationDays)
        {
            return iAccommodationRepository.GetByReservationDays(reservationDays);
        }
    }
}
