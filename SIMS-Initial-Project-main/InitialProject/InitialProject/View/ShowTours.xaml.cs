using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
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
using System.Xml.Linq;
using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Model;
using InitialProject.View;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for ShowTours.xaml
    /// </summary>
    /// 
    public partial class ShowTours : Window
    {
        public ObservableCollection<TourBasicInfoDto> BasicTours { get; set; }
        public TourController tourController { get; set; } = new TourController();
        public TourKeyPointController tourKeyPointController { get; set; } = new TourKeyPointController();
        private int ColNum{ get; set; } = 0;
        public User LoggedInGuide { get; set; }


        public ShowTours(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            LoggedInGuide = user;
            BasicTours = new ObservableCollection<TourBasicInfoDto>();
            List<TourBasicInfoDto> basicTours = getUsersTours();
            //List<TourBasicInfoDto> basicTours = tourController.getAllBasicInfo();
            foreach (TourBasicInfoDto tour in basicTours)
            {
                BasicTours.Add(tour);    
            }


        }

        private List<TourBasicInfoDto> getUsersTours()
        {
            List<TourBasicInfoDto> tours = tourController.GetTodays().Where(t => t.GuideId == LoggedInGuide.Id).ToList();

            return tours;
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

        private void StartTour(object sender, RoutedEventArgs e)
        {
        
            TourBasicInfoDto selectedTour= (TourBasicInfoDto)dataGridTours.SelectedItem;

            tourController.SetStatus(selectedTour.id, TourStatus.Started);
            tourKeyPointController.StartTour(selectedTour.id);
            LiveTour liveTour= new LiveTour(selectedTour.id, LoggedInGuide);

            liveTour.Show();
            Close();
        }

        private void ShowHistory(object sender, RoutedEventArgs e)
        {
            GuideHistory guideHistory = new GuideHistory(LoggedInGuide);
            guideHistory.Show();

        }




    }
}
