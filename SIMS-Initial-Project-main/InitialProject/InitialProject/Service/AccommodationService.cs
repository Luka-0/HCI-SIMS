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
        private LocationService locationService;
        private ImageService imageService;

        public AccommodationService(IAccommodationRepository iAccommodationRepository)
        {
            this.iAccommodationRepository = iAccommodationRepository;
            this.locationService = new(new LocationRepository());
            this.imageService = new(new ImageRepository());
        }

        public void Save(Accommodation accommodation, string cityName, List<String> images, string ownerUsername)
        {
            //  TODO: napraviti interface za USER repository, povezati ga sa servisom i ovde pozvati taj servis
            User owner = UserRepository.Get(ownerUsername);

            //Saving new accommodation into databse
            this.iAccommodationRepository.Save(accommodation);

            var db = new UserContext();
            var tempRecord = db.accommodation.Find(accommodation.Id);   //Try creating method in Accommodation repository to return the same thing

            //Updating foreign key values of new accommodation record
            tempRecord.Location = locationService.GetByCity(cityName);
            tempRecord.Owner = owner;

            //saving all images refered to new accommodation.
            imageService.Save(images, accommodation);

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
