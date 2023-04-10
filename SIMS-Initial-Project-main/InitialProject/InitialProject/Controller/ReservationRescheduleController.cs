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
    public class ReservationRescheduleController
    {
        private readonly ReservationRescheduleService ReservationRescheduleService = new(new ReservationRescheduleRepository());

        public void Save(ReservationReschedule reservationReschedule)
        {
            ReservationRescheduleService.Save(reservationReschedule);
        }
    }
}
