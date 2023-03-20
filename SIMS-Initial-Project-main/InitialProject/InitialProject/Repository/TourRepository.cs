using InitialProject.Contexts;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public List<Tour> getAll()
    {
        using var db = new UserContext();
        return db.tours.ToList();
    }

    public Location getLocationByTourId(int id)
    {
        Location location = new Location();
        using (var db = new UserContext())
        {
            location = db.tours.Where(a => a.Id == id).Select(a => a.Location).First();
            return location;
        }
    }
}