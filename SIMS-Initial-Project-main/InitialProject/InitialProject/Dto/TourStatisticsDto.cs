namespace InitialProject.Dto;

public class TourStatisticsDto
{
    public int TourId { get; set; }
    public string TourName { get; set; }
    public int YouthCount{ get; set; }
    public int MiddleAgedCount { get; set; }
    public int OldPeopleCount{ get; set; }
    public double WithVouchers { get; set; }
    public double WithoutVouchers{ get; set; }


    public TourStatisticsDto()
    {
    }

}