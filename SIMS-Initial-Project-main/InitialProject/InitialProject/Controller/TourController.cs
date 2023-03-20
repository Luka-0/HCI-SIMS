﻿using System;
using System.Collections.Generic;
using InitialProject.Contexts;
using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace InitialProject.Controller;

public class TourController
{
    private TourKeyPointService tourKeyPointService = new TourKeyPointService();

    private TourService tourService = new TourService();

    private LocationService locationService = new LocationService();
    private ImageService imageService = new ImageService();
    


    public void add(TourToControllerDto dto)
    {
        Location location = locationService.getBy(dto.Country, dto.City);
        List<TourKeyPoint> tourKeyPoints = tourKeyPointService.save(dto.TourKeyPointNames);
        List<Image> images = imageService.save(dto.ImageURLs);

        Tour tour = new Tour(dto.Name, dto.Description, dto.Language, 
            dto.GuestLimit, dto.StartDateAndTime, 
            dto.Duration);

        var db = new UserContext();
        tour = tourService.save(tour);

        //int tourId = tourService.get(tour.Id);
        tourKeyPointService.update(tourKeyPoints, tour);
       // tourKeyPointService.setTypes(tourKeyPoints);
        tour.Location = locationService.getBy(dto.Country, dto.City);

        imageService.setTourId(images, tour);

        db.SaveChanges();
        
    }

    public List<Tour> getAll()
    {
        return tourService.getAll();
    }

    public List<TourBasicInfoDto> getAllBasicInfo()
    {
        return tourService.getBasicInfo();
    }
    public List<TourBasicInfoDto> GetTodaysToursBasicInfo()
    {
        return tourService.getTodaysToursBasicInfo();
    }

}