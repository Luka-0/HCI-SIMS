using System;
using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace InitialProject.Service;

public class TourService
{
    TourRepository tourRepository = new TourRepository();
    public void save(Tour tour)
    { 
        tourRepository.save(tour);
    }
}