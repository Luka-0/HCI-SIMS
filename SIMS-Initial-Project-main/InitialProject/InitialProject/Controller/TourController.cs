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

    public List<GetTourDto> GetAll()
    {
        List<Tour> allTours = tourService.GetAll();
        List<GetTourDto> getTourDtos = new List<GetTourDto>();

        Location tourLocation = new Location();
        foreach (Tour tour in allTours)
        {
            getTourDtos.Add(new GetTourDto(tour.Name, tour.Description, tour.Location, tour.Language, tour.GuestLimit, tour.Duration, tour.StartDateAndTime, tour.TourKeyPoints, tour.images));
        }
        return getTourDtos;
    }



    public List<GetTourDto> GetBy(Location location)
    {
        List<Tour> allTours = tourService.GetBy(location);
        List<GetTourDto> getTourDtos = new List<GetTourDto>();

        foreach (Tour tour in allTours)
        {
            getTourDtos.Add(new GetTourDto(tour.Name, tour.Description, tour.Location, tour.Language, tour.GuestLimit, tour.Duration, tour.StartDateAndTime, tour.TourKeyPoints, tour.images));
        }
        return getTourDtos;
    }

    public List<GetTourDto> GetBy(TimeSpan duration)
    {
        List<Tour> allTours = tourService.GetBy(duration);
        List<GetTourDto> getTourDtos = new List<GetTourDto>();

        foreach (Tour tour in allTours)
        {
            getTourDtos.Add(new GetTourDto(tour.Name, tour.Description, tour.Location, tour.Language, tour.GuestLimit, tour.Duration, tour.StartDateAndTime, tour.TourKeyPoints, tour.images));
        }
        return getTourDtos;
    }

    public List<GetTourDto> GetBy(string language)
    {
        List<Tour> allTours = tourService.GetBy(language);
        List<GetTourDto> getTourDtos = new List<GetTourDto>();

        foreach (Tour tour in allTours)
        {
            getTourDtos.Add(new GetTourDto(tour.Name, tour.Description, tour.Location, tour.Language, tour.GuestLimit, tour.Duration, tour.StartDateAndTime, tour.TourKeyPoints, tour.images));
        }
        return getTourDtos;
    }

    public List<GetTourDto> GetBy(int guestNumber)
    {
        List<Tour> allTours = tourService.GetAll();
        List<GetTourDto> getTourDtos = new List<GetTourDto>();

        foreach (Tour tour in allTours)
        {
            if (reservationService.IsReserved(tour))
                if (reservationService.CountGuestsBy(tour) == guestNumber)
                    getTourDtos.Add(new GetTourDto(tour.Name, tour.Description, tour.Location, tour.Language, tour.GuestLimit, tour.Duration, tour.StartDateAndTime, tour.TourKeyPoints, tour.images));
        }
        return getTourDtos;
    }



    public Tour Reserve(Tour tour, int guestNumber)
    {
        Tour chosenTour = tourService.GetById(tour.Id);

        //TODO: ispravi ovo
        return null;
    }

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

