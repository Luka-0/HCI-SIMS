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
    public string GuestLimit;
    public string TourKeyPointNames;
    public string StartDate;
    public string StartTime;
    public string Duration;
    public string ImageURLs;



    public NewTourDto(){}

    public NewTourDto(string name, string country, string city, string description, string language, string guestLimit, string tourKeyPointNames,  string startDate, string startTime,  string duration, string imageURLs)
    {
        Name = name;
        Country = country;
        City = city;
        Description = description;
        Language = language;
        GuestLimit = guestLimit;
        TourKeyPointNames = tourKeyPointNames;
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
        this.TourKeyPointNames= newTourDto.TourKeyPointNames;
        this.StartDate = newTourDto.StartDate;
        this.StartTime = newTourDto.StartTime;
        this.Duration = newTourDto.Duration;
        this.ImageURLs = newTourDto.ImageURLs;

    }
}