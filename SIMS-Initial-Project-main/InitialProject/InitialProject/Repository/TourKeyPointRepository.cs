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
            db.SaveChanges();
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

    /*public static List<TourKeyPoint> save(List<string> tourKeyPointNames)
    {
        List<TourKeyPoint> tourKeyPoints = new List<TourKeyPoint>();
        using var db = new UserContext();

        foreach (String name in tourKeyPointNames)
        {
            TourKeyPoint tourKeyPoint = new TourKeyPoint(name);
            db.tourKeyPoints.Add(tourKeyPoint);
            tourKeyPoints.Add(tourKeyPoint);

        }

        db.SaveChanges();
        return tourKeyPoints;
    }*/
    public static void Save(TourKeyPoint tourKeyPoint, TourKeyPointType type)
    {
        tourKeyPoint.Type = type;
        using var db = new UserContext();

        db.Add(tourKeyPoint);
        db.SaveChanges();
    }

    public static void update(TourKeyPoint tourKeyPoint, Tour tour)
    {
        using (var db =new UserContext())
        {
            var tempRecord = db.tourKeyPoints.Find(tourKeyPoint.Id);   //Try creating method in Accommodation repository to return the same thing
            tempRecord.Tour = tour;

            db.SaveChanges();
        }
    }

    public static TourKeyPoint getBy(int id)
    {
        using (var db = new UserContext())
        {
            return db.tourKeyPoints.Find(id);
        }
    }

    public static void setType(int  startId, int endId)
    {
        using (var db = new UserContext())
        {
            var tempRecord = db.tourKeyPoints.Find(startId);
            tempRecord.Type = TourKeyPointType.Start;
            var tempRecord2 = db.tourKeyPoints.Find(endId);
            tempRecord2.Type = TourKeyPointType.End;

            db.SaveChanges();
        }
    }

    
}