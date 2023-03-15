using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class AccommodationController
    {
        public static void Register(NewAccommodationDto record) {

            Accommodation accommodation = new Accommodation(record.Title, record.GuestLimit, record.Type, record.MinimumReservationDays, record.CancellationDeadline);

            AccommodationService.Save(accommodation, record.CityName, record.Images);

        }

    }
}
