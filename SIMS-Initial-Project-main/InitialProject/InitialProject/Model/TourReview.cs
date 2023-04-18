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
    [Table("TourReview")]
    public class TourReview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int GuideKnowledge { get; set; }
        public int GuideLanguage { get; set; }
        public int AmusementScore { get; set; }
        public string Images { get; set; }

        [ForeignKey("TourReservationId")]
        public TourReservation Reservation { get; set; }

        public bool Valid { get; set; }

        [AllowNull]
        public String Comment { get; set; }

        //TODO jedno polje za KeyPointove

        public TourReview() { }
        public TourReview(int id, int guideKnowledge, int guideLanguage, int amusementScore, string images, TourReservation reservation)
        {
            Id = id;
            GuideKnowledge = guideKnowledge;
            GuideLanguage = guideLanguage;
            AmusementScore = amusementScore;
            Images = images;
            Reservation = reservation;
            Valid = true;
        }
        public TourReview(int id, int guideKnowledge, int guideLanguage, int amusementScore, string images, TourReservation reservation, String comment)
        {
            Id = id;
            GuideKnowledge = guideKnowledge;
            GuideLanguage = guideLanguage;
            AmusementScore = amusementScore;
            Images = images;
            Reservation = reservation;
            Valid = true;
            this.Comment = comment;
        }

        public override string ToString()
        {
            return GuideKnowledge.ToString() + " " + GuideLanguage.ToString() + " " + AmusementScore.ToString();
        }
        

    }
}
