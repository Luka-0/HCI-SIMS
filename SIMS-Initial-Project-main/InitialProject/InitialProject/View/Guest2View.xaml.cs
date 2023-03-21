using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// <summary>
    /// Interaction logic for Guest2View.xaml
    /// </summary>
    public partial class Guest2View : Window
    {
        TourController tourController = new TourController();   

        TourService TourService = new TourService();
        TourReservationControler reservationControler = new TourReservationControler(); 

        public List<GetTourDto> Tours
        {
            get;
            set;
        }

        public List<TourReservation> Reservations
        {
            get;
            set;
        }

        public Guest2View()
        {
            InitializeComponent();
            ShowAllTours();
           // ShowReservations();

        }

        public void ShowReservations()
        {
            Reservations = reservationControler.GetAll();
            TourShowGrid.ItemsSource = Tours;
        }

        public void ShowAllTours()
        {
          //  this.DataContext = Tours;
            Tours = tourController.GetAll();
            TourShowGrid.ItemsSource = Tours;
        }

        private void mojButton_Click(object sender, RoutedEventArgs e)
        {
            
            if(Tours.Count > 0)
            {
                MessageBox.Show("Nisam prazna");
            }
            else
            {
                MessageBox.Show("Prazna sam");
            }
            String s = "";


            foreach (GetTourDto t in Tours)
            {
                s += t.ToString();
                
            }
            MessageBox.Show(s);
        }

        private void ShowByLocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string city = inputField.Text;
                string country = inputField2.Text;

                Location location = LocationService.getBy(city, country);

                Tours = tourController.GetBy(location);
                TourShowGrid.ItemsSource = Tours;
            }
            catch 
            {
                MessageBox.Show("Greska");
            }
        }

        private void ShowByLanguagee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string language = inputField.Text;
                Tours = tourController.GetBy(language);
                TourShowGrid.ItemsSource = Tours;
            }
            catch
            {
                MessageBox.Show("Greska");
            }
        }

        private void ShowByLenght_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string duration1 = inputField.Text;
                TimeOnly duration;
                TimeOnly.TryParse(inputField.Text, out duration);
                MessageBox.Show(duration.ToString());
                Tours = tourController.GetBy(duration);
                TourShowGrid.ItemsSource = Tours;
            }
            catch
            {
                MessageBox.Show("Greska");
            }
        }

        private void ShowByGuestNumber_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int guestNumber = Int32.Parse(inputField.Text);
                Tours = tourController.GetBy(guestNumber);
                TourShowGrid.ItemsSource = Tours;
            }
            catch
            {
                MessageBox.Show("Greska");
            }
        }
    }
}
