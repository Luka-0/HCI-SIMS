using InitialProject.Contexts;
using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class TourReservationController
    {
        private  TourReservationService tourReservationService = new TourReservationService(new TourReservationRepository());
        public TourReservationResponseDto Reserve(Tour tour, int guestNumber)
        {
            TourReservationResponseDto responseDto;

            if(tourReservationService.GetAvailableSpace(tour) < guestNumber)
                responseDto = new TourReservationResponseDto(true, tourReservationService.GetAvailableSpace(tour));
            else
                responseDto = new TourReservationResponseDto(false, tourReservationService.GetAvailableSpace(tour));

            return responseDto;
        }

        public List<TourReservation> GetAll()
        {
            return tourReservationService.GetAll();
        }

        public List<TourReservation> GetByTour(Tour tour)
        {
            return tourReservationService.GetByTour(tour);
        }

        public void Save(TourReservation reservation, Tour tour, User guest, int guestNumber)
        {
            tourReservationService.Save(reservation, tour, guest, guestNumber);
        }

        public int CountGuestsOnTour(Tour tour)
        {
            return tourReservationService.CountGuestsOnTour(tour);
        }

        public int CountTourReservations(Tour tour)
        {
            return tourReservationService.CountTourReservations(tour);
        }

        public List<TourReservation> GetByTour(int id)
        {
            return tourReservationService.GetByTour(id);
        }

        public void SetArrivalKeyPoint(TourKeyPoint keyPoint, int reservationId)    
        {
            tourReservationService.SetArrivalKeyPoint(keyPoint, reservationId);
        }

        public TourReservation GetById(int id)
        {
            return tourReservationService.GetById(id);
        }

        public string GetArrivalKeyPointName(int id)
        {
            return tourReservationService.GetArrivalKeyPointName(id);
        }
    }
}
