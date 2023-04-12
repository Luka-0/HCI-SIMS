using InitialProject.Model;

namespace InitialProject.Dto;

public class TourGuestsDto
{
    public int TourId { get; set; }
    public int TotalGuests { get; set; }
    public int TotalYouth { get; set; }
    public int TotalMiddleAged { get; set; }
    public int TotalOld { get; set; }
    public int TotalWithVoucher { get; set; }
    public int TotalWithoutVoucher { get; set; }

    public TourGuestsDto(int tourId, int totalGuests)
    {
     TourId = tourId;
     TotalGuests = totalGuests;
    }

    public TourGuestsDto()
    {
    }

    public TourGuestsDto(TourGuestsDto dto)
    {
        TourId = dto.TourId;
        TotalGuests = dto.TotalGuests;
        TotalYouth = dto.TotalYouth;
        TotalMiddleAged = dto.TotalMiddleAged;
        TotalOld = dto.TotalOld;
        TotalWithVoucher = dto.TotalWithVoucher;
        TotalWithoutVoucher = dto.TotalWithoutVoucher;
    }

    
    public void AddByAge(int bookingGuestAge, int count)
    {
        if (bookingGuestAge < 18)
        {
            TotalYouth += count;
        }
        else
        {
            if (bookingGuestAge < 50)
            {
                TotalMiddleAged += count;
            }
            else
            {
                TotalOld += count;
            }
        }
    }

    public void AddByVoucher(Voucher voucher, int count)
    {
        if (voucher == null)
        {
            TotalWithoutVoucher += count;
        }
        else
        {
            TotalWithVoucher += count;
        }
    }
}