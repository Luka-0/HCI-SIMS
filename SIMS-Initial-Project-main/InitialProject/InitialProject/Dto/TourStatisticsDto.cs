namespace InitialProject.Dto;

public class TourStatisticsDto
{
    public int TourId { get; set; }
    public string TourName { get; set; }
    public int YouthCount{ get; set; }
    public int MiddleAgedCount { get; set; }
    public int OldPeopleCount{ get; set; }
    public int WithVouchers { get; set; }
    public int WithoutVouchers{ get; set; }


    public TourStatisticsDto()
    {
    }
    public TourStatisticsDto(TourGuestsDto dto)
    {
        TourId = dto.TourId;
        TourName = "";
        YouthCount = dto.TotalYouth;
        MiddleAgedCount = dto.TotalMiddleAged;
        OldPeopleCount = dto.TotalOld;
        SetPercentage(dto.TotalGuests, dto.TotalWithVoucher);
    }
    private void SetPercentage(int totalGuests, int withVouchers)
    {
        WithVouchers = (int)((double)withVouchers / (double)totalGuests * 100.0);
        WithoutVouchers= 100 - WithVouchers;
    }



}