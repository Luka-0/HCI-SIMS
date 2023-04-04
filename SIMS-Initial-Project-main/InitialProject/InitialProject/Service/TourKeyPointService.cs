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

    public static List<TourKeyPoint> GetBy(List<string> names)
    {

        return TourKeyPointRepository.GetBy(names);
    }

    public TourKeyPointType GetTypesByIndex(List<String> names, String currentName)
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
    

    public  List<TourKeyPoint> Save(List<string> keyPointNames)
    {
        List<TourKeyPoint> tourKeyPoints= new List<TourKeyPoint>();
        foreach (string name in keyPointNames)
        {

            TourKeyPoint tourKeyPoint= new TourKeyPoint(name);
            TourKeyPointType type = GetTypesByIndex(keyPointNames, name);
            TourKeyPointRepository.Save(tourKeyPoint, type);
            TourKeyPoint keyPointFromDb = TourKeyPointRepository.GetBy(tourKeyPoint.Id);
            tourKeyPoints.Add(keyPointFromDb);

        }

        return tourKeyPoints;
    }

    public void Update(List<TourKeyPoint> tourKeyPoints, Tour tour)
    {
        {
            foreach (var keyPoint in tourKeyPoints)
            {
                TourKeyPointRepository.Update(keyPoint,tour);
            }
        }
    }

    public void SetTypes(List<TourKeyPoint> tourKeyPoints)
    {
        TourKeyPoint start = tourKeyPoints.First();
        TourKeyPoint end = tourKeyPoints.Last();
        TourKeyPointRepository.SetType(start.Id, end.Id);

    }

}