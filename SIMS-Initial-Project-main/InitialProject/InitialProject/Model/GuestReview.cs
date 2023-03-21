using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    [Table("GuestReview")]
    public class GuestReview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //For cistoca
        [Range(1, 5)]
        public int Tidiness { get; set; }

        //For postovanje pravila
        [Range(1, 5)]
        public int Obedience { get; set; }

        public String Comment { get; set; }

        //changes
        [ForeignKey("ReservationId")]
        public AccommodationReservation? Reservation { get; set; }

        public GuestReview(int tidiness, int obedience, string comment) {
            this.Tidiness = tidiness;
            this.Obedience = obedience;
            this.Comment = comment;
        }

        public static explicit operator GuestReview(List<AccommodationReservation?> v)
        {
            throw new NotImplementedException();
        }
    }
}
