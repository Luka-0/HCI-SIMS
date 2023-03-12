using System;
using System.Collections.Generic;
using System.IO.Packaging;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace InitialProject.Model;

public class Tour
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Language { get; set; }
    public int GuestLimit { get; set; }
    public List<TourKeyPoint> TourKeyPoints { get; set; }
    public DateAndTime StartDateAndTime{ get;set; }
    public TimeOnly Duration { get; set; }
    public List<String> Pictures { get; set; }
    public Boolean Started { get; set; }


    public Tour()
    {
        Started = false;
    }

    public Tour(String name, String language, int guestLimit, List<TourKeyPoint> tourKeyPoints)
    {
        Started = false;
    }





}