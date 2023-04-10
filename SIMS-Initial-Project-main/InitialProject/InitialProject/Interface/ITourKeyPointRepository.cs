using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    public interface ITourKeyPointRepository
    {
        public List<TourKeyPoint> GetByTourKeyPointNames(List<string> TourKeyPointNames);
        public void Save(TourKeyPoint tourKeyPoint, TourKeyPointType type);
        public void Update(TourKeyPoint tourKeyPoint, Tour tour);
        public TourKeyPoint GetById(int id);
        public void SetType(int startId, int endId);
        public List<TourKeyPoint> GetAll();
        public List<TourKeyPoint> GetByTour(Tour tour);
        public void StartTour(int id);
        public void Reach(int id);

        public List<TourKeyPoint> GetAllByTour(int id);
    }
}
