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
    }
}
