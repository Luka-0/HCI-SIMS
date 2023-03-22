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

            if(service.GetAvaliableSpace(tour) < guestNumber)
                responseDto = new TourReservationResponseDto(true, service.GetAvaliableSpace(tour));
            else
                responseDto = new TourReservationResponseDto(false, service.GetAvaliableSpace(tour));

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

    }
}
