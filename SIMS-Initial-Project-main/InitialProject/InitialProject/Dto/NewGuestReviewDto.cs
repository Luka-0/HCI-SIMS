using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    public class NewGuestReviewDto
    {

        public int Tidiness { get; set; }
        public int Obedience { get; set; }
        public string Comment { get; set; }
        public int ReservationId { get; set; }

        public NewGuestReviewDto(int tidiness, int obedience, string comment, int reservationId) {

            this.Tidiness = tidiness;
            this.Obedience = obedience;
            this.Comment = comment;
            this.ReservationId = reservationId;
        
        }


       
    }
}
