using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    [Table("RenovationSuggestion")]
    public class RenovationSuggestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Range(1, 5)]
        public int RenovationRating { get; set; }

        public string Comment { get; set; } = new("");

        [ForeignKey("ReservationId")]
        public AccommodationReservation AccommodationReservation { get; set; }

        public RenovationSuggestion()
        {
            AccommodationReservation = new();
        }

        public RenovationSuggestion(int renovationRating, string comment, AccommodationReservation accommodationReservation)
        {
            RenovationRating = renovationRating;
            Comment = comment;
            AccommodationReservation = accommodationReservation;
        }
    }
}
