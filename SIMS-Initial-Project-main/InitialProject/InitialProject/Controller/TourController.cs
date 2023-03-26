using System;
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
    private TourReservationService reservationService = new TourReservationService();
    private TourService tourService = new TourService();

    //private LocationService locationService = new LocationService();
    private ImageService imageService = new ImageService();

    public List<Tour> GetAll()
    {
        return tourService.GetAll();
    }

    public List<Tour> GetByLocation(Location location)
    {
        return tourService.GetByLocation(location);
    }

    public List<Tour> GetByDuration(TimeSpan duration)
    {
        return tourService.GetByDuration(duration);
    }

    public List<Tour> GetByLanguage(string language)
    {
        return tourService.GetByLanguage(language);
    }

    public List<Tour> GetByGuestLimit(int guestLimit)
    {
        return tourService.GetByGuestLimit(guestLimit); 
    }

    public List<GetTourDto> GetBy(int guestNumber)
    {
        List<Tour> allTours = tourService.GetAll();
        List<GetTourDto> getTourDtos = new List<GetTourDto>();

        foreach (Tour tour in allTours)
        {
            if (reservationService.IsReserved(tour))
                if (reservationService.CountGuestsOnTour(tour) == guestNumber)
                    getTourDtos.Add(new GetTourDto(tour.Name, tour.Description, tour.Location, tour.Language, tour.GuestLimit, tour.Duration, tour.StartDateAndTime, tour.TourKeyPoints, tour.images));
        }
        return getTourDtos;
    }

    //TODO Krstic ispravlja
    public void Add(TourToControllerDto dto)
    {
        Location location = LocationService.GetBy(dto.Country, dto.City);
        List<TourKeyPoint> tourKeyPoints = tourKeyPointService.Save(dto.TourKeyPointNames);
        List<Image> images = imageService.Save(dto.ImageURLs);

        Tour tour = new Tour(dto.Name, dto.Description, dto.Language, 
            dto.GuestLimit, dto.StartDateAndTime, 
            dto.Duration);

        var db = new UserContext();
        tour = tourService.Save(tour);

        //int tourId = tourService.get(tour.Id);
        tourKeyPointService.Update(tourKeyPoints, tour);
       // tourKeyPointService.SetTypes(tourKeyPoints);
        tour.Location = LocationService.GetBy(dto.Country, dto.City);

        imageService.SetTourId(images, tour);

        db.SaveChanges();
        
    }

    public List<TourBasicInfoDto> Get()
    {
        return tourService.Get();
    }
    public List<TourBasicInfoDto> GetTodays()
    {
        return tourService.GetTodays();
    }

}

