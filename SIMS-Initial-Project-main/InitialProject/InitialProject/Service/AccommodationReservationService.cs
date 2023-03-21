using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.View;

namespace InitialProject.Service
{
    class AccommodationReservationService
    {
        public static bool Reservate(Accommodation accommodation, User user, int guestNumber, DateTime startingDate, DateTime endingDate)
        {
            AccommodationReservation ar = new AccommodationReservation();

            if(accommodation.GuestLimit < guestNumber || IsViolatingMinReservatingDays(accommodation.MinimumReservationDays, startingDate, endingDate))
            {
                return false;
            }

            if (IsAvailable(accommodation.Id, startingDate, endingDate))
            {
                ar.Accommodation = accommodation;
                ar.BegginingDate = startingDate;
                ar.EndingDate = endingDate;
                ar.GuestNumber = guestNumber;
                ar.Guest = user;

                AccommodationReservationRepository.Add(ar);
                return true;
            }

            return false;
        }

        public static bool IsAvailable(int id, DateTime startingDate, DateTime endingDate)
        {
            List<AccommodationReservation> accommodationReservations = AccommodationReservationRepository.GetByAccommodation(id);

            foreach(AccommodationReservation ar in accommodationReservations)   // Checks if chosen date is between some other days that are already reservated
            {
                if (startingDate > ar.BegginingDate && endingDate < ar.EndingDate)
                {
                    MessageBox.Show("Chosen accommodation is already registered during those days");
                    return false;
                }
            }

            return true;
        }

        public static bool IsViolatingMinReservatingDays(int minimumReservationDays, DateTime startingDate, DateTime endingDate)
        {
            return (endingDate - startingDate).Days + 1 < minimumReservationDays;
        }
    }
}
