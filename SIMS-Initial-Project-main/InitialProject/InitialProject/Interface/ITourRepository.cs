using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Enumeration;

namespace InitialProject.Interface
{
    public interface ITourRepository
    {
        public Tour Save(Tour tour);
        public List<Tour> GetAll();
        public Tour GetById(int id);
        public List<Tour> GetByLocation(Location location);
        public List<Tour> GetByDuration(TimeSpan duration);
        public List<Tour> GetByLanguage(string language);
        public List<Tour> GetByGuestLimit(int guestLimit);
        public void SetStatus(int id, TourStatus status);

        public void Delete(int id);
        List<Tour> GetAllByGuide(int id);
    }
}
