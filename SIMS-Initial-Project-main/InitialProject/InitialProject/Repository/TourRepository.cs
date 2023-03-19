using InitialProject.Contexts;
using InitialProject.Model;
using System;
using System.Windows.Navigation;

namespace InitialProject.Repository;

public class TourRepository
{
    public Tour save(Tour tour)
    {
        using var db = new UserContext();
        db.Add(tour);
        db.SaveChanges();
        
        return db.tours.Find(tour.Id);
        //Try creating method in Accommodation repository to return the same thing

    }
}