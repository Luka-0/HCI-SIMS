using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class TourKeyPointController
    {
        private TourKeyPointService tourKeyPointService = new TourKeyPointService(new TourKeyPointRepository());

        public TourKeyPoint GetById(int id)
        {
            return tourKeyPointService.GetById(id);
        }

        public List<TourKeyPoint> GetByTourKeyPointNames(List<string> names)
        {
            return tourKeyPointService.GetByTourKeyPointNames(names);
        }

        public TourKeyPointType GetTypesByIndex(List<String> names, String currentName)
        {
            return tourKeyPointService.GetTypesByIndex(names, currentName);
        }

        public List<TourKeyPoint> Save(List<string> keyPointNames)
        {
            return tourKeyPointService.Save(keyPointNames);
        }

        public void Save(TourKeyPoint tourKeyPoint, TourKeyPointType type)
        {
            tourKeyPointService.Save(tourKeyPoint, type);
        }

        public void Update(List<TourKeyPoint> tourKeyPoints, Tour tour)
        {
            tourKeyPointService.Update(tourKeyPoints, tour);
        }

        public void SetTypes(List<TourKeyPoint> tourKeyPoints)
        {
            tourKeyPointService.SetTypes(tourKeyPoints);
        }

        public List<TourKeyPoint> GetByGuestAndActiveTour(User user)
        {
            List<TourKeyPoint> keyPoints = new List<TourKeyPoint>();



            return keyPoints;
        }

        public void StartTour(int id)
        {

            tourKeyPointService.StartTour(id);
        }

        public void Reach(int id, TourKeyPointType type)
        {
            if (type != TourKeyPointType.Start)
            {
                tourKeyPointService.Reach(id);
            }

        }

        public List<TourKeyPoint> Create(List<string> tourKeyPointNames, Tour tour)
        {
            return tourKeyPointService.Create(tourKeyPointNames, tour);
        }
    }
}
