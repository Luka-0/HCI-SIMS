﻿using System;
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

    public int getBy(int id)
    {
        var db = new UserContext();
        var tempRecord = db.tours.Find(id);   //Try creating method in Accommodation repository to return the same thing
        return tempRecord.Id;
    }

    public List<Tour> getAll()
    {
        return tourRepository.getAll();
    }

    public Location getLocationByTourId(int id)
    {
       return tourRepository.getLocationByTourId(id);
    }
    public List<TourBasicInfoDto> getBasicInfo()
    {
        List<TourBasicInfoDto> basicInfoDtos = new List<TourBasicInfoDto>();
        List<Tour> tours = getAll();

        foreach (var tour in tours)
        {
            
            Location location = getLocationByTourId(tour.Id);
            TourBasicInfoDto basicInfo = new TourBasicInfoDto(tour.Id, tour.Name,
                location.Country, location.City, tour.Language, tour.GuestLimit,tour.StartDateAndTime );
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
}