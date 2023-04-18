using InitialProject.Enumeration;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    public interface IReservationReschedulingRequestRepository
    {
        public void Save(ReservationReschedulingRequest reservationReschedulingRequest);
        public List<ReservationReschedulingRequest> GetAllBy(User owner);
        public void UpdateCommentBy(int id, string comment);
        public void UpdateStateBy(int id, RequestState requestState);
        public void UpdateWasNotifiedBy(int id, bool wasNotified);
        public ReservationReschedulingRequest GetBy(int id);
        public List<ReservationReschedulingRequest> GetAllByUser(int id);

    }
}
