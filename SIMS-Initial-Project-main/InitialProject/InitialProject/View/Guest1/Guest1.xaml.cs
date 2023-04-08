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
using System.Windows.Shapes;

namespace InitialProject.View
{
    public partial class Guest1 : Window
    {
        private User User { get; set; }

        public Guest1(User user)
        {
            InitializeComponent();
            User = user;
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

        private void EditAccommodation_Click(object sender, RoutedEventArgs e)
        {
            EditReservation editReservation = new(User);
            editReservation.Show();

            Close();
        }
    }
}
