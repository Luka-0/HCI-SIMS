using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    public interface IGuestReviewRepository
    {
        public void Save(GuestReview guestReview);
        public List<AccommodationReservation> GetGradedReservations(User owner);
        public List<GuestReview> GetAll(User user);
        public List<GuestReview> GetByUser(User user);

    }
}
