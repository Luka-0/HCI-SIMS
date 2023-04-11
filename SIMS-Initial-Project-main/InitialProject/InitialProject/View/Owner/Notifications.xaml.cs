using InitialProject.Controller;
using InitialProject.Dto;
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

        public string OwnerUsername;

        public Notifications(string ownerUsername)
        {
            OwnerUsername = ownerUsername;
            InitializeComponent();
            LoadNotifications();
     
        }

        public void LoadNotifications() {

            List<ExpiredReservationDto> records = new List<ExpiredReservationDto>();
            records = GuestReviewController.LoadExpiredReservations(OwnerUsername);

            if (records.Count == 0) {
                notifications.Items.Clear();
                return;
            }

            string message;

            foreach (ExpiredReservationDto record in records) {

                message = PrepareMessage(record);
                notifications.Items.Add(message);   
            }
        }

        public string PrepareMessage(ExpiredReservationDto record) {

            DateTime todaysDate = DateTime.UtcNow.Date;
            int daysLeft = 5 - (todaysDate.Day - record.EndingDate.Day);

            string message;

            if (daysLeft == 1)
            {
                return message = "Reservation "+ record.ReservationId +" has expired: \n   " + daysLeft.ToString() + " day left to rate guest: " + record.GuestUsername;
            }

            return message = "Reservation "+ record.ReservationId +" has expired: \n   " + daysLeft.ToString() + " days left to rate guest: " + record.GuestUsername;
        }
    }
}
