using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public  class AccommodationReviewService
    {
        private readonly IAccommodationReviewRepository IAccommodationReviewRepository;
        private AccommodationReservationService AccommodationReservationService;

        public AccommodationReviewService(IAccommodationReviewRepository iaccommodationReviewRepository) {

            this.IAccommodationReviewRepository = iaccommodationReviewRepository;
            this.AccommodationReservationService = new(new AccommodationReservationRepository());
        }

        public List<AccommodationReview> GetAllBy(string ownerUsername)
        {
            //napravi preko servisa
            User owner = UserRepository.Get(ownerUsername);

            return this.IAccommodationReviewRepository.GetAllBy(owner);
        }
    }
}
