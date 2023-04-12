using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InitialProject.Model
{
    [Table("TourReservation")]
    public class TourReservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("TourId")]
        public Tour? Tour { get; set; }

        [Required]
        public int GuestNumber { get; set; }    //for how many guests is booked

        public TourReservation(){}
        [Required]
        public User? BookingGuest { get; set; }

        [ForeignKey("TourKeyPointId")]
        public TourKeyPoint? ArrivalPoint { get; set; }
        [ForeignKey("VoucherId")]
        public Voucher? Voucher { get; set; }

        public TourReservation(Tour tour, int guestNumber, User bookingGuest)
        {
            Tour = tour;
            GuestNumber = guestNumber;
            BookingGuest = bookingGuest;
            ArrivalPoint = null;
        }

        public TourReservation(Voucher voucher)
        {
            this.Voucher = voucher;
        }

        public override string ToString()
        {
            return Id.ToString() + " " + Tour.ToString() + " " + GuestNumber.ToString() + "\n";
        }
       
    }
}
