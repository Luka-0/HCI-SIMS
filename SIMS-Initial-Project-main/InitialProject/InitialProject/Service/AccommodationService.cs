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
        private readonly IAccommodationRepository IAccommodationRepository;
        private LocationService LocationService;
        private ImageService ImageService;
        private UserService UserService;

        public AccommodationService(IAccommodationRepository iAccommodationRepository)
        {
            this.IAccommodationRepository = iAccommodationRepository;
            this.LocationService = new(new LocationRepository());
            this.ImageService = new(new ImageRepository());
            this.UserService = new(new UserRepository());

        }

        public void Save(Accommodation accommodation, string cityName, List<String> images, string ownerUsername)
        {
            //finished-->  TODO: napraviti interface za USER repository, povezati ga sa servisom i ovde pozvati taj servis
            User owner = UserService.GetBy(ownerUsername);

            //Saving new accommodation into databse
            this.IAccommodationRepository.Save(accommodation);

            var db = new UserContext();
            var existinAccommodation = db.accommodation.Find(accommodation.Id);   //Try creating method in Accommodation repository to return the same thing

            //Updating foreign key values of new accommodation record
            existinAccommodation.Location = LocationService.GetByCity(cityName);
            existinAccommodation.Owner = owner;

            //saving all images refered to new accommodation.
            ImageService.Save(images, accommodation);

            db.SaveChanges();
        }

        public void UpdateClassBy(string ownerUsername, bool superOwner) {

            //finished--> TODO: napraviti interface za USER repository, povezati ga sa servisom i ovde pozvati taj servis
            User owner = UserService.GetBy(ownerUsername);
            
            string accommodationClass = "B";

            if (superOwner) {
                accommodationClass = "A";
            }

            this.IAccommodationRepository.UpdateClassBy(owner, accommodationClass);
        }

        // Stajic
        public void Save(Accommodation accommodation)
        {
            IAccommodationRepository.Save(accommodation);
        }

        public List<Accommodation> GetAll()
        {
            return IAccommodationRepository.GetAll();
        }

        public List<Accommodation> GetBy(string name)
        {
            return IAccommodationRepository.GetBy(name);
        }

        public List<Accommodation> GetBy(Location location)
        {
            return IAccommodationRepository.GetBy(location);
        }

        public List<Accommodation> GetByCity(string city)
        {
            return IAccommodationRepository.GetByCity(city);
        }

        public List<Accommodation> GetBy(AccommodationType accommodationType)
        {
            return IAccommodationRepository.GetBy(accommodationType);
        }

        public List<Accommodation> GetByGuestNumber(int guestNumber)
        {
            return IAccommodationRepository.GetByGuestNumber(guestNumber);
        }

        public List<Accommodation> GetByReservationDays(int reservationDays)
        {
            return IAccommodationRepository.GetByReservationDays(reservationDays);
        }
    }
}
