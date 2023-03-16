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
        public static TourReservationResponseDto Reserve(Tour tour, int guestNumber)
        {
            TourReservationResponseDto responseDto;

            if(TourReservationService.GetAvaliableSpace(tour) < guestNumber)
                responseDto = new TourReservationResponseDto(true, 0);
            else
                responseDto = new TourReservationResponseDto(false, TourReservationService.GetAvaliableSpace(tour));

            return responseDto;
        }
    }
}
