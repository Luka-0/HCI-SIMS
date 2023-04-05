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
        private AccommodationService _service = new (new AccommodationRepository());

        public void Register(NewAccommodationDto record) {

            Accommodation accommodation = new Accommodation(record.Title, record.GuestLimit, record.Type, record.MinimumReservationDays, record.CancellationDeadline);

            this._service.Save(accommodation, record.CityName, record.Images);

        }

        public void Save(Accommodation accommodation)
        {
            _service.Save(accommodation);
        }

        public List<Accommodation> GetAll()
        {
            return _service.GetAll();
        }

        public List<Accommodation> GetBy(string name)
        {
            return _service.GetBy(name);
        }

        public List<Accommodation> GetBy(Location location)
        {
            return _service.GetBy(location);
        }

        public List<Accommodation> GetByCity(string city)
        {
            return _service.GetByCity(city);
        }

        public List<Accommodation> GetBy(AccommodationType accommodationType)
        {
            return _service.GetBy(accommodationType);
        }

        public List<Accommodation> GetByGuestNumber(int guestNumber)
        {
            return _service.GetByGuestNumber(guestNumber);
        }

        public List<Accommodation> GetByReservationDays(int reservationDays)
        {
            return _service.GetByReservationDays(reservationDays);
        }
    }
}
