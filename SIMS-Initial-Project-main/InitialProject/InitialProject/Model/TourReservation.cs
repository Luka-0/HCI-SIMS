using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class TourReservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("TourId")]
        public Tour Tour { get; set; }

        [Required]
        public int GuestNumber { get; set; }    //for how many guests is booked

        public TourReservation(){}
        [Required]
        public User BookingGuest { get; set; }


    }
}
