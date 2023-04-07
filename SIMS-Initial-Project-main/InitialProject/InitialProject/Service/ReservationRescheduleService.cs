using InitialProject.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    class ReservationRescheduleService
    {
        private readonly IReservationRescheduleRepository IReservationRescheduleRepository;

        public ReservationRescheduleService(IReservationRescheduleRepository iReservationRescheduleRepository)
        {
            IReservationRescheduleRepository = iReservationRescheduleRepository;
        }


    }
}
