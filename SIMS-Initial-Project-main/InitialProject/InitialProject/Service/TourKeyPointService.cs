using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Windows;
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

    public TourKeyPointType getTypesByIndex(List<String> names, String currentName)
    {
        String first= names.First();
        String last = names.Last();

        if (currentName.Equals(first))
        {
            return TourKeyPointType.Start;
        }

        if (currentName.Equals(last))
        {
            return TourKeyPointType.End;
        }
        return TourKeyPointType.Mid;
    }
    

    public  List<TourKeyPoint> save(List<string> keyPointNames)
    {
        List<TourKeyPoint> tourKeyPoints= new List<TourKeyPoint>();
        foreach (string name in keyPointNames)
        {

            TourKeyPoint tourKeyPoint= new TourKeyPoint(name);
            TourKeyPointType type = getTypesByIndex(keyPointNames, name);
            TourKeyPointRepository.Save(tourKeyPoint, type);
            TourKeyPoint keyPointFromDb = TourKeyPointRepository.getBy(tourKeyPoint.Id);
            tourKeyPoints.Add(keyPointFromDb);

        }

        return tourKeyPoints;
    }

    public void update(List<TourKeyPoint> tourKeyPoints, Tour tour)
    {
        {
            foreach (var keyPoint in tourKeyPoints)
            {
                TourKeyPointRepository.update(keyPoint,tour);
            }
        }
    }

    public void setTypes(List<TourKeyPoint> tourKeyPoints)
    {
        TourKeyPoint start = tourKeyPoints.First();
        TourKeyPoint end = tourKeyPoints.Last();
        TourKeyPointRepository.setType(start.Id, end.Id);

    }

}