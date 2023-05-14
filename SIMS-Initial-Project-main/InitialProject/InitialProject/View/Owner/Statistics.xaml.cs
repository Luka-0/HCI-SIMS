using InitialProject.Controller;
using InitialProject.Dto;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Page
    {
        AccommodationController AccommodationController = new AccommodationController();
        private string Username;

        public Statistics(string ownerUsername)
        {
            Username = ownerUsername;

            InitializeComponent();
            InitializeOwnersAccommoadtions();
        }

        public void InitializeOwnersAccommoadtions()
        {

            accommodations.ItemsSource = AccommodationController.GetAllBy(Username);
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            annualStatistics.ItemsSource = new List<InitialProject.Dto.Statistics>();
            monthlyStatistics.ItemsSource= new List<InitialProject.Dto.Statistics>();
            yearOccupancy.Text = "";
            monthOccupacy.Text = "";

           Accommodation selectedAccommodation = accommodations.SelectedItem as Accommodation;

            annualStatistics.ItemsSource = AccommodationController.GetAnnualStatisticsBy(selectedAccommodation);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Accommodation selectedAccommodation = accommodations.SelectedItem as Accommodation;

            InitialProject.Dto.Statistics selectedAnnualStatistics = annualStatistics.SelectedItem as InitialProject.Dto.Statistics;

            int selectedYear = Convert.ToInt32(selectedAnnualStatistics.TimePeriod);

            monthlyStatistics.ItemsSource = AccommodationController.GetMonthsStatisticsBy(selectedYear, selectedAccommodation);

            DisplayMaxOccupancy();
        }

        private void DisplayMaxOccupancy() {

            double maxOccupancyYear = 0, maxOccupancyMonth=0;
            string month="", year="";

            List<InitialProject.Dto.Statistics> monthStatistics = new List<Dto.Statistics>();
            List<InitialProject.Dto.Statistics> yearStatistics = new List<Dto.Statistics>();

            Accommodation selectedAccommodation = accommodations.SelectedItem as Accommodation;
            InitialProject.Dto.Statistics selectedAnnualStatistics = annualStatistics.SelectedItem as InitialProject.Dto.Statistics;

            int selectedYear = Convert.ToInt32(selectedAnnualStatistics.TimePeriod);

            yearStatistics = AccommodationController.GetAnnualStatisticsBy(selectedAccommodation);
            monthStatistics = AccommodationController.GetMonthsStatisticsBy(selectedYear, selectedAccommodation);


            foreach (var statistics in yearStatistics) {

                if (statistics.Occupancy > maxOccupancyYear) {

                    maxOccupancyYear = statistics.Occupancy;
                    year = statistics.TimePeriod;
                }
            }

            maxOccupancyMonth = 0;
          
            foreach (var statistics in monthStatistics)
            {

                if (statistics.Occupancy > maxOccupancyMonth)
                {

                    maxOccupancyMonth = statistics.Occupancy;
                    month = statistics.TimePeriod;
                }
            }

            yearOccupancy.Text = "  Max occupancy is " + maxOccupancyYear.ToString() + " %, in year: " + year;
            monthOccupacy.Text = "  Max occupancy is " + maxOccupancyMonth.ToString() + " %, in month: " + month;

        }
    }
}
