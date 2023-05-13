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
    public class RenovationService
    {
        private readonly IRenovationRepository IRenovationRepository;
        private UserService UserService;
        private AccommodationService AccommodationService;

        public RenovationService(IRenovationRepository iRenovationRepository)
        {
            this.IRenovationRepository = iRenovationRepository;
            this.UserService = new(new UserRepository());
            this.AccommodationService = new(new AccommodationRepository());
        }

        public void Save(Renovation renovation) {

            AccommodationService.UpdateLastRenovatedBy(renovation.Accommodation, renovation.End);

            this.IRenovationRepository.Save(renovation);

        }

        public List<Renovation> GetAllBetweenBy(Accommodation accommodation, DateTime startingDate, DateTime endingDate) {

            return this.IRenovationRepository.GetAllBetweenBy(accommodation, startingDate, endingDate);
        }

    }
}
