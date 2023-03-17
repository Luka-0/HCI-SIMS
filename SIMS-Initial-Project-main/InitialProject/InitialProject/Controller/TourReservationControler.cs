using InitialProject.Dto;
using InitialProject.Model;
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
        private static TourReservationService service;
        public static TourReservationResponseDto Reserve(Tour tour, int guestNumber)
        {
            TourReservationResponseDto responseDto;

            if(service.GetAvaliableSpace(tour) < guestNumber)
                responseDto = new TourReservationResponseDto(true, 0);
            else
                responseDto = new TourReservationResponseDto(false, service.GetAvaliableSpace(tour));

            return responseDto;
        }
    }
}
