using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Packaging;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace InitialProject.Model;

[Table("Tour")]
public class Tour
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    //Tour Location as a ForeignKey to Location table
    [ForeignKey("locationID")]
    public Location Location { get; set; }

    public string Description { get; set; }
    public string Language { get; set; }
    public int GuestLimit { get; set; }

    //KeyPoints are in TourKeyPoints Table
    public List<TourKeyPoint> TourKeyPoints { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime StartDateAndTime{ get;set; }

    [DataType(DataType.Time)]
    public TimeOnly Duration { get; set; }

    //Images are in Image table
    public List<Image> images { get; set; }
    public Boolean Started { get; set; }

    public Tour()
    {
        Started = false;
    }

    public Tour(String name, String language, int guestLimit, List<TourKeyPoint> tourKeyPoints)
    {
        Started = false;
    }


    public override string ToString()
    {
        return Name + " " + Language + " " + Description;
    }


}