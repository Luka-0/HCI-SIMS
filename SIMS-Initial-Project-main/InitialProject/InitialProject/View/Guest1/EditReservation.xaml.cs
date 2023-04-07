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
        private AccommodationReservationController AccommodationReservationController = new();

        public ObservableCollection<AccommodationReservation> reservationsToShow { get; set; }

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
            reservationsToShow = new ObservableCollection<AccommodationReservation>();
            ReservationsGrid.ItemsSource = reservationsToShow;
            foreach (AccommodationReservation a in accommodationReservations)
            {
                reservationsToShow.Add(a);
            }
        }
    }
}
