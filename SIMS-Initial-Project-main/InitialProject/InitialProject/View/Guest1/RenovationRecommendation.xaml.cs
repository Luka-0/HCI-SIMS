using InitialProject.Controller;
using InitialProject.Model;
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
    public partial class RenovationRecommendation : Window
    {
        private readonly RenovationSuggestionController RenovationSuggestionController = new();
        private readonly AccommodationReservationController AccommodationReservationController = new();

        public ObservableCollection<AccommodationReservation> ReservationToShow { get; set; } = new();

        public RenovationRecommendation(AccommodationReservation accommodationReservation)
        {
            InitializeComponent();
            InitializeCB();

            AccommodationReservation tmp = AccommodationReservationController.GetByIdWithInclude(accommodationReservation.Id);
            ReservationToShow.Add(tmp);
            ReservationGrid.ItemsSource = ReservationToShow;
        }

        private void InitializeCB()
        {
            RatingCB.Items.Add("--Select--");
            RatingCB.Items.Add("1");
            RatingCB.Items.Add("2");
            RatingCB.Items.Add("3");
            RatingCB.Items.Add("4");
            RatingCB.Items.Add("5");

            RatingCB.SelectedIndex = 0;
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsViolatingAnyUIControl()) return;

            RenovationSuggestion renovationSuggestion = new (int.Parse(RatingCB.SelectedItem.ToString()), CommentTB.Text, ReservationToShow.First());
            RenovationSuggestionController.Save(renovationSuggestion);

            MessageBox.Show("Successfull");

            Guest1Menu guest1Menu = new(ReservationToShow.First().Guest);
            guest1Menu.Show();

            Close();
        }

        private bool IsViolatingAnyUIControl()
        {
            if(RatingCB.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a proper rating");
                return false;
            }

            return true;
        }
    }
}
