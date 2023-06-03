using InitialProject.Contexts;
using InitialProject.Controller;
using InitialProject.Model;
using InitialProject.View.Guest1;
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
using System.Windows.Shapes;

namespace InitialProject.View
{
    public partial class Guest1Menu : Window
    {
        private readonly ReservationReschedulingRequestController ReservationReschedulingRequestController = new();
        private readonly AccommodationReservationController AccommodationReservationController = new();
        private readonly UserController UserController = new();

        private User User { get; set; }

        public Guest1Menu(User user)
        {
            InitializeComponent();
            User = user;

            CheckNotifications();
            CheckSuperTitleRequirements();
        }

        private void CheckNotifications()
        {
            List<ReservationReschedulingRequest> reschedulings = ReservationReschedulingRequestController.GetAllByUser(User.Id);
            bool notify = false;

            foreach(ReservationReschedulingRequest r in reschedulings)
            {
                if (r.WasNotified == false && r.State != Enumeration.RequestState.Waiting)
                {
                    notify = true;
                    ReservationReschedulingRequestController.UpdateWasNotifiedBy(r.Id, true);
                }
            }

            if(notify)
            {
                MessageBox.Show("One of your requests for reservation was answered by the owner");
            }
        }

        private void CheckSuperTitleRequirements()
        {
            if (!User.SuperTitle)
            {
                List<AccommodationReservation> reservations = AccommodationReservationController.GetDuringLastYearBy(User);
                if (reservations.Count > 9)
                {
                    UserController.UpdateBy(User.Id, true, 5, DateTime.Now.AddYears(1));
                    MessageBox.Show("You have become a SUPER GUEST");
                }

                return;
            }

            else if (User.SuperTitleValidTill < DateTime.Now)
            {
                List<AccommodationReservation> reservations = AccommodationReservationController.GetDuringLastYearBy(User);

                if (reservations.Count > 9)
                {
                    UserController.UpdateBy(User.Id, true, 5, DateTime.Now.AddYears(1));
                    MessageBox.Show("You have become a SUPER GUEST");
                }
                else
                {
                    UserController.UpdateBy(User.Id, false, 0);
                    MessageBox.Show("You are no longer super guest");
                }
            }
        }

        private void AccommodationReservate_Click(object sender, RoutedEventArgs e)
        {

            AccommodationReservate accommodationReservate = new(User);
            accommodationReservate.Show();

            Close();
        }

        private void EditReservation_Click(object sender, RoutedEventArgs e)
        {
            EditReservation editReservation = new(User);
            editReservation.Show();

            Close();
        }

        private void RateAccommodation_Click(object sender, RoutedEventArgs e)
        {
            RateAccommodation rateAccommodation = new(User);
            rateAccommodation.Show();

            Close();
        }

        private void ViewReviews_Click(object sender, RoutedEventArgs e)
        {
            ViewRatings viewRatings = new(User);
            viewRatings.Show();

            Close();
        }

        private void AnywhereWhenever_Click(object sender, RoutedEventArgs e)
        {
            AnywhereWhenever anywhereWhenever = new(User);
            anywhereWhenever.Show();

            Close();
        }

        private void CreateForum_Click(object sender, RoutedEventArgs e)
        {
            CreateForum createForum = new(User);
            createForum.Show();

            Close();
        }

        private void CommentOnForum_Click(object sender, RoutedEventArgs e)
        {
            CommentOnForum commentOnForum = new(User);
            commentOnForum.Show();

            Close();
        }
    }
}
