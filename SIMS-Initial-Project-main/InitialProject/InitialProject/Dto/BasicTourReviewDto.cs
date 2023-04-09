using System;
using InitialProject.Model;

namespace InitialProject.Dto;

public class BasicTourReviewDto
{
    public String TouristName { get; set; }
    public Boolean Valid { get; set; }
    public TourReview Review { get; set; }
    public String ArrivalKeyPointName { get; set; }


    public BasicTourReviewDto(string username, bool valid, TourReview review, string arrivalKeyPointName)
    {
        TouristName = username;
        Valid = valid;
        Review = review;
       ArrivalKeyPointName = arrivalKeyPointName;
    }

    public BasicTourReviewDto(){}
}