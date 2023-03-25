using System;

namespace InitialProject.Dto;

public class TourBasicInfoDto
{
    public int id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Language { get; set; }
    public int GuestLimit { get; set; }
    public DateTime StartDateAndTime { get; set; }


    public TourBasicInfoDto()
    {
    }

    public TourBasicInfoDto(int id, string name, string country, string city, string language, int guestLimit,
        DateTime startDateAndTime)
    {
        this.id = id;
        this.Name = name;
        this.Country = country;
        this.City = city;
        this.Language = language;
        this.GuestLimit = guestLimit;
        this.StartDateAndTime = startDateAndTime;
    }


}