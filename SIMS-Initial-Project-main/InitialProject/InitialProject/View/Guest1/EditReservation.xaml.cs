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

namespace InitialProject.View
{
    public partial class EditReservation : Window
    {
        private readonly AccommodationReservationController AccommodationReservationController = new();

        public ObservableCollection<AccommodationReservation> ReservationsToShow { get; set; }

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
                this.Close();
            }

            RefreshDataGrid(allReservations);
        }

        private void RefreshDataGrid(List<AccommodationReservation> accommodationReservations)
        {
            ReservationsToShow = new ObservableCollection<AccommodationReservation>();
            ReservationsGrid.ItemsSource = ReservationsToShow;
            foreach (AccommodationReservation a in accommodationReservations)
            {
                ReservationsToShow.Add(a);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservation accommodationReservation = (AccommodationReservation)ReservationsGrid.SelectedItem;

            if (IsViolatingAnyUIControl(accommodationReservation)) return;

            if (!AccommodationReservationController.Delete(accommodationReservation))
                MessageBox.Show("You can't cancel that reservation");

            RefreshDataGrid(AccommodationReservationController.GetBy(User));
        }

        private bool IsViolatingAnyUIControl(AccommodationReservation accommodationReservation)
        {
            if(accommodationReservation == null)
            {
                MessageBox.Show("Please select a reservation you want to cancel");
                return true;
            }


            return false;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Guest1 guest1 = new(User);
            guest1.Show();

            Close();
        }
    }
}
