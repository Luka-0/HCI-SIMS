using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    internal interface IAccommodationReviewRepository
    {
        public void Save(AccommodationReview accommodationReview);
        public List<AccommodationReview> GetBy(User user);
    }
}
