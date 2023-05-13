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

        public bool Delete(Renovation renovation) {

            if (renovation.Start > DateTime.UtcNow.Add(new TimeSpan(5,0,0,0)))
            {
                //renoviranje se moze otkazati
                this.IRenovationRepository.Delete(renovation);

                return true;
            }
            //preostalo je manje od 5 dana do pocetka renoviranja
            return false;
        }

        public List<Renovation> GetAllBetweenBy(Accommodation accommodation, DateTime startingDate, DateTime endingDate) {

            return this.IRenovationRepository.GetAllBetweenBy(accommodation, startingDate, endingDate);
        }

        public List<Renovation> GetAllBy(string ownerUsername) {

            return this.IRenovationRepository.GetAllBy(ownerUsername);
        }

    }
}
