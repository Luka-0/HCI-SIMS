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
using InitialProject.Interface;
using InitialProject.Dto;

namespace InitialProject.Service
{
    internal class AccommodationReservationService
    {
        private readonly IAccommodationReservationRepository iAccommodationreservationRepository;
        private UserService UserService;
        
        public AccommodationReservationService(IAccommodationReservationRepository iAccommodationreservationRepository)
        {
            this.iAccommodationreservationRepository = iAccommodationreservationRepository;
            this.UserService = new(new UserRepository());
        }

        public List<AccommodationReservation> getAllExpiredlBy(DateTime date, string ownerUsername) {

            ProcessedDate processedDate = new ProcessedDate();
            //  Finished-->  TODO: napraviti interface za USER repository, povezati ga sa servisom i ovde pozvati taj servis
            User owner = UserService.GetBy(ownerUsername);

            processedDate = SeparateDate(date);

            List<AccommodationReservation> expiredReservations = new List<AccommodationReservation>();
            expiredReservations = iAccommodationreservationRepository.GetAllExpiredBy(processedDate.Day, processedDate.Month, processedDate.Year, owner);

            return expiredReservations;
            
            }

        public ProcessedDate SeparateDate(DateTime date)
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

        public AccommodationReservation GetBy(int id) {

            return IAccommodationreservationRepository.GetBy(id);
        }

        // Stajic
        public List<StartEndDateDto> GetAvailableDates(Accommodation accommodation, DateTime startingDate, DateTime endingDate, int daysToStay)
        {
            if (IsViolatingMinReservatingDays(accommodation.MinimumReservationDays, startingDate, endingDate))
            {
                return null;
            }

            List<StartEndDateDto> datesToChose = new();
            int iterations = endingDate.Day - startingDate.Day + 1 - daysToStay;
            for(int i=0; i<iterations; ++i)
            {
                DateTime tmpStartingDate = startingDate.AddDays(i);

                DateTime tmpEndingDate = startingDate.AddDays(i + daysToStay);

                StartEndDateDto tmp = new(tmpStartingDate, tmpEndingDate);
                datesToChose.Add(tmp);
            }

            for (int i = 0; i < datesToChose.Count; ++i)
            {
                StartEndDateDto tmp = datesToChose[i];
                if (!IsAvailable(accommodation.Id, tmp.StartingDate, tmp.EndingDate))
                { 
                    datesToChose.Remove(tmp);
                    --i;
                }
            }

            if (datesToChose.Count == 0) return null;

            return datesToChose;

        }

        public List<StartEndDateDto> FindOtherDates(DateTime endDate, Accommodation accommodation, int daysToStay)
        {
            List<StartEndDateDto> dates = new();

            for(int i=1; ; ++i)
            {
                DateTime newStartDate = endDate.AddDays(i);
                DateTime newEndDate = newStartDate.AddDays(daysToStay);

                if(IsAvailable(accommodation.Id, newStartDate, newEndDate))
                {
                    StartEndDateDto tmp = new(newStartDate, newEndDate);
                    dates.Add(tmp);
                    return dates;
                }
            }
        }


        public bool CreateReservation(Accommodation accommodation, DateTime startingDate, DateTime endingDate, int guestNumber, User user)
        {

            if (accommodation.GuestLimit < guestNumber) return false;

            AccommodationReservation ar = new()
            {
                Accommodation = accommodation,
                BegginingDate = startingDate,
                EndingDate = endingDate,
                GuestNumber = guestNumber,
                Guest = user
            };

            IAccommodationreservationRepository.Save(ar);
            return true;
        }

        public bool IsAvailable(int id, DateTime startingDate, DateTime endingDate)
        {
            List<AccommodationReservation> accommodationReservations = IAccommodationreservationRepository.GetByAccommodation(id);

            foreach (AccommodationReservation ar in accommodationReservations)
            {
                if (startingDate < ar.BegginingDate && endingDate < ar.BegginingDate)
                {
                    continue;
                }
                else if (startingDate > ar.EndingDate && endingDate > ar.EndingDate)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }



        public bool IsViolatingMinReservatingDays(int minimumReservationDays, DateTime startingDate, DateTime endingDate)
        {
            return endingDate.Day - startingDate.Day + 1 < minimumReservationDays;
        }

        public void Add(AccommodationReservation accommodationReservation)
        {
            IAccommodationreservationRepository.Save(accommodationReservation);
        }

        public List<AccommodationReservation> GetByAccommodation(int id)
        {
            return IAccommodationreservationRepository.GetByAccommodation(id);
        }

        public List<AccommodationReservation> GetBy(User user)
        {
            return IAccommodationreservationRepository.GetBy(user);
        }

    }
}
