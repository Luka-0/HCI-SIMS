using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    [Table("Renovation")]
    public  class Renovation
    {

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Description { get; set; }
            public TimeSpan Duration { get; set; }

            [DataType(DataType.DateTime)]
            public DateTime Start { get; set; }

            [DataType(DataType.DateTime)]
            public DateTime End { get; set; }

            [ForeignKey("accommodationId")]
            public Accommodation Accommodation { get; set; }

            public Renovation(string description, Accommodation accommodation, TimeSpan duration, DateTime start, DateTime end)
            {

                this.Accommodation = accommodation;
                this.Start = start;
                this.End = end;
                this.Description = description;
                this.Duration = duration;

            }
            public Renovation()
            {

            }
    }
}
