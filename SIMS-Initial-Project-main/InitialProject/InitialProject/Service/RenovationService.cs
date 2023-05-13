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

        public RenovationService(IRenovationRepository iRenovationRepository)
        {
            this.IRenovationRepository = iRenovationRepository;
        }

        public void Save(Renovation renovation) {

            this.IRenovationRepository.Save(renovation);

        }

        public List<Renovation> GetAllBetweenBy(Accommodation accommodation, DateTime startingDate, DateTime endingDate) {

            return this.IRenovationRepository.GetAllBetweenBy(accommodation, startingDate, endingDate);
        }

    }
}
