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
    /// Interaction logic for RateGuestsView.xaml
    /// </summary>
    public partial class GuestReview : Page
    {

        GuestReviewController GuestReviewController =  new GuestReviewController();

        public string OwnerUsername;

        
        public GuestReview(string ownerUsername)
        {
            InitializeComponent();

            this.OwnerUsername = ownerUsername;

            List<ExpiredReservationDto> records = new List<ExpiredReservationDto>();
            records = GuestReviewController.LoadExpiredReservations(OwnerUsername);
            ExpiredReservations.ItemsSource = records;
 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ExpiredReservationDto record = ExpiredReservations.SelectedItem as ExpiredReservationDto;
            int tidinessGrade = Int32.Parse(tidiness.Text);
            int obedienceGrade = Int32.Parse(obedience.Text);

            NewGuestReviewDto guestReviewDto = new NewGuestReviewDto(tidinessGrade, obedienceGrade, comment.Text, record.ReservationId);
            GuestReviewController.Save(guestReviewDto);
        }

        private void obedience_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.Parse(obedience.Text) > 5 || Int32.Parse(obedience.Text) < 1) {

                MessageBox.Show("Unsupported grade value!");
            }
        }

        private void tidiness_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.Parse(tidiness.Text) > 5 || Int32.Parse(tidiness.Text) < 1)
            {

                MessageBox.Show("Unsupported grade value!");
            }
        }
    }
}
