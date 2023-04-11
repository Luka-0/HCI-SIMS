using System;
using System.Collections.Generic;
using System.Linq;
using InitialProject.Contexts;
using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.VisualBasic;

namespace InitialProject.Service;

public class TourService
{
    private readonly TourReservationService tourReservationService = new TourReservationService(new TourReservationRepository());
    //private readonly TourKeyPointService tourKeyPointService = new TourKeyPointService(new TourKeyPointRepository());
    private readonly LocationService locationService = new LocationService(new LocationRepository());
    private readonly ImageService imageService = new ImageService(new ImageRepository());
    private readonly ITourRepository _tourRepository;

    public TourService(ITourRepository repository)
    {
        _tourRepository = repository;
    }
    public Tour Save(Tour tour)
    {

        tour = _tourRepository.Save(tour);
        return tour;

    }
    public List<Tour> GetAll()
    {
        return _tourRepository.GetAll();
    }

    
    public List<TourBasicInfoDto> Get()
    {
        List<Tour> tours = GetAll();

        List<TourBasicInfoDto> basicInfoDtos = tours.Select(tour =>
            new TourBasicInfoDto(tour.Id, tour.Name, tour.Location.Country, tour.Location.City,
                    tour.Language,tour.Guide.Id, tour.GuestLimit, tour.StartDateAndTime, tour.Status))
                        .ToList();


        return basicInfoDtos;
    }

    public List<TourBasicInfoDto> GetTodays ()
    {
        List<TourBasicInfoDto> tours = new List<TourBasicInfoDto>();
        tours = Get();

        List<TourBasicInfoDto> todayTours = tours.Where(t =>t.Status == TourStatus.Waiting &&
                                                 t.StartDateAndTime.Date.Equals(DateTime.Today)).ToList();


        return todayTours;

    }

   

    public Tour GetById(int id)
    {
        return _tourRepository.GetById(id);
    }
  
    public List<Tour> GetByLocation(Location location)
    {
        return _tourRepository.GetByLocation(location);
    }

    public List<Tour> GetByDuration(TimeSpan duration)
    {
        return _tourRepository.GetByDuration(duration);
    }

    public List<Tour> GetByLanguage(string language)
    {
        return _tourRepository.GetByLanguage(language);
    }

    public List<Tour> GetByGuestLimit(int guestNumber)
    {
        return _tourRepository.GetByGuestLimit(guestNumber);
    }

    public bool IsActive(Tour tour)
    {
        if (tour.Status == TourStatus.Started)
        {
            return true;
        }
        return false;
    }

    public List<Tour> GetAllActive()
    {
        List<Tour> tours = _tourRepository.GetAll();
        List<Tour> activeTours = new List<Tour>();

        foreach (Tour tour in tours)
        {
            if (IsActive(tour))
                activeTours.Add(tour);
        }
        return activeTours;
    }

    public bool IsTourInCollection(Tour tour, ICollection<Tour> collection)
    {
        foreach(Tour t in collection)
        {
            if(tour.Id == t.Id)
                return true;
        }

        return false;
    }

    public List<Tour> GetAllActiveByGuest(User user)
    {
        List<Tour> activeToursByGuest = new List<Tour>();

        List<Tour> activeTours = GetAllActive();
        List<TourReservation> tourReservations = tourReservationService.GetByGuest(user);

        

        foreach(TourReservation reservation in tourReservations)
        {
            if (IsTourInCollection(reservation.Tour, activeTours))
                activeToursByGuest.Add(reservation.Tour);
        }


        return activeToursByGuest;
    }

    public void SetStatus(int id, TourStatus status)
    {
        _tourRepository.SetStatus(id, status);
    }

    public List<TourBasicInfoDto> GetByStatus(int guideId, TourStatus status)
    {
        List<TourBasicInfoDto> tours = new List<TourBasicInfoDto>();

        tours = Get();

        List<TourBasicInfoDto> finishedTours = tours.Where(t => t.Status == status).ToList();

        return finishedTours;
    }

    public void Cancel(int id)
    {
        tourReservationService.GiveOutVouchers(id);
        _tourRepository.Delete(id);

    }

    public void UpdateTourProperties(Tour tour, TourToControllerDto dto, List<TourKeyPoint> keyPoints)
    {
        tour.Guide = dto.Guide;
        tour.Location = locationService.GetBy(dto.Country, dto.City);
        List<Image> images = imageService.Save(dto.ImageURLs);
        imageService.SetTourId(images, tour);

    }

    public int GetMostVisitedTour(int guideId, string time)
    {
        List<Tour> allTours = _tourRepository.GetAllByGuide(guideId);

        List<Tour> selected = Select(allTours, time);

        return FindBestTour(selected);
    }

    private List<Tour> Select(List<Tour> allTours, string time)
    {
        if (time.Equals("All time"))
        {
            return allTours;
        }
        else
        {
            return allTours.Where(t => t.StartDateAndTime.Date.Year.ToString().Equals(time)).ToList();
        }
    }

    private int FindBestTour(List<Tour> tours)
    {
        Dictionary<string, TourGuestsDto> tourAndTotalTourists = new Dictionary<string, TourGuestsDto>();

        foreach (Tour tour in tours)
        {
            if (!tourAndTotalTourists.ContainsKey(tour.Name))
            {
                int tourists = tourReservationService.getGuestNumber(tour.Id);
                tourAndTotalTourists.Add(tour.Name, new TourGuestsDto(tour.Id, tourists));
            }
            else
            {
                TourGuestsDto dto = tourAndTotalTourists[tour.Name];
                dto.TotalGuests+= tourReservationService.getGuestNumber(tour.Id);
            }
        }
        TourGuestsDto maxPair = tourAndTotalTourists.FirstOrDefault(x => x.Value.TotalGuests == tourAndTotalTourists.Max(y => y.Value.TotalGuests)).Value;

        return maxPair.TourId;
    }
}
