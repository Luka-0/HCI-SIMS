using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
using System.Xml.Linq;

namespace InitialProject.View
{
    public partial class AccommodationReservate : Window
    {
        public ObservableCollection<Accommodation> accommodationsToShow { get; set; }
        private User User { get; set; }
        
        public AccommodationReservate(User user)
        {
            InitializeComponent();
            InitializeFilterComboBox();
            InitializeAccommodationTypeComboBox();
            InitializeLocationComboBox();
            User = user;
            DataContext = this;

            List<Accommodation> allAccommodations = AccommodationRepository.GetAll();
            if(allAccommodations.Count == 0)
            {
                MessageBox.Show("There are currently no Accommodations to look at :(");
                this.Close();
            }

            RefreshDataGrid(allAccommodations);
        }

        private void InitializeFilterComboBox()
        {
            FilterComboBox.Items.Add("--Select--");             //0
            FilterComboBox.Items.Add("Name");                   //1
            FilterComboBox.Items.Add("Location");               //2
            FilterComboBox.Items.Add("Accommodation type");     //3
            FilterComboBox.Items.Add("Guest number");           //4
            FilterComboBox.Items.Add("Days for reservation");   //5
            FilterComboBox.Items.Add("Reset");                  //6

            FilterComboBox.SelectedIndex = 0;
        }

        private void InitializeAccommodationTypeComboBox()
        {
            AccommodationTypeComboBox.Items.Add("Apartment");   //0
            AccommodationTypeComboBox.Items.Add("House");       //1
            AccommodationTypeComboBox.Items.Add("Cottage");     //2

            AccommodationTypeComboBox.SelectedIndex = 0;

        }

        private void InitializeLocationComboBox()
        {
            LocationCBFilter.Items.Add("--Chose--");

            List<Location> locations = LocationRepository.GetAll();
            foreach(Location location in locations)
            { 
                LocationCBFilter.Items.Add(location.Country.ToString() + "-" +  location.City.ToString());
            }

            LocationCBFilter.SelectedIndex = 0;
        }

        private void ReservateAccommodation_Click(object sender, RoutedEventArgs e)
        {

            string startingDate = StartingDatePicker.Text.ToString();
            if(startingDate.Equals(""))
            {
                MessageBox.Show("Please select a starting date");
                return;

            }

            string endingDate = EndingDatePicker.Text.ToString();
            if (endingDate.Equals(""))
            {
                MessageBox.Show("Please select an ending date");
                return;

            }

            int guestNumber = int.Parse(GuestNumberTB.Text);
            if(guestNumber <= 0)
            {
                MessageBox.Show("Please enter a proper guest number");
                return;

            }

            Accommodation accommodation = (Accommodation)AccommodationsGrid.SelectedItem;
            if(accommodation == null)
            {
                MessageBox.Show("Please select an accommodation");
                return;
            }

            DateTime startDate = StartingDatePicker.SelectedDate.Value;
            DateTime endDate = EndingDatePicker.SelectedDate.Value;

            if(startDate > endDate)
            {
                MessageBox.Show("Selected starting date is after ending date, please select valid dates");
                return;
            }

            if (!AccommodationReservationService.Reservate(accommodation, User, guestNumber, startDate, endDate))
            {
                MessageBox.Show("Reservation was UNsuccessful");
                return;
            }
            MessageBox.Show("Reservation was successful");

        }

        private void RefreshDataGrid(List<Accommodation> accommodations)
        {
            accommodationsToShow = new ObservableCollection<Accommodation>();   
            AccommodationsGrid.ItemsSource = accommodationsToShow;
            foreach (Accommodation a in accommodations)
            {
                accommodationsToShow.Add(a);
            }
        }

        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            switch (FilterComboBox.SelectedIndex)
            {
                case 1:
                    ApplyByName(NameTextBox.Text.ToString());
                    break;
                case 2:
                    if(LocationCBFilter.SelectedIndex == 0)
                    {
                        MessageBox.Show("Please select a proper location");
                        return;
                    }

                    string city = ExtractCity(LocationCBFilter.SelectedItem.ToString());
                    ApplyByCity(city);

                    break;
                case 3:
                    ApplyByType((AccommodationType)AccommodationTypeComboBox.SelectedIndex);

                    break;
                case 4:
                    ApplyByGuestNumber(int.Parse(GuestNumberTBFilter.Text.ToString()));         //TODO proveriti da li je ovde potrebno ToString()

                    break;
                case 5:
                    ApplyByReservationDays(int.Parse(GuestNumberTBFilter.Text.ToString()));

                    break;
                case 6:
                {
                    List<Accommodation> accommodations = AccommodationRepository.GetAll();
                    RefreshDataGrid(accommodations);

                    break;
                }
                default:
                    MessageBox.Show("Please select a proper filter type");
                    break;
            }


        }

        private static string ExtractCity(string location)
        {
            string[] separeted = location.Split('-');
            return separeted[1];
        }

        private void ApplyByName(string name)
        {
            List<Accommodation> accommodations = AccommodationRepository.GetBy(name);
            if (accommodations.Count == 0)
            {
                MessageBox.Show("There are currently no Accommodations with that name");
                return;
            }

            RefreshDataGrid(accommodations);
        }
        private void ApplyByType(AccommodationType accommodationType)
        {
            List<Accommodation> accommodations = AccommodationRepository.GetBy(accommodationType);
            if (accommodations.Count == 0)
            {
                MessageBox.Show("There are currently no Accommodations with that type");
                return;
            }

            RefreshDataGrid(accommodations);
        }
        private void ApplyByGuestNumber(int guestNumber)
        {
            List<Accommodation> accommodations = AccommodationRepository.GetByGuestNumber(guestNumber);
            if (accommodations.Count == 0)
            {
                MessageBox.Show("There are currently no Accommodations that can accept that many guests");
                return;
            }

            RefreshDataGrid(accommodations);
        }
        private void ApplyByReservationDays(int reservationDays)
        {
            List<Accommodation> accommodations = AccommodationRepository.GetByReservationDays(reservationDays);
            if (accommodations.Count == 0)
            {
                MessageBox.Show("There are currently no Accommodations that can be reservated that shortly");
                return;
            }

            RefreshDataGrid(accommodations);
        }
        private void ApplyByCity(string city)
        {
            List<Accommodation> accommodations = AccommodationRepository.GetByCity(city);
            if (accommodations.Count == 0)
            {
                MessageBox.Show("There are currently no Accommodations that are in selected location");
                return;
            }

            RefreshDataGrid(accommodations);

        }
    }
}
