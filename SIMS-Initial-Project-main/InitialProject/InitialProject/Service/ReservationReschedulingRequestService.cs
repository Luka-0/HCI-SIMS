using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    internal class ReservationReschedulingRequestService
    {
        private readonly IReservationReschedulingRequestRepository IReservationReschedulingRequestRepository;
        private UserService UserService;
        public ReservationReschedulingRequestService(IReservationReschedulingRequestRepository IReservationReschedulingRequestRepository)
        {
            this.IReservationReschedulingRequestRepository = IReservationReschedulingRequestRepository;
            this.UserService = new(new UserRepository());
        }

        public void Save(ReservationReschedulingRequest reservationReschedulingRequest)
        {
            IReservationReschedulingRequestRepository.Save(reservationReschedulingRequest);
        }

        public List<ReservationReschedulingRequest> GetAllBy(string ownerUsername) {

            User owner = UserService.GetBy(ownerUsername);

            return this.IReservationReschedulingRequestRepository.GetAllBy(owner);
        }
        

     }
}
