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
    public Tour save(Tour tour)
    {
        
           tour =  tourRepository.save(tour);
            return tour;

    }

    /*public Location getLocationByTourId(int id)
    {
       return tourRepository.getLocationByTourId(id);
    }
    */
    public List<TourBasicInfoDto> getBasicInfo()
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

    public List<TourBasicInfoDto> getTodaysToursBasicInfo()
    {
        List<TourBasicInfoDto> todaysTours = new List<TourBasicInfoDto>();
        List<TourBasicInfoDto> tours = new List<TourBasicInfoDto>();
        tours = getBasicInfo();
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
        public List<Tour> GetBy(Location location)
        {
            return tourRepository.GetBy(location);
        }

        public List<Tour> GetBy(TimeSpan duration)
        {
            return tourRepository.GetBy(duration);
        }

        public List<Tour> GetBy(string language)
        {
            return tourRepository.GetBy(language);
        }

        public List<Tour> GetBy(int guestNumber)
        {
            return tourRepository.GetBy(guestNumber);
        }
}
