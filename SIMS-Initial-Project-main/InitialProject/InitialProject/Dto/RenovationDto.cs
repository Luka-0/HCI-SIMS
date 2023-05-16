using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    public class RenovationDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Accommodation { get; set; }

        public RenovationDto(Renovation renovation)
        {
            this.Id = renovation.Id;
            this.Description = renovation.Description;
            this.Duration = renovation.Duration;
            this.Start = renovation.Start;
            this.End = renovation.End;
            this.Accommodation = renovation.Accommodation.Title;
        }
    }

}
