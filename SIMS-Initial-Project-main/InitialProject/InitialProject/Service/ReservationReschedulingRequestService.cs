using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    internal class ReservationReschedulingRequestService
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

                existingReservations = AccommodationReservationService.GetAllBetween(request.NewStartingDate, request.NewEndingDate, ownerUsername);

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
     }
}
