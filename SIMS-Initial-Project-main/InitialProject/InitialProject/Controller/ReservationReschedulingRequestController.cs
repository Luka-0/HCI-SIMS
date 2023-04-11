using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Interface;
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
    public class ReservationReschedulingRequestController
    {
        private readonly ReservationReschedulingRequestService reservationReschedulingRequestService = new(new ReservationReschedulingRequestRepository());

        public void Save(ReservationReschedulingRequest reservationReschedulingRequest)
        {
            this.reservationReschedulingRequestService.Save(reservationReschedulingRequest);
        }

        public List<ReservationReschedulingRequestDto> GetAllBy(string ownerUsername) {

            List<ReservationReschedulingRequestDto> reservationReschedulingRequests = new List<ReservationReschedulingRequestDto>();

            foreach (var request in reservationReschedulingRequestService.GetAllBy(ownerUsername)) {

                reservationReschedulingRequests.Add(new ReservationReschedulingRequestDto(request));
            }
            return reservationReschedulingRequests;
        }

        public void Respond(ReservationReschedulingResponseDto reservationReschedulingResponseDto) {
        
            string comment = reservationReschedulingResponseDto.Comment;
            RequestState requestState = reservationReschedulingResponseDto.State;
            int existingRequestId = reservationReschedulingResponseDto.RequestId;

            this.reservationReschedulingRequestService.DetermineResponse(existingRequestId, comment, requestState);
        }

        public void UpdateWasNotifiedBy(int id, bool wasNotified)
        {
            reservationReschedulingRequestService.UpdateWasNotifiedBy(id, wasNotified);
        }


        public List<ReservationReschedulingRequest> GetAllByUser(int id)
        {
            return reservationReschedulingRequestService.GetAllByUser(id);
        }
    }
}
