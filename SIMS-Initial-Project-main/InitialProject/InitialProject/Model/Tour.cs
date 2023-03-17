﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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
    [AllowNull]
    public Location? Location { get; set; }

    public string Description { get; set; }
    public string Language { get; set; }
    public int GuestLimit { get; set; }

    //KeyPoints are in TourKeyPoints Table
    public List<TourKeyPoint>? TourKeyPoints { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime StartDateAndTime{ get;set; }

    [DataType(DataType.Time)]
    public TimeSpan Duration { get; set; }

    //Images are in Image table
    public List<Image>? images { get; set; }
    public Boolean Started { get; set; }

    public Tour()
    {
        Started = false;
    }

    public Tour(String name, String language, int guestLimit, List<TourKeyPoint> tourKeyPoints)
    {
        Started = false;
    }

    public Tour(String name, Location location, string description, string language, int guestLimit,
        List<TourKeyPoint> keyPoints, DateTime start, TimeSpan duration, List<Image> images)
    {
        this.Name = name;
        this.Location = location;
        this.Description = description;
        this.Language = language;
        this.GuestLimit = guestLimit;
        this.TourKeyPoints = keyPoints;
        this.StartDateAndTime = start;
        this.Duration = duration;
        this.images = images;
        this.Started = false;
    }
    public Tour(String name, string description, string language, int guestLimit,
        List<TourKeyPoint> keyPoints, DateTime start, TimeSpan duration, List<Image> images)
    {
        this.Name = name;
        this.Location = null;
        this.Description = description;
        this.Language = language;
        this.GuestLimit = guestLimit;
        this.TourKeyPoints = keyPoints;
        this.StartDateAndTime = start;
        this.Duration = duration;
        this.images = images;
        this.Started = false;
    }





}