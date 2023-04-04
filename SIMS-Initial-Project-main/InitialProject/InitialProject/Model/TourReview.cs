using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int InterestLevel { get; set; }
        public string Images { get; set; }

        [ForeignKey("TourReservationId")]
        public TourReservation Reservation { get; set; }

        //TODO jedno polje za KeyPointove

        public override string ToString()
        {
            return GuideKnowledge.ToString() + " " + GuideLanguage.ToString() + " " + InterestLevel.ToString();
        }
        

    }
}
