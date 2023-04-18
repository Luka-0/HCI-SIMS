using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for GuideHistory.xaml
    /// </summary>
    public partial class GuideHistory : Window
    {
        private User LoggedInGuide { get; set;}

        public ObservableCollection<TourBasicInfoDto> BasicTours { get; set; } = new ObservableCollection<TourBasicInfoDto>();
        public TourController TourController { get; set; } = new TourController();
        private int ColNum { get; set; } = 0;
        public ObservableCollection<string> TourYears { get; set; } = new ObservableCollection<string>();
        public GuideHistory(User user)
        {
            LoggedInGuide = user;
            InitializeComponent();
            this.DataContext = this;
            List<TourBasicInfoDto> tours =  GetFinishedTours(LoggedInGuide.Id);
            GetTourYears(tours);

        }

        private void GetTourYears(List<TourBasicInfoDto> tours)
        {
            foreach (TourBasicInfoDto tour in tours)
            {
                string year = tour.StartDateAndTime.Date.Year.ToString();
                if (!TourYears.Contains(year))
                {
                    TourYears.Add(year);
                }    
            }
            TourYears.Add("All time");
        }

        private List<TourBasicInfoDto> GetFinishedTours(int guideId)
        {
            List<TourBasicInfoDto> tours = TourController.GetByStatus(guideId, TourStatus.Finished);

            foreach (TourBasicInfoDto tour in tours)
            {
                BasicTours.Add(tour);
            }

            return tours;
        }
        /*private void generateColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "id" || propertyDescriptor.DisplayName == "GuideId")
            {
                e.Cancel = true;
            }
            ColNum++;
            if (ColNum == 7)
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        */

        
        private void ShowReviews(object sender, RoutedEventArgs e)
        {
            TourBasicInfoDto selectedTour = (TourBasicInfoDto)DataGridTours.SelectedItem;

            TourReviews tourReviews = new TourReviews(LoggedInGuide, selectedTour.id);

            tourReviews.Show();
            Close();
        }

        private void ShowStatistics(object sender, RoutedEventArgs e)
        {
            TourBasicInfoDto selectedTour = (TourBasicInfoDto)DataGridTours.SelectedItem;
            TourStatistics tourStatistics= new TourStatistics(selectedTour.id, selectedTour.Name);
            
            tourStatistics.Show();
        }

        private TourGuestsDto GetBestTour()
        {
            string time = ComboBox1.Text;
            TourGuestsDto dto= TourController.GetMostVisitedTour(LoggedInGuide.Id, time);

            return dto;

        }

        private void ShowBestStatistics(object sender, RoutedEventArgs e)
        {
            //TourStatistics tourStatistics = new TourStatistics(tour.Id , tour.Name);
            //tourStatistics.Show();
            BestTourStatistics tourStatistics = new BestTourStatistics(GetBestTour());
            tourStatistics.Show();


        }
    }
}
