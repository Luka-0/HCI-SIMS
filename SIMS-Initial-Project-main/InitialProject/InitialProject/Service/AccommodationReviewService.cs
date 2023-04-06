using InitialProject.Interface;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    internal class AccommodationReviewService
    {
        private readonly IAccommodationReviewRepository IAccommodationReviewRepository;

        public AccommodationReviewService(IAccommodationReviewRepository iAccommodationReviewRepository)
        {
            IAccommodationReviewRepository = iAccommodationReviewRepository;
        }

        public void Save(AccommodationReview accommodationReview)
        {
            IAccommodationReviewRepository.Save(accommodationReview);
        }

        public List<AccommodationReview> GetBy(User user)
        {
            return IAccommodationReviewRepository.GetBy(user);
        }

    }
}
