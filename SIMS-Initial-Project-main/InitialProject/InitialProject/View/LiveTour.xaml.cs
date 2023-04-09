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
using System.Windows.Shapes;
using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for LiveTour.xaml
    /// </summary>
    public partial class LiveTour : Window
    {
        public int TourId { get; set; }
        public TourKeyPointController TourKeyPointController { get; set; } = new TourKeyPointController();
        public TourController tourController { get; set; } = new TourController();
        public TourReservationController TourReservationController { get; set; } = new TourReservationController();
        public ObservableCollection<TourKeyPoint> keyPoints { get; set; } = new ObservableCollection<TourKeyPoint>();

        public ObservableCollection<TourReservation> Reservations{ get; set; } = new ObservableCollection<TourReservation>();
        private int ColNum { get; set; } = 5;
        public User LoggedInGuide { get; set; }
        private TourKeyPoint _lastCheckedKeyPoint { get; set; }
        


        public LiveTour(int tourId, User user)
        {
            TourId = tourId;
            LoggedInGuide = user;
            this.DataContext = this;
            InitializeComponent();
            Tour tour = tourController.GetById(tourId);
            List<TourReservation> reservations = TourReservationController.GetByTour(tour.Id);
            //keyPoints = new ObservableCollection<TourKeyPoint>(tour.TourKeyPoints.ToList());
            foreach (var keyPoint in tour.TourKeyPoints)
            {
                keyPoints.Add(keyPoint);
            }

            foreach (TourReservation reservation in reservations)
            {
                Reservations.Add(reservation);
            }
            
           
        }

        private void generateColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;

            if (propertyDescriptor.DisplayName == "Name")
            {
                if (ColNum == 2)
                {
                    e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }

            }
            else
            {
                e.Cancel = true;
                ColNum--;
            }

        }

        private void CheckKeyPoint(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            TourKeyPoint keyPoint = (TourKeyPoint)checkBox.DataContext;

            TourKeyPointController.Reach(keyPoint.Id, keyPoint.Type);

            _lastCheckedKeyPoint = keyPoint;

            if (keyPoint.Type == TourKeyPointType.End)
            {
                EndTour();
            }
        }
        private void EndTour()
        {
            tourController.SetStatus(TourId, TourStatus.Finished);
            ShowTours showTours = new ShowTours(LoggedInGuide);
            showTours.Show();
            Close();
        }


        private void EndFromButton(object sender, RoutedEventArgs e)
        {
            ShowTours showTours = new ShowTours(LoggedInGuide);
            showTours.Show();
            Close();
        }

        private void SetArrivalKeyPoint(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            TourReservation reservation = (TourReservation)checkBox.DataContext;
            TourReservationController.SetArrivalKeyPoint(_lastCheckedKeyPoint, reservation.Id);
        }
    }
}
