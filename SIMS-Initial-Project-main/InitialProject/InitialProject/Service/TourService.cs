using System;
using System.Collections.Generic;
using InitialProject.Contexts;
using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.VisualBasic;

namespace InitialProject.Service;

public class TourService
{
    TourRepository tourRepository = new TourRepository();
    public Tour Save(Tour tour)
    {
        
           tour =  tourRepository.Save(tour);
            return tour;

    }

    /*public Location getLocationByTourId(int id)
    {
       return tourRepository.getLocationByTourId(id);
    }
    */
    //TODO Pavle ispravi
    public List<TourBasicInfoDto> Get()
    {
        List<TourBasicInfoDto> basicInfoDtos = new List<TourBasicInfoDto>();
        List<Tour> tours = GetAll();

        foreach (var tour in tours)
        {
            
            //Location location = getLocationByTourId(tour.Id);
            TourBasicInfoDto basicInfo = new TourBasicInfoDto(tour.Id, tour.Name,
                tour.Location.Country, tour.Location.City, tour.Language, tour.GuestLimit,tour.StartDateAndTime );
            basicInfoDtos.Add(basicInfo);
        }
        return basicInfoDtos;
    }

    //TODO Pavle ispravi
    public List<TourBasicInfoDto> GetTodays()
    {
        List<TourBasicInfoDto> todaysTours = new List<TourBasicInfoDto>();
        List<TourBasicInfoDto> tours = new List<TourBasicInfoDto>();
        tours = Get();
        foreach (var tour in tours)
        {
            DateTime date = tour.StartDateAndTime.Date;
            if (date.Equals(DateTime.Today))
            {
                todaysTours.Add(tour);
            }
        }

        return todaysTours;

    }

        public List<Tour> GetAll()
        {
            return tourRepository.GetAll();
        }

        public Tour GetById(int id)
        {
            return tourRepository.GetById(id);
        }
        public List<Tour> GetByLocation(Location location)
        {
            return tourRepository.GetByLocation(location);
        }

        public List<Tour> GetByDuration(TimeSpan duration)
        {
            return tourRepository.GetByDuration(duration);
        }

        public List<Tour> GetByLanguage(string language)
        {
            return tourRepository.GetByLanguage(language);
        }

        public List<Tour> GetByGuestLimit(int guestNumber)
        {
            return tourRepository.GetByGuestLimit(guestNumber);
        }
}
