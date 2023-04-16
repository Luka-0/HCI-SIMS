using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InitialProject.Enumeration;
using Microsoft.VisualBasic;

namespace InitialProject.Model;

[Table("TourRequest")]
public class TourRequest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [ForeignKey("LocationId")]
    public Location? Location { get; set; }
    public string Language { get; set; }
    public int GuestNumber { get; set; }
    public string Description { get; set; }
    public DateTime LowerDateLimit { get; set; }
    public DateTime UpperDateLimit { get; set; }
    public TourRequestState State { get; set; }

    public TourRequest()
    {
        State = TourRequestState.Pending;
    }
}
