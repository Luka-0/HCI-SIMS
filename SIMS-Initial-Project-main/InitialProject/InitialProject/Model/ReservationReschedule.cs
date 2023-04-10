using InitialProject.Enumeration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    [Table("ReservationRescheduling")]
    public class ReservationReschedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public RequestState State { get; set; }

        public string Comment { get; set; }

        [ForeignKey("AccommodationReservationId")]
        public AccommodationReservation Reservation { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime NewStartingDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime NewEndingDate {  get; set; }

        public ReservationReschedule() { }

        public ReservationReschedule(RequestState state, AccommodationReservation reservation, DateTime newStartingDate, DateTime newEndingDate)
        {
            State = state;
            Reservation = reservation;
            NewStartingDate = newStartingDate;
            NewEndingDate = newEndingDate;
            Comment = new("");
        }

        public override string ToString()
        {
            return State.ToString() + " " + Comment;
        }

    }
}
