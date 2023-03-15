using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using InitialProject.Model;
using Microsoft.VisualBasic;

namespace InitialProject.Dto;

public class NewTourDto
{
    public string Name;
    public string Country;
    public string City;
    public string Description;
    public string Language;
    public int GuestLimit;
    //public List<TourKeyPoint> TourKeyPoints;
    public List<int> TourKeyPoints;
    public DateAndTime StartDate;
    public string StartTime;
    public TimeOnly Duration;
    public List<string> ImageURLs;



    public NewTourDto(){}

    public NewTourDto(string name, string country, string city, string description, string language, int guestLimit, List<int> tourKeyPoints, DateAndTime startDate, string startTime, TimeOnly duration, List<string> imageURLs)
    {
        Name = name;
        Country = country;
        City = city;
        Description = description;
        Language = language;
        GuestLimit = guestLimit;
        TourKeyPoints = tourKeyPoints;
        StartDate = startDate;
        StartTime = startTime;
        Duration = duration;
        ImageURLs = imageURLs;
    }

    public NewTourDto(NewTourDto newTourDto)
    {
        this.Name = newTourDto.Name;
        this.Country= newTourDto.Country;
        this.City = newTourDto.City;
        this.Description = newTourDto.Description;
        this.Language = newTourDto.Language;
        this.GuestLimit = newTourDto.GuestLimit;
        this.TourKeyPoints= newTourDto.TourKeyPoints;
        this.StartDate = newTourDto.StartDate;
        this.StartTime = newTourDto.StartTime;
        this.Duration = newTourDto.Duration;
        this.ImageURLs = newTourDto.ImageURLs;

    }
}