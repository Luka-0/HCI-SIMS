using System;
using InitialProject.Contexts;
using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace InitialProject.Service;

public class TourService
{
    TourRepository tourRepository = new TourRepository();
    public Tour save(Tour tour)
    {
        
           tour =  tourRepository.save(tour);
            return tour;

    }

    public int getBy(int id)
    {
        var db = new UserContext();
        var tempRecord = db.tours.Find(id);   //Try creating method in Accommodation repository to return the same thing
        return tempRecord.Id;
    }
}