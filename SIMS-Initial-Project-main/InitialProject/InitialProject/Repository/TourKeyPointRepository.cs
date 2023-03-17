using System;
using InitialProject.Contexts;
using InitialProject.Model;
using System.Collections.Generic;

namespace InitialProject.Repository;

public class TourKeyPointRepository
{
    public static List<TourKeyPoint> getAll()
    {
        List<TourKeyPoint> tourKeyPoints= new List<TourKeyPoint>();

        using (var db = new UserContext())
        {
            foreach (TourKeyPoint tourKeyPoint in db.tourKeyPoints)
            {
                tourKeyPoints.Add(tourKeyPoint);
            }
        }
        return tourKeyPoints;
    }

    public static List<TourKeyPoint> getBy(List<string> TourKeyPointNames) 
    {
        List<TourKeyPoint> allTourKeyPoints = getAll();
        List<TourKeyPoint> tourKeyPoints = new List<TourKeyPoint>();
        using var db = new UserContext();

        foreach (TourKeyPoint tourKeyPoint in allTourKeyPoints)
        {
            foreach (string name in TourKeyPointNames)
            {
                if (tourKeyPoint.Name.Equals(name))
                {
                    tourKeyPoints.Add(tourKeyPoint);
                }
            }
        }
        return tourKeyPoints;

    }

    public static List<TourKeyPoint> save(List<string> tourKeyPointNames)
    {
        List<TourKeyPoint> tourKeyPoints = new List<TourKeyPoint>();
        using var db = new UserContext();

        foreach (String name in tourKeyPointNames)
        {
            TourKeyPoint tourKeyPoint = new TourKeyPoint(name);
            db.tourKeyPoints.Add(tourKeyPoint);
            tourKeyPoints.Add(tourKeyPoint);
        }
        return tourKeyPoints;
    }

    
}