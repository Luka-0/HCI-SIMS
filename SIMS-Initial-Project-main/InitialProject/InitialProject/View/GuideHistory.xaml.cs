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

        public ObservableCollection<TourBasicInfoDto> BasicTours { get; set; } =
            new ObservableCollection<TourBasicInfoDto>();
        public TourController tourController { get; set; } = new TourController();
        private int ColNum { get; set; } = 0;

        public GuideHistory(User user)
        {
            LoggedInGuide = user;
            InitializeComponent();
            this.DataContext = this;
            GetFinishedTours(LoggedInGuide.Id);
        }

        private void GetFinishedTours(int guideId)
        {
            List<TourBasicInfoDto> tours = tourController.GetFinished(guideId);


            foreach (TourBasicInfoDto tour in tours)
            {
                BasicTours.Add(tour);
            }
        }
        private void generateColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
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

        
        private void ShowReviews(object sender, RoutedEventArgs e)
        {
            TourBasicInfoDto selectedTour = (TourBasicInfoDto)DataGridTours.SelectedItem;

            TourReviews tourReviews = new TourReviews(LoggedInGuide, selectedTour.id);

            tourReviews.Show();
            Close();
        }
    }
}
