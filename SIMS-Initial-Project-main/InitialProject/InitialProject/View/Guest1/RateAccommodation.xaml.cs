using InitialProject.Controller;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
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

    public partial class RateAccommodation : Window
    {
        private readonly AccommodationReservationController AccommodationReservationController = new();
        private readonly AccommodationReviewController AccommodationReviewController = new();

        public ObservableCollection<AccommodationReservation> ReservationsToShow { get; set; }

        public StringBuilder Images = new();

        private readonly User User;

        public RateAccommodation(User user)
        {
            InitializeComponent();
            InitializeTidinesComboBox();
            InitializeCorrectnessComboBox();
            User = user;

            RefreshDataGrid(AccommodationReservationController.GetBy(User));
        }

        private void InitializeTidinesComboBox()
        {
            TidinessComboBox.Items.Add("--Select--");
            TidinessComboBox.Items.Add("1");
            TidinessComboBox.Items.Add("2");
            TidinessComboBox.Items.Add("3");
            TidinessComboBox.Items.Add("4");
            TidinessComboBox.Items.Add("5");

            TidinessComboBox.SelectedIndex = 0;
        }

        private void InitializeCorrectnessComboBox()
        {
            CorrectnessComboBox.Items.Add("--Select--");
            CorrectnessComboBox.Items.Add("1");
            CorrectnessComboBox.Items.Add("2");
            CorrectnessComboBox.Items.Add("3");
            CorrectnessComboBox.Items.Add("4");
            CorrectnessComboBox.Items.Add("5");

            CorrectnessComboBox.SelectedIndex = 0;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservation accommodationReservation = (AccommodationReservation)ReservationsGrid.SelectedItem;

            if (IsViolatingAnyUIControl(accommodationReservation))
            {
                return;
            }

            int tidiness = int.Parse(TidinessComboBox.SelectedIndex.ToString());
            int correctness = int.Parse(CorrectnessComboBox.SelectedIndex.ToString());

            AccommodationReview accommodationReview = new(tidiness, correctness, CommentTextBox.Text, Images.ToString(), accommodationReservation);

            AccommodationReviewController.Save(accommodationReview);
            MessageBox.Show("Everything was succesfull");
        }

        private void AddLink_Click(object sender, RoutedEventArgs e)
        {
            if (ImagesTextBox.Text.Equals(""))
            {
                MessageBox.Show("Please enter an image link before clicking the buttom");
                return;
            }

            Images.Append(ImagesTextBox.Text);
            Images.Append('$');

            ImagesTextBox.Clear();
            MessageBox.Show("Succesfully added");
        }

        private void RefreshDataGrid(List<AccommodationReservation> accommodationReservations)
        {
            ReservationsToShow = new ObservableCollection<AccommodationReservation>();
            ReservationsGrid.ItemsSource = ReservationsToShow;

            foreach(AccommodationReservation ar in accommodationReservations)
            {
                ReservationsToShow.Add(ar);
            }
        }

        private bool IsViolatingAnyUIControl(AccommodationReservation accommodationReservation)
        {
            if (TidinessComboBox.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a proper mark for tidiness");
                return true;
            }

            if (CorrectnessComboBox.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a proper mark for owner's correctness");
                return true;
            }

            if (accommodationReservation == null)
            {
                MessageBox.Show("Please select a reservation you want to rate");
                return true;
            }

            if (!ValidateSelectedEndingDate(accommodationReservation))
            {
                MessageBox.Show("You can't rate it now");
                return true;
            }

            return false;
        }

        private bool ValidateSelectedEndingDate(AccommodationReservation accommodationReservation)
        {
            return accommodationReservation.EndingDate.Day <= DateTime.Now.Day && accommodationReservation.EndingDate.Day + 5 >= DateTime.Now.Day;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Guest1 guest1 = new(User);
            guest1.Show();

            Close();
        }
    }
}
