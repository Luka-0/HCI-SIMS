using InitialProject.Model;
using InitialProject.NewFolder;
using InitialProject.Repository;
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
    internal class AccommodationReservationService
    {
        public static List<AccommodationReservation> getAllExpiredlBy(DateTime date) {

            ProcessedDate processedDate = new ProcessedDate();

            processedDate = SeparateDate(date);

            List<AccommodationReservation> expiredReservations = new List<AccommodationReservation>();

            expiredReservations = AccommodationReservationRepository.GetAllExpiredBy(processedDate.Day, processedDate.Month, processedDate.Year);

            return expiredReservations;
            
            }

        public static ProcessedDate SeparateDate(DateTime date)
            {
            ProcessedDate processedDate = new ProcessedDate();  

            string formattedDate = date.ToString("dd-MM-yyyy");

            //MessageBox.Show(date.ToString("dd-MM-yyyy"));
            char[] delimiter = { '-' };

            string[] parts = formattedDate.Split(delimiter);

            processedDate.Day = Int32.Parse(parts[0]);
            processedDate.Month = Int32.Parse(parts[1]);
            processedDate.Year = Int32.Parse(parts[2]);

            return processedDate;
            }

        public static AccommodationReservation GetBy(int id) {

            return AccommodationReservationRepository.GetBy(id);
        }

        // Stajic
        public static bool Reservate(Accommodation accommodation, User user, int guestNumber, DateTime startingDate, DateTime endingDate)
        {
            AccommodationReservation ar = new AccommodationReservation();

            if (accommodation.GuestLimit < guestNumber || IsViolatingMinReservatingDays(accommodation.MinimumReservationDays, startingDate, endingDate))
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

            foreach (AccommodationReservation ar in accommodationReservations)
            {
                if ((startingDate < ar.BegginingDate && endingDate < ar.BegginingDate) || (startingDate > ar.EndingDate && endingDate > ar.EndingDate))
                {
                    continue;
                }
                else
                {
                    MessageBox.Show("Accommodation is already registered for those days");
                    return false;
                }
            }

            return true;
        }

        public static bool IsViolatingMinReservatingDays(int minimumReservationDays, DateTime startingDate, DateTime endingDate)
        {
            return endingDate.Day - startingDate.Day + 1 < minimumReservationDays;
        }
    }
}
