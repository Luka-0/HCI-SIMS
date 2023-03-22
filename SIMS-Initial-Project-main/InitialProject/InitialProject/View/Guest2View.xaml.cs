﻿using InitialProject.Controller;
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

        public ObservableCollection<Tour> toursToShow { get; set; }

        public List<Tour> Tours
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
          //  ShowReservations();
            this.ResizeMode = ResizeMode.NoResize;
            // ShowReservations();

        }

        public void ShowReservations()
        {
            Reservations = reservationControler.GetAll();
            foreach(TourReservation tour in Reservations)
            {
                MessageBox.Show(tour.ToString());
            }
        }

        private void RefreshDataGrid(List<Tour> tours)
        {
            toursToShow = new ObservableCollection<Tour>();
            TourShowGrid.ItemsSource = toursToShow;
            foreach (Tour t in tours)
            {
                toursToShow.Add(t);
            }
        }

        public void ShowAllTours()
        {
            this.DataContext = this;
            List<Tour> allTours = TourService.GetAll();
            RefreshDataGrid(allTours);
        }

        private void ShowByLocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string city = inputField.Text;
                string country = inputField2.Text;

                Location location = LocationService.getBy(country, city);

                Tours = TourService.GetBy(location);
                RefreshDataGrid(Tours);
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
                Tours = TourService.GetBy(language);
                RefreshDataGrid(Tours);
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
                TimeSpan duration;
                TimeSpan.TryParse(inputField.Text, out duration);
                MessageBox.Show(duration.ToString());
                Tours = TourService.GetBy(duration);
                RefreshDataGrid(Tours);
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
                Tours = TourService.GetBy(guestNumber);
                RefreshDataGrid(Tours);
            }
            catch
            {
                MessageBox.Show("Greska");
            }
        }

        private void ReserveTourButton_Click(object sender, RoutedEventArgs e)
        {
           try
            {
                Tour tour = (Tour)TourShowGrid.SelectedItem;
                int guestNumber = int.Parse(SelectedGuestNumber.Text);

                TourReservationResponseDto response = reservationControler.Reserve(tour, guestNumber);

                if (response.IsFull && response.AvaliableSpace == 0)        //nemoguce rezervisati
                {
                    List<Tour> newTours = TourService.GetBy(tour.Location);
                    MessageBox.Show("Tura je popunjena. Pogledajte ture sa iste lokacije.");
                    RefreshDataGrid(newTours);
                }else
                    if(response.AvaliableSpace < guestNumber)
                {
                    MessageBox.Show("Nema mesta za uneti broj gostiju. Smanjite broj i pokusajte ponovo!");
                }

                if(!response.IsFull)
                {
                    TourReservation newTourReservation = new TourReservation();
                    reservationControler.Save(newTourReservation, tour, UserRepository.GetUser("Perica"), guestNumber);
                    MessageBox.Show("Uspesno sacuvana rezervacija!");
                    ShowAllTours();
                }

                FreeSpacesLabel.Content = "Broj slobodnih mesta: " + response.AvaliableSpace.ToString();


                MessageBox.Show(response.ToString());
            }
            catch
            {
                MessageBox.Show("Error");
            }

        }

        private void CancelReservationButton_Click(object sender, RoutedEventArgs e)
        {
            ShowAllTours();
        }
    }
}
