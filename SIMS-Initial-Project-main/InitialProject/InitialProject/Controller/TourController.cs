using System;
using System.Collections.Generic;
using System.Linq;
using InitialProject.Contexts;
using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Interface;
using InitialProject.Migrations;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace InitialProject.Controller;

public class TourController
{

   // private TourRepository tourRepository = new TourRepository();
    private TourKeyPointService tourKeyPointService = new TourKeyPointService(new TourKeyPointRepository());
    private TourReservationService reservationService = new TourReservationService(new TourReservationRepository());

    private TourService tourService = new TourService(new TourRepository());

    private LocationService locationService = new (new LocationRepository());
    //private ImageService imageService = new ImageService();
    //private UserService userService = new UserService();

    public List<Tour> GetAll()
    {
        return tourService.GetAll();
    }

    public Tour GetById(int id)
    {
        return tourService.GetById(id);
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


    public Tour Create(TourToControllerDto dto)
    {
        Tour newTour = new Tour(dto.Name, dto.Description, dto.Language,
            dto.GuestLimit, dto.StartDateAndTime,
            dto.Duration);

        return  tourService.Save(newTour);

    }

    public void UpdateTourProperties(Tour tour, TourToControllerDto dto, List<TourKeyPoint> keyPoints)
    {
        //int tourId = tourService.get(tour.Id);

        // tourKeyPointService.SetTypes(tourKeyPoints);
        

       tourService.UpdateTourProperties(tour, dto, keyPoints);

    }

    public List<TourBasicInfoDto> Get()
    {
        return tourService.Get();
    }
    public List<TourBasicInfoDto> GetTodays()
    {
        return tourService.GetTodays();
    }

    public void SetStatus(int id, TourStatus status)
    {
        tourService.SetStatus(id,status);
    }

    public List<TourBasicInfoDto> GetByStatus(int guideId, TourStatus status)
    {
        return tourService.GetByStatus(guideId, status);
    }

    public void Cancel(int id)
    {
        tourService.Cancel(id);
    }

    public TourGuestsDto GetMostVisitedTour(int guideId, string time)
    {
        return tourService.GetMostVisitedTour(guideId, time);
    }
}

