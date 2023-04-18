using InitialProject.Model;
using InitialProject.NewFolder;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InitialProject.View;
using InitialProject.Interface;
using InitialProject.Dto;
using System.Printing;

namespace InitialProject.Service
{
    internal class AccommodationReservationService
    {
        private readonly IAccommodationReservationRepository IAccommodationreservationRepository;
        private UserService UserService;
        
        public AccommodationReservationService(IAccommodationReservationRepository iAccommodationreservationRepository)
        {
            this.IAccommodationreservationRepository = iAccommodationreservationRepository;
            this.UserService = new(new UserRepository());
        }

        public List<AccommodationReservation> getAllExpiredlBy(DateTime date, string ownerUsername) {

            //ProcessedDate processedDate = new ProcessedDate();
            //  Finished-->  TODO: napraviti interface za USER repository, povezati ga sa servisom i ovde pozvati taj servis
            User owner = UserService.GetBy(ownerUsername);

           // processedDate = SeparateDate(date);

            List<AccommodationReservation> expiredReservations = new List<AccommodationReservation>();
            expiredReservations = IAccommodationreservationRepository.GetAllExpiredBy(date.Day, date.Month, date.Year, owner);

            return expiredReservations;
            
            }
        /*
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
            }*/

        public AccommodationReservation GetBy(int id) {

            return IAccommodationreservationRepository.GetBy(id);
        }

        // Stajic
        public List<StartEndDateDto> GetAvailableDates(Accommodation accommodation, DateTime startingDate, DateTime endingDate, int daysToStay)
        {
            if (daysToStay < accommodation.MinimumReservationDays)
            {
                MessageBox.Show("Your reservation is not long enough for this accommodation");
                return null;
            }

            List<StartEndDateDto> datesToChose = new();

            TimeSpan difference = endingDate - startingDate;
            int iterations = (int)difference.TotalDays - daysToStay + 1;

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

            if (datesToChose.Count == 0)
            {
                MessageBox.Show("Accommodation is full during those days but we can reccommend another option");
                return null;
            }

            return datesToChose;

        }

        public List<StartEndDateDto> FindOtherDates(DateTime endDate, Accommodation accommodation, int daysToStay)
        {
            List<StartEndDateDto> dates = new();

            for(int i=0; ; ++i)
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

        /*public bool IsViolatingMinReservatingDays(int minimumReservationDays, DateTime startingDate, DateTime endingDate)
        {
            return endingDate.Day - startingDate.Day + 1 < minimumReservationDays;
        }*/

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

        public bool Delete(bool logicaly, AccommodationReservation accommodationReservation)
        {
            if (accommodationReservation.Accommodation.CancellationDeadline < 1)
            {
                if(DateTime.Now.AddDays(1)  > accommodationReservation.BegginingDate)
                {
                    return false;
                }
            }
            else
            {
                if (DateTime.Now.AddDays(accommodationReservation.Accommodation.CancellationDeadline) > accommodationReservation.BegginingDate)
                {
                    return false;
                }
            }

            if (logicaly)
            {
                IAccommodationreservationRepository.LogicalyDelete(accommodationReservation);
            }
            else
            {
                IAccommodationreservationRepository.Delete(accommodationReservation);
            }

            return true;
        }


        //Aleksandra
        
        //dobavlja sve rezervacije rezervisane(neotkazane) za period iz primljenog zahteva
        //parametar je bas zahtev kako broj parametara funkcije ne bi bio opterecen
        public List<AccommodationReservation> GetAllPreserved(ReservationReschedulingRequest reschedulingRequest, string ownerUsername) {

            int reservedAccommodationId;
            List<AccommodationReservation> preservedReservations = new List<AccommodationReservation>();
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();

            int accommodationId = reschedulingRequest.Reservation.Accommodation.Id;
            int reservationId = reschedulingRequest.Reservation.Id;

            User owner = this.UserService.GetBy(ownerUsername);
            reservations = this.IAccommodationreservationRepository.GetAllBetween(reschedulingRequest.NewStartingDate, reschedulingRequest.NewEndingDate, owner);
         
            foreach (var reservation in reservations) {

                reservedAccommodationId = reservation.Accommodation.Id;

                if (!reservation.Cancelled && reservedAccommodationId == accommodationId && reservationId != reservation.Id) {

                    preservedReservations.Add(reservation);
                }
            }
            return preservedReservations;
        }
        //aleksandra
        public void UpdateScheduledDatesBy(int id, DateTime newBegginingDate, DateTime newEndingDate) {

            this.IAccommodationreservationRepository.UpdateScheduledDatesBy(id, newBegginingDate, newEndingDate);
        }
        //aleksandra
        public List<AccommodationReservation> GetAllCancelled(string ownerUsername) {

            User owner = UserService.GetBy(ownerUsername);

            return this.IAccommodationreservationRepository.GetAllCancelled(owner);
        }
        


    }
}
