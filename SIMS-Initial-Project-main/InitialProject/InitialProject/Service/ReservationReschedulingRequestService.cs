using InitialProject.Interface;
using InitialProject.Model;
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

        public ReservationReschedulingRequestService(IReservationReschedulingRequestRepository IReservationReschedulingRequestRepository)
        {
            this.IReservationReschedulingRequestRepository = IReservationReschedulingRequestRepository;
        }

        public void Save(ReservationReschedulingRequest reservationReschedulingRequest)
        {
            IReservationReschedulingRequestRepository.Save(reservationReschedulingRequest);
        }

    }
}
