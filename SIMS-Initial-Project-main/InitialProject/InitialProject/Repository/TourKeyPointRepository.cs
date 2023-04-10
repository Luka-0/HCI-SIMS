using System;
using InitialProject.Contexts;
using InitialProject.Model;
using System.Collections.Generic;
using System.Linq;
using InitialProject.Interface;
using Microsoft.EntityFrameworkCore;

namespace InitialProject.Repository;

public class TourKeyPointRepository:ITourKeyPointRepository
{
    public List<TourKeyPoint> GetAll()
    {
        List<TourKeyPoint> keyPoints = new List<TourKeyPoint>();

        using (var dbContext = new UserContext())
        {
            keyPoints = dbContext.tourKeyPoints.Include(t => t.Tour).ToList();
        }
        return keyPoints;
    }

    public List<TourKeyPoint> GetByTour(Tour tour)
    {
        List<TourKeyPoint> keyPoints = new List<TourKeyPoint>();

        using (var dbContext = new UserContext())
        {
            keyPoints = dbContext.tourKeyPoints.Include(t => t.Tour)
                                               .Where(t  => t.Tour.Id == tour.Id) 
                                               .ToList();
        }
        return keyPoints;
    }

    public List<TourKeyPoint> GetByTourKeyPointNames(List<string> TourKeyPointNames) 
    {
        using var db = new UserContext();
        List<TourKeyPoint> allTourKeyPoints = db.tourKeyPoints.ToList();
        List<TourKeyPoint> tourKeyPoints = new List<TourKeyPoint>();
        

        foreach (TourKeyPoint keyPoint in allTourKeyPoints)
        {
            tourKeyPoints.Add(allTourKeyPoints.Find(i => i.Name == keyPoint.Name));
        }
        
        return tourKeyPoints;

    }

    public void Save(TourKeyPoint tourKeyPoint, TourKeyPointType type)
    {
        tourKeyPoint.Type = type;
        using var db = new UserContext();

        db.Add(tourKeyPoint);
        db.SaveChanges();
    }

    public void Update(TourKeyPoint tourKeyPoint, Tour tour)
    {
        using (var db =new UserContext())
        {
            var tempRecord = db.tourKeyPoints.Find(tourKeyPoint.Id);   //Try creating method in Accommodation repository to return the same thing
            tempRecord.Tour = tour;

            db.SaveChanges();
        }
    }

    public TourKeyPoint GetById(int id)
    {
        using (var db = new UserContext())
        {
            return db.tourKeyPoints.Find(id);
        }
    }

    public void SetType(int  startId, int endId)
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

    public void StartTour(int id)
    {
        using (var db = new UserContext()){
            var tempRecord = db.tourKeyPoints
                .FirstOrDefault(t => t.Tour.Id == id && t.Type == TourKeyPointType.Start);
            tempRecord.Reached = true;
            db.SaveChanges();
        }
    }

    public void Reach(int id)
    {
        using (var db = new UserContext())
        {
            var tempRecord = db.tourKeyPoints.Find(id);
            tempRecord.Reached = true;
            db.SaveChanges();
        }
        
    }

    public List<TourKeyPoint> GetAllByTour(int id)
    {
        using (var db = new UserContext())
        {
            return db.tourKeyPoints.Where(t=>t.Tour.Id==id).ToList();
        }
        
    }


}