using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    [Table("AccommodationReview")]
    public class AccommodationReview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Range(1, 5)]
        public int Tidiness { get; set; }

        [Range(1, 5)]
        public int Correctness { get; set; }

        public string Comment { get; set; }
        public string Images { get; set; }

        [ForeignKey("AccommodationReservationId")]
        public AccommodationReservation Reservation { get; set; }

        public override string ToString()
        {
            return Tidiness.ToString() + " " + Correctness.ToString() + " " + Comment;
        }

    }
}
