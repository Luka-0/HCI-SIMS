using System;
using System.CodeDom;
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

        public AccommodationReview() { }

        public AccommodationReview(int tidiness, int correctness, string comment, string images, AccommodationReservation reservation)
        {
            Tidiness = tidiness;
            Correctness = correctness;
            Comment = comment;
            Images = images;
            Reservation = reservation;
        }

        public override string ToString()
        {
            return Tidiness.ToString() + " " + Correctness.ToString() + " " + Comment;
        }

    }
}
