using InitialProject.Controller;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InitialProject.View.Guest1
{
    public partial class EditReservation : Window
    {
        private readonly AccommodationReservationController AccommodationReservationController = new();
        private readonly ReservationReschedulingRequestController ReservationReschedulingRequestController = new();

        public ObservableCollection<AccommodationReservation> ReservationsToShow { get; set; }
        public ObservableCollection<ReservationReschedulingRequest> ReschedulingsToShow { get; set; }

        private User User { get; set; }

        public EditReservation(User user)
        {
            InitializeComponent();
            DataContext = this;
            User = user;

            List<AccommodationReservation> allReservations = AccommodationReservationController.GetBy(User);
            if(allReservations.Count == 0)
            {
                MessageBox.Show("There are currently no reservations to show");
                Close();
            }

            RefreshReservationDataGrid(allReservations);

            RefreshReschedulingDataGrid();
        }

        private void RemoveRedFromControls()
        {
            ReservationsGrid.ClearValue(Border.BorderThicknessProperty);
            ReservationsGrid.ClearValue(Border.BorderBrushProperty);

            DaysToPostponeTB.ClearValue(Border.BorderThicknessProperty);
            DaysToPostponeTB.ClearValue(Border.BorderBrushProperty);
        }

        public void MakeControlRed<T>(T control) where T : Control
        {
            control.Focus();
            control.BorderBrush = new SolidColorBrush(Colors.Red);
            control.BorderThickness = new Thickness(2);
        }

        private void RefreshReservationDataGrid(List<AccommodationReservation> accommodationReservations)
        {
            ReservationsToShow = new ObservableCollection<AccommodationReservation>();
            ReservationsGrid.ItemsSource = ReservationsToShow;

            foreach (AccommodationReservation a in accommodationReservations)
            {
                ReservationsToShow.Add(a);
            }
        }

        private void RefreshReschedulingDataGrid()
        {
            List<ReservationReschedulingRequest> allrequests = ReservationReschedulingRequestController.GetAllByUser(User.Id);

            ReschedulingsToShow = new ObservableCollection<ReservationReschedulingRequest>();
            ReservationsResceduleDG.ItemsSource = ReschedulingsToShow;

            foreach(ReservationReschedulingRequest r in allrequests)
            {
                ReschedulingsToShow.Add(r);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservation accommodationReservation = (AccommodationReservation)ReservationsGrid.SelectedItem;

            if (IsViolatingAnyUIControl(accommodationReservation)) return;

            if (!AccommodationReservationController.Delete(true, accommodationReservation))
            {
                MessageBox.Show("You can't cancel that reservation");
                return;
            }

            RemoveRedFromControls();
            MessageBox.Show("Successful");
        }

        private bool IsViolatingAnyUIControl(AccommodationReservation accommodationReservation, bool clickedPostpone = false)
        {
            if(accommodationReservation == null)
            {
                MessageBox.Show("Please select a reservation you want to cancel");

                MakeControlRed(ReservationsGrid);

                return true;
            }

            if (clickedPostpone)
            {
                if(int.Parse(DaysToPostponeTB.Text) <= 0)
                {
                    MessageBox.Show("Please enter a proper number of days to postpone by");

                    MakeControlRed(DaysToPostponeTB);

                    return true;
                }
            }

            RemoveRedFromControls();
            return false;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Guest1Menu guest1Menu = new(User);
            guest1Menu.Show();

            Close();
        }

        private void Postpone_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservation accommodationReservation = (AccommodationReservation)ReservationsGrid.SelectedItem;

            if (IsViolatingAnyUIControl(accommodationReservation, true)) return;

            int postponeDays = int.Parse(DaysToPostponeTB.Text);

            DateTime newStartingDate = accommodationReservation.BegginingDate.AddDays(postponeDays);
            DateTime newEndingDate = accommodationReservation.EndingDate.AddDays(postponeDays);

            ReservationReschedulingRequest reservationReschedulingRequest = new(Enumeration.RequestState.Waiting, accommodationReservation, newStartingDate, newEndingDate);

            ReservationReschedulingRequestController.Save(reservationReschedulingRequest);
            MessageBox.Show("Successful");

            RemoveRedFromControls();
            RefreshReschedulingDataGrid();
        }
    }
}
