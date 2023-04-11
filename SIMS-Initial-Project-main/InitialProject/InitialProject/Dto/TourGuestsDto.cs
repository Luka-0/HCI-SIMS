namespace InitialProject.Dto;

public class TourGuestsDto
{
    public int TourId { get; set; }
    public int TotalGuests { get; set; }


    public TourGuestsDto(int tourId, int totalGuests)
    {
     TourId = tourId;
     TotalGuests = totalGuests;
    }

}