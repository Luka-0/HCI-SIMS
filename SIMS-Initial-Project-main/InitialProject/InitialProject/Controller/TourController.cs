using System;
using System.Collections.Generic;
using InitialProject.Dto;
using InitialProject.Model;
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
            dto.GuestLimit, tourKeyPoints, dto.StartDateAndTime, 
            dto.Duration, images);

        tourService.save(tour);
    }

}