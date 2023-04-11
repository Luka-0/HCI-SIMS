using InitialProject.Enumeration;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using System.Security.AccessControl;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class ReservationReschedulingRequestService
    {
        private readonly IReservationReschedulingRequestRepository IReservationReschedulingRequestRepository;
        private UserService UserService;
        private AccommodationReservationService AccommodationReservationService;
        
        public ReservationReschedulingRequestService(IReservationReschedulingRequestRepository IReservationReschedulingRequestRepository)
        {
            this.IReservationReschedulingRequestRepository = IReservationReschedulingRequestRepository;
            this.UserService = new(new UserRepository());
            this.AccommodationReservationService = new(new AccommodationReservationRepository());
        }

        public void Save(ReservationReschedulingRequest reservationReschedulingRequest)
        {
            IReservationReschedulingRequestRepository.Save(reservationReschedulingRequest);
        }

        public List<ReservationReschedulingRequest> GetAllBy(string ownerUsername) {

            User owner = UserService.GetBy(ownerUsername);
            
            List<ReservationReschedulingRequest> reservationReschedulingRequests =  this.IReservationReschedulingRequestRepository.GetAllBy(owner);

            return DeclareAchievability(reservationReschedulingRequests, ownerUsername);
        }

        private List<ReservationReschedulingRequest> DeclareAchievability(List<ReservationReschedulingRequest> requests, string ownerUsername) {

            List<AccommodationReservation> existingReservations = new List<AccommodationReservation>();

            List<ReservationReschedulingRequest> processedRequests = new List<ReservationReschedulingRequest>();
            
  
            foreach (var request in requests) {

                existingReservations = AccommodationReservationService.GetAllPreserved(request, ownerUsername);

                if (existingReservations.Count() == 0)
                {
                    request.Achievable = true;
                }
                else {
                    request.Achievable = false;
                }

                processedRequests.Add(request);
            }

            return processedRequests;
        }


        public void DetermineResponse(int id, string comment, RequestState requestState){

            this.IReservationReschedulingRequestRepository.UpdateCommentBy(id, comment);
            this.IReservationReschedulingRequestRepository.UpdateStateBy(id, requestState);
            this.UpdateReservationDates(id);
        }

        private void UpdateReservationDates(int requestId){

            ReservationReschedulingRequest existingRequest = new ReservationReschedulingRequest();

            existingRequest = this.IReservationReschedulingRequestRepository.GetBy(requestId);

            int reservationId = existingRequest.Reservation.Id;

            if (existingRequest.State == RequestState.Approved){

                this.AccommodationReservationService.UpdateScheduledDatesBy(reservationId, existingRequest.NewStartingDate, existingRequest.NewEndingDate);
            }
        }

        public void UpdateWasNotifiedBy(int id, bool wasNotified)
        {
            IReservationReschedulingRequestRepository.UpdateWasNotifiedBy(id, wasNotified);
        }


        public List<ReservationReschedulingRequest> GetAllByUser(int id)
        {
            return IReservationReschedulingRequestRepository.GetAllByUser(id);
        }
    }
 }
    

