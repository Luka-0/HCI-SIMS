using InitialProject.Enumeration;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    public class ReservationReschedulingRequestDto
    {
        public int ReservationId { get; set; }
        public DateTime NewStartingDate { get; set; }
        public DateTime NewEndingDate { get; set; }
        public bool Achievable { get; set; }
        public string Comment { get; set; }

        public ReservationReschedulingRequestDto(ReservationReschedulingRequest reservationReschedulingRequest) {

            this.Comment = reservationReschedulingRequest.Comment;
            this.ReservationId = reservationReschedulingRequest.Reservation.Id;
            this.NewStartingDate = reservationReschedulingRequest.NewStartingDate;
            this.NewEndingDate = reservationReschedulingRequest.NewEndingDate;
        }
    }
}
