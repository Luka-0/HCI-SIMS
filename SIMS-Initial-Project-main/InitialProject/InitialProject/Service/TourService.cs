﻿using System;
using System.Collections.Generic;
using InitialProject.Contexts;
using InitialProject.Dto;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.VisualBasic;

namespace InitialProject.Service;

public class TourService
{
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
            return _tourRepository.GetAll();
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
}
