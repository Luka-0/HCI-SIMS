using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class AccommodationController
    {
        private readonly AccommodationService AccommodationService = new (new AccommodationRepository());

        public void Register(NewAccommodationDto record) {

            Accommodation accommodation = new Accommodation(record.Title, record.GuestLimit, record.Type, record.MinimumReservationDays, record.CancellationDeadline);

            this.AccommodationService.Save(accommodation, record.CityName, record.Images, record.OwnerUsername);

        }
        public void UpdateClassBy(string ownerUsername, bool superOwner)
        {
            this.AccommodationService.UpdateClassBy(ownerUsername, superOwner);
        
        }

        public void Save(Accommodation accommodation)
        {
            AccommodationService.Save(accommodation);
        }

        public List<Accommodation> GetAll()
        {
            return AccommodationService.GetAll();
        }

        public List<Accommodation> GetBy(string name)
        {
            return AccommodationService.GetBy(name);
        }

        public List<Accommodation> GetBy(Location location)
        {
            return AccommodationService.GetBy(location);
        }

        public List<Accommodation> GetByCity(string city)
        {
            return AccommodationService.GetByCity(city);
        }

        public List<Accommodation> GetBy(AccommodationType accommodationType)
        {
            return AccommodationService.GetBy(accommodationType);
        }

        public List<Accommodation> GetByGuestNumber(int guestNumber)
        {
            return AccommodationService.GetByGuestNumber(guestNumber);
        }

        public List<Accommodation> GetByReservationDays(int reservationDays)
        {
            return AccommodationService.GetByReservationDays(reservationDays);
        }
    }
}
