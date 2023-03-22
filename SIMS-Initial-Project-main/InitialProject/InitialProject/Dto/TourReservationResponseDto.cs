using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    public class TourReservationResponseDto
    {
        public bool IsFull { get; set; }
        public int AvaliableSpace { get; set; }

        public TourReservationResponseDto(bool isFull, int avaliableSpace)
        {
            IsFull = isFull;
            AvaliableSpace = avaliableSpace;
        }
        public TourReservationResponseDto() { }

        public override string ToString()
        {
            return IsFull + " " + AvaliableSpace;
        }
    }
}
