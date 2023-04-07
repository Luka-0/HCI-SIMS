using InitialProject.Dto;
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
    internal class TourKeyPointController
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

        public List<TourAndKeyPointsDto> GetByGuestAndActiveTour(User user)
        {
            return tourKeyPointService.GetByGuestAndActiveTour(user);
        }



    }
}
