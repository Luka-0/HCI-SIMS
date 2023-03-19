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
    public class ExpiredReservationDto
    {
        public int ReservationId { get; set; }
        public DateTime BegginingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public int GuestId { get; set; }
        public string GuestUsername { get; set; }
        public int GuestNumber { get; set; }
        public int AccommodationId { get; set; }
        public string AccommodationTtile { get; set; }

        public ExpiredReservationDto(AccommodationReservation reservation) {

            this.AccommodationId = reservation.Accommodation.Id;
            this.AccommodationTtile = reservation.Accommodation.Title;
            this.GuestId = reservation.Guest.Id;
            this.GuestUsername = reservation.Guest.Username;
            this.GuestNumber = reservation.GuestNumber;
            this.BegginingDate = reservation.BegginingDate;
            this.EndingDate = reservation.EndingDate;
            this.ReservationId = reservation.Id;
        }

    }
}
