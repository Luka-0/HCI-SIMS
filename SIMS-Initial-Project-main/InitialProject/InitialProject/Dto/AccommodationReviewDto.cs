using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    public class AccommodationReviewDto
    {
        public string Guest { get; set; }
        public string Accommodation { get; set; }
        public int Tidiness { get; set; }
        public int Correctness { get; set; }
        public string Comment { get; set; }
        public string Images { get; set; }

        public AccommodationReviewDto(AccommodationReview accommodationReview) {

            this.Guest = accommodationReview.Reservation.Guest.Username;
            this.Accommodation = accommodationReview.Reservation.Accommodation.Title;
            this.Tidiness = accommodationReview.Tidiness;
            this.Comment= accommodationReview.Comment;
            this.Correctness = accommodationReview.Correctness;
            this.Images = accommodationReview.Images;
        
        }


    }
}
