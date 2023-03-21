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
        public static List<AccommodationReservation> getAlExpiredlBy(DateTime date) {

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
    }
}
