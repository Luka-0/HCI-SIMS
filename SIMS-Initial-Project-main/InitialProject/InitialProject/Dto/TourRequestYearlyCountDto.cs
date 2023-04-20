using System;
using System.Collections.Generic;
using System.Net;

namespace InitialProject.Dto;

public class TourRequestYearlyCountDto
{
    public int Year { get; set; }
    public int Count { get; set; }
    public List<DateTime> Dates { get; set; }



    public TourRequestYearlyCountDto(int year, DateTime date)
    {
        Year = year;
        Count = 1;
        Dates = new List<DateTime>();
        Dates.Add(date);
    }

    public  void Increase()
    {
        Count++;
    }
}