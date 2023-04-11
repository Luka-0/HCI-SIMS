using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    [Table("AccommodationReservation")]
    public class AccommodationReservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("AccommodationId")]
        public Accommodation Accommodation { get; set; }

        [ForeignKey("GuestId")]
        public User Guest { get; set; }

        [Required]
        public int GuestNumber { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BegginingDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndingDate { get; set; }
        public bool Cancelled { get; set; }

        public AccommodationReservation() {
            Cancelled = false;
           // this.Accommodation = new Accommodation();
           // this.Guest = new User();
        }

        
    }
}
