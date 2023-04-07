using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    public class TourAndKeyPointsDto
    {
        public Tour tour { get; set; }
        public List<TourKeyPoint> keyPoints { get; set; }

        public TourAndKeyPointsDto() { }

        public TourAndKeyPointsDto(Tour tour, List<TourKeyPoint> keyPoints)
        {
            this.tour = tour;
            this.keyPoints = keyPoints;
        }
    }
}
