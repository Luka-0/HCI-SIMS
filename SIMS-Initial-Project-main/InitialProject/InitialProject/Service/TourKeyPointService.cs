using System.Collections.Generic;
using InitialProject.Contexts;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service;

public class TourKeyPointService
{   

    public static List<TourKeyPoint> getAll()
    {
        return TourKeyPointRepository.getAll();
    }
    public static List<TourKeyPoint> getBy(List<string> names)
    {

        return TourKeyPointRepository.getBy(names);
    }

    public  List<TourKeyPoint> save(List<string> keyPointNames)
    {
        List<TourKeyPoint> tourKeyPoints= new List<TourKeyPoint>();
        foreach (string name in keyPointNames)
        {

            TourKeyPoint tourKeyPoint= new TourKeyPoint(name);
            TourKeyPointRepository.Save(tourKeyPoint);
            TourKeyPoint keyPointFromDb = TourKeyPointRepository.getBy(tourKeyPoint.Id);
            tourKeyPoints.Add(keyPointFromDb);

        }

        return tourKeyPoints;
    }

    public void update(List<TourKeyPoint> tourKeyPoints, Tour tour)
    {
        {
            foreach (var KeyPoint in tourKeyPoints)
            {
                TourKeyPointRepository.update(KeyPoint,tour);
            }
        }
    }
}