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
    public class TourReservationControler
    {
        private  TourReservationService service = new TourReservationService();
        public TourReservationResponseDto Reserve(Tour tour, int guestNumber)
        {
            TourReservationResponseDto responseDto;

            if(service.GetAvailableSpace(tour) < guestNumber)
                responseDto = new TourReservationResponseDto(true, service.GetAvailableSpace(tour));
            else
                responseDto = new TourReservationResponseDto(false, service.GetAvailableSpace(tour));

            return responseDto;
        }

        public List<TourReservation> GetAll()
        {
            return service.GetAll();
        }

        public void Save(TourReservation reservation, Tour tour, User guest, int guestNumber)
        {
            service.Save(reservation, tour, guest, guestNumber);
        }

        public int CountGuestsBy(Tour tour)
        {
            return service.CountGuestsBy(tour);
        }

    }
}
