using InitialProject.Enumeration;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InitialProject.Model
{
    [Table("Accommodation")]
    public class Accommodation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Title { get; set; }
        public int GuestLimit { get; set; }
        public AccommodationType Type { get; set; }
        public int MinimumReservationDays { get; set; }
        public int CancellationDeadline { get; set; }
        public bool Available { get; set; }

        //changes
        [ForeignKey("locationID")]
        public Location? Location { get; set; }

        //Images are in Image table
        public List<Image> Images { get; set; }

        //changes
        [ForeignKey("ownerId")]
        public User? Owner { get; set; }

        public string Class { get; set; }

        public Accommodation()
        {
            this.Title = "";
            this.CancellationDeadline = 1;
            this.MinimumReservationDays = 1;
            this.Type = AccommodationType.House;
            this.GuestLimit = 1;
            this.Available = true;
            this.Class = "B";//smestaji super-vlasnika ce biti oznaceni kao smestaji klase A
        }


        public Accommodation(String title, int guestLimit, AccommodationType type, int minimumReservationDays,
            int cancellationDeadline)
        {
            this.Title = title;
            this.GuestLimit = guestLimit;
            this.Type = type;
            this.MinimumReservationDays = minimumReservationDays;
            this.CancellationDeadline = cancellationDeadline;
            this.Available = true;
            this.Class = "B";
        }
        /*
        public Accommodation()
        {
            CancellationDeadline = 1;
            Available = true;
        }
        public Accommodation(String title, int guestLimit, AccommodationType type, int minimumReservationDays, int cancellationDeadline)
        {
            CancellationDeadline = 1;

            Type = type;
            MinimumReservationDays = minimumReservationDays;
            CancellationDeadline = cancellationDeadline;
            Title = title;
            Available = true;

        }
        */

        public Accommodation(Accommodation accommodation)
        {
            this.Title = accommodation.Title;
            this.GuestLimit = accommodation.GuestLimit;
            this.Type = accommodation.Type;
            this.MinimumReservationDays = accommodation.MinimumReservationDays;
            this.CancellationDeadline = accommodation.CancellationDeadline;
            this.Available = accommodation.Available;
            this.Class = "B";
        }


    }
}