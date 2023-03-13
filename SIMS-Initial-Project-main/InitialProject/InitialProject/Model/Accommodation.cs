using InitialProject.Enumeration;
using System;
using System.ComponentModel.DataAnnotations;

namespace InitialProject.Model
{
    public class Accommodation
    {
        //[Key]?
        public int Id { get; set; }
        String Title { get; set; }
        int GuestLimit { get; set; }
        AccommodationType Type { get; set; }
        int MinimumReservationDays { get; set; }
        int CancellationDeadline { get; set; }
        bool Available { get; set; }

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