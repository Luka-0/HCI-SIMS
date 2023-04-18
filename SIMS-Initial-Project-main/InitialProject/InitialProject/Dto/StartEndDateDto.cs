using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    public class StartEndDateDto
    {
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }

        public StartEndDateDto(DateTime startingDate, DateTime endingDate)
        {
            StartingDate = startingDate;
            EndingDate = endingDate;
        }
    }
}
