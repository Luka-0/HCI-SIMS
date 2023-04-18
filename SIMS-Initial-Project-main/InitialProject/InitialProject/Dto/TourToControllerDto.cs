using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using InitialProject.Model;
using Microsoft.VisualBasic;

namespace InitialProject.Dto;

public class TourToControllerDto
{
    public string Name;
    public string Country;
    public string City;
    public string Description;
    public string Language;
    public User Guide;
    public int GuestLimit;
    public List<String> TourKeyPointNames;
    public DateTime StartDateAndTime;
    public TimeSpan Duration;
    public List<String> ImageURLs;



    public TourToControllerDto() { }

    public TourToControllerDto(string name, string country, string city, string description, string language, User guide, int guestLimit, List<String> tourKeyPointNames,DateTime startDateAndTime, TimeSpan duration, List<String> imageURLs)
    {
        Name = name;
        Country = country;
        City = city;
        Description = description;
        Language = language;
        this.Guide = guide;
        GuestLimit = guestLimit;
        TourKeyPointNames = tourKeyPointNames;
        StartDateAndTime = startDateAndTime;
        Duration = duration;
        ImageURLs = imageURLs;
    }

    
}