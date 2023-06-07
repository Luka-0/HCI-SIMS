using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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
    [AllowNull]
    public DateTime? SelectedDate { get; set; }

    [ForeignKey("TouristId")]
    public User? Tourist { get; set; }
    
    [ForeignKey("ComplexTourRequestId"), AllowNull]
    public ComplexTourRequest? ComplexTourRequest { get; set; }


    public TourRequest()
    {
        State = TourRequestState.Pending;
    }
}
