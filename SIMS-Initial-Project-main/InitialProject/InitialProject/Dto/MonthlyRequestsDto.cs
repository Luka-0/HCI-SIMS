using System.Collections.Generic;

namespace InitialProject.Dto;

public class MonthlyRequestsDto
{
    public string Month{ get; set; }
    public int Count { get; set; }

    public MonthlyRequestsDto(string month)
    {
        Month = month;
    }
}