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
    internal class ReservationRescheduleService
    {
        private readonly IReservationRescheduleRepository IReservationRescheduleRepository;

        public ReservationRescheduleService(IReservationRescheduleRepository iReservationRescheduleRepository)
        {
            IReservationRescheduleRepository = iReservationRescheduleRepository;
        }

        public void Save(ReservationReschedule reservationReschedule)
        {
            IReservationRescheduleRepository.Save(reservationReschedule);
        }

    }
}
