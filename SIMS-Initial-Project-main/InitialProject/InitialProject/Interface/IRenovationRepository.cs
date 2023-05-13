using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    public interface IRenovationRepository
    {
        public void Save(Renovation renovation);
        public List<Renovation> GetAllBetweenBy(Accommodation accommodation, DateTime startingDate, DateTime endingDate);
    }
}
