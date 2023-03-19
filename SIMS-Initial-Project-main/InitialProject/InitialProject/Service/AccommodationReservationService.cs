using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service
{
    class AccommodationReservationService
    {
        private static readonly char dateSpliter = '-';
        private static readonly int dayPosition = 1;   // index of day in DateTime, 0 is starting (leftmost)

        public static bool Reservate(Accommodation accommodation, int guestNumber, string startingDate, string endingDate)
        {
            AccommodationReservation accommodationReservation = new AccommodationReservation();

            if(accommodation.GuestLimit < guestNumber || IsViolatingMinReservatingDays(accommodation, startingDate, endingDate))
            {
                return false;
            }

            if (IsAvailable(accommodation.Id, startingDate, endingDate))
            {
                //TODO dodeliti sve property-je novokreiranoj rezervaciji (problem je sa datumima)

                AccommodationReservationRepository.Add(accommodationReservation);
                return true;
            }

            return false;
        }

        public static bool IsAvailable(int id, string startingDate, string endingDate)
        {


            return true;
        }

        public static bool IsViolatingMinReservatingDays(Accommodation accommodation, string startingDate, string endingDate)
        {
            string[] startingDateSeparated = startingDate.Split(dateSpliter);
            int startingDay = int.Parse(startingDateSeparated[dayPosition]);

            string[] endingDateSeparated = endingDate.Split(dateSpliter);
            int endingDay = int.Parse(endingDateSeparated[dayPosition]);

            if(endingDay - startingDay + 1 < accommodation.MinimumReservationDays)
            {
                return true;
            }
            return false;
        }
    }
}
