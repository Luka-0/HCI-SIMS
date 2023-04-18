using InitialProject.Contexts;
using InitialProject.Enumeration;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    interface IAccommodationRepository
    {
        public void Save(Accommodation accommodation);
        public void UpdateClassBy(User owner, string accommodationClass);
        public List<Accommodation> GetAll();
        public List<Accommodation> GetBy(string name);
        public List<Accommodation> GetBy(Location location);
        public List<Accommodation> GetByCity(string city);
        public List<Accommodation> GetBy(AccommodationType accommodationType);
        public List<Accommodation> GetByGuestNumber(int guestNumber);
        public List<Accommodation> GetByReservationDays(int reservationDays);

    }
}
