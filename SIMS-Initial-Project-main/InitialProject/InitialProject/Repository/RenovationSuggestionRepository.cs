using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class RenovationSuggestionRepository : IRenovationSuggestionRepository
    {
        public RenovationSuggestionRepository() { }

        public void Save(RenovationSuggestion renovationSuggestion)
        {
            using var db = new UserContext();

            var existingLocation = db.location.Find(renovationSuggestion.AccommodationReservation.Accommodation.Location.Id);
            var existingAccommodation = db.accommodation.Find(renovationSuggestion.AccommodationReservation.Accommodation.Id);
            var existingAccommodationReservation = db.accommodationReservation.Find(renovationSuggestion.AccommodationReservation.Id);
            var existingGuest = db.users.Find(renovationSuggestion.AccommodationReservation.Guest.Id);

            renovationSuggestion.AccommodationReservation.Accommodation.Location = existingLocation;
            renovationSuggestion.AccommodationReservation.Accommodation = existingAccommodation;
            renovationSuggestion.AccommodationReservation = existingAccommodationReservation;
            renovationSuggestion.AccommodationReservation.Guest = existingGuest;

            db.location.Attach(existingLocation);
            db.accommodation.Attach(existingAccommodation);
            db.accommodationReservation.Attach(existingAccommodationReservation);
            db.users.Attach(existingGuest);

            db.Add(renovationSuggestion);
            db.SaveChanges();
        }

    }
}
