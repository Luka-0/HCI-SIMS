using InitialProject.AuxiliaryClass;
using InitialProject.Contexts;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class RenovationController
    {
        private readonly RenovationService RenovationService = new(new RenovationRepository());

        public void Save(Accommodation accommodation, DateSuggestion chosenDate, string description, TimeSpan duration) {

            Renovation renovation = new Renovation(description, accommodation, duration, chosenDate.Start, chosenDate.End);

            this.RenovationService.Save(renovation);
        }

        public List<Renovation> GetAllBetweenBy(Accommodation accommodation, DateTime startingDate, DateTime endingDate)
        {

            return this.RenovationService.GetAllBetweenBy(accommodation, startingDate, endingDate);
        }
    }
}
