using InitialProject.Enumeration;
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

        //Images are in Image table
        public List<Image> images { get; set; }

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


    }
}