using InitialProject.Controller;
using InitialProject.Model;
using InitialProject.View;
using Microsoft.Win32;
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
using System.IO;

namespace InitialProject.View.Guest1
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
            RenovationSuggestionButton.IsEnabled = false;
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

        private void RemoveRedFromControls()
        {
            ReservationsGrid.ClearValue(Border.BorderThicknessProperty);
            ReservationsGrid.ClearValue(Border.BorderBrushProperty);

            TidinessComboBox.ClearValue(Border.BorderThicknessProperty);
            TidinessComboBox.ClearValue(Border.BorderBrushProperty);

            CorrectnessComboBox.ClearValue(Border.BorderThicknessProperty);
            CorrectnessComboBox.ClearValue(Border.BorderBrushProperty);

            ImagesTextBox.ClearValue(Border.BorderThicknessProperty);
            ImagesTextBox.ClearValue(Border.BorderBrushProperty);

            CommentTextBox.ClearValue(Border.BorderThicknessProperty);
            CommentTextBox.ClearValue(Border.BorderBrushProperty);
        }

        public void MakeControlRed<T>(T control) where T : Control
        {
            control.Focus();
            control.BorderBrush = new SolidColorBrush(Colors.Red);
            control.BorderThickness = new Thickness(2);
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

            RenovationSuggestionButton.IsEnabled = true;
            ReservationsGrid.IsEnabled = false;

            RemoveRedFromControls();
        }

        private void AddLink_Click(object sender, RoutedEventArgs e)
        {
            if (ImagesTextBox.Text.Equals(""))
            {
                MessageBox.Show("Please enter an image link before clicking the buttom");

                MakeControlRed(ImagesTextBox);

                return;
            }

            Images.Append(ImagesTextBox.Text);
            Images.Append('$');

            ImagesTextBox.Clear();
            MessageBox.Show("Succesfully added");

            RemoveRedFromControls();
        }

        private void RefreshDataGrid(List<AccommodationReservation> accommodationReservations)
        {
            ReservationsToShow = new ObservableCollection<AccommodationReservation>();
            ReservationsGrid.ItemsSource = ReservationsToShow;

            foreach (AccommodationReservation ar in accommodationReservations)
            {
                ReservationsToShow.Add(ar);
            }
        }

        private bool IsViolatingAnyUIControl(AccommodationReservation accommodationReservation)
        {
            if (TidinessComboBox.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a proper mark for tidiness");

                MakeControlRed(TidinessComboBox);

                return true;
            }

            if (CorrectnessComboBox.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a proper mark for owner's correctness");

                MakeControlRed(CorrectnessComboBox);

                return true;
            }

            if (accommodationReservation == null)
            {
                MessageBox.Show("Please select a reservation you want to rate");

                MakeControlRed(ReservationsGrid);

                return true;
            }

            if (!ValidateSelectedEndingDate(accommodationReservation))
            {
                MessageBox.Show("You can't rate it now");
                return true;
            }

            RemoveRedFromControls();
            return false;
        }

        private bool ValidateSelectedEndingDate(AccommodationReservation accommodationReservation)
        {
            return accommodationReservation.EndingDate.Day <= DateTime.Now.Day && accommodationReservation.EndingDate.Day + 5 >= DateTime.Now.Day;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Guest1Menu guest1Menu = new(User);
            guest1Menu.Show();

            Close();
        }

        private void SuggestionForReservation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservation accommodationReservation = (AccommodationReservation)ReservationsGrid.SelectedItem;
            if (accommodationReservation == null)
            {
                MessageBox.Show("Please select an accomodation first");

                MakeControlRed(ReservationsGrid);

                return;
            }

            RenovationRecommendation renovationRecommendation = new(accommodationReservation);
            renovationRecommendation.Show();

            Close();
        }

        private void AddPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;

                string destinationFolderPath = @"C:\\Users\\Luka stajic\\Documents\\Projekat SiMS-HCI\\HCI-SIMS\\SIMS-Initial-Project-main\\InitialProject\\InitialProject\\Resources\\Images\\Guest1";
                string destinationFilePath = System.IO.Path.Combine(destinationFolderPath, System.IO.Path.GetFileName(selectedImagePath));

                File.Copy(selectedImagePath, destinationFilePath, true);

                MessageBox.Show("Picture added successfully!");
            }
        }
    }
}
