using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for Notifications.xaml
    /// </summary>
    public partial class Notifications : Page
    {
        GuestReviewController GuestReviewController = new GuestReviewController();
        AccommodationReservationController AccommodationReservationController = new AccommodationReservationController();

        public string OwnerUsername;

        public Notifications(string ownerUsername)
        {
            OwnerUsername = ownerUsername;
            InitializeComponent();
            LoadNotifications();
     
        }

        public void LoadNotifications() {

            LoadExpiredReservationsNotifications();
            LoadCancelledReservationNotifications();

        }

        public void LoadExpiredReservationsNotifications() {

            List<ExpiredReservationDto> expiredReservations = new List<ExpiredReservationDto>();
            expiredReservations = GuestReviewController.LoadExpiredReservations(OwnerUsername);

            if (expiredReservations.Count == 0) {
                notifications.Items.Clear();
                return;
            }

            string message;

            foreach (ExpiredReservationDto reservation in expiredReservations) {

                message = PrepareMessage(reservation);
                notifications.Items.Add(message);   
            }
        }

        public string PrepareMessage(ExpiredReservationDto reservation) {

            DateTime todaysDate = DateTime.UtcNow.Date;
            int daysLeft = 5 - (todaysDate.Day - reservation.EndingDate.Day);

            string message;

            if (daysLeft == 1)
            {
                return message = "Reservation "+ reservation.ReservationId +" has expired: \n   " + daysLeft.ToString() + " day left to rate guest: " + reservation.GuestUsername;
            }

            return message = "Reservation "+ reservation.ReservationId +" has expired: \n   " + daysLeft.ToString() + " days left to rate guest: " + reservation.GuestUsername;
        }

        public void LoadCancelledReservationNotifications() {

            List<AccommodationReservation> cancelledReservations = new List<AccommodationReservation>();
            cancelledReservations = AccommodationReservationController.GetAllCancelled(OwnerUsername);

            List<string> cancelledReservationsNotifications = new List<string>();

            foreach (var reservation in cancelledReservations)
            {

                cancelledReservationsNotifications.Add("Reservation: " + reservation.Id + "has been cancelled.");
            }

            cancellationNotification.ItemsSource = cancelledReservationsNotifications;

        }
    }
}
