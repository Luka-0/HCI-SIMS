using InitialProject.Controller;
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
        private readonly AccommodationReservationController AccommodationReservationController = new();
        private readonly AccommodationController AccommodationController = new();
        private readonly LocationController LocationController = new();

        public ObservableCollection<Accommodation> AccommodationsToShow { get; set; }
        private List<StartEndDateDto> DatesToChose { get; set; }
        private User User { get; set; }
        
        public AccommodationReservate(User user)
        {
            InitializeComponent();
            InitializeFilterComboBox();
            InitializeAccommodationTypeComboBox();
            InitializeCountryComboBox();
            User = user;
            DataContext = this;

            List<Accommodation> allAccommodations = AccommodationController.GetAll();
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

        private void InitializeCountryComboBox()
        {
            CountryComboBox.Items.Add("--Chose--");

            List<Location> countries = LocationController.GetAllDistinctByCountry();
            foreach(Location location in countries)
            {
                CountryComboBox.Items.Add(location.Country.ToString());
            }

            CountryComboBox.SelectedIndex = 0;
        }

        private void InitializeCityComboBox(object sender, MouseEventArgs e)
        {
            if (CountryComboBox.SelectedIndex == 0) return;

            CityComboBox.Items.Clear();

            List<Location> locations = LocationController.GetByCountry(CountryComboBox.SelectedItem.ToString());
            foreach (Location location in locations)
            {
                CityComboBox.Items.Add(location.City);
            }
        }

        private void InitializeDatesComboBox()
        {
            OfferedDatesCB.Items.Clear();

            foreach (StartEndDateDto t in DatesToChose)
            {
                OfferedDatesCB.Items.Add(t.StartingDate.Date.ToString() + t.EndingDate.Date.ToString());
            }

            OfferedDatesCB.SelectedIndex = 0;
        }

        private void GenerateDates_Click(object sender, RoutedEventArgs e)
        {
            int daysToStay = int.Parse(ReservatingDaysTB.Text);
            Accommodation accommodation = (Accommodation)AccommodationsGrid.SelectedItem;
            DateTime startDate = StartingDatePicker.SelectedDate.Value;
            DateTime endDate = EndingDatePicker.SelectedDate.Value;

            if (IsViolatingAnyUIControl(startDate, endDate, accommodation)) return;

            DatesToChose = AccommodationReservationController.GetAvailableDates(accommodation, startDate, endDate, daysToStay);
            if (DatesToChose == null)
            {
                DatesToChose = AccommodationReservationController.FindOtherDates(endDate, accommodation, daysToStay);
            }

            InitializeDatesComboBox();
        }

        private void CreateReservation_Click(object sender, RoutedEventArgs e)
        {
            int guestNumber = int.Parse(GuestNumberTB.Text);
            int daysToStay = int.Parse(ReservatingDaysTB.Text);
            Accommodation accommodation = (Accommodation)AccommodationsGrid.SelectedItem;
            DateTime startDate = DatesToChose[OfferedDatesCB.SelectedIndex].StartingDate;
            DateTime endDate = DatesToChose[OfferedDatesCB.SelectedIndex].EndingDate;

            if (IsViolatingAnyUIControl(startDate, endDate, accommodation, guestNumber)) return;

            if (daysToStay < accommodation.MinimumReservationDays)
            {
                MessageBox.Show("Entered days to stay are bellow the threshold for selected accommodation");
                return;
            }

            if (AccommodationReservationController.CreateReservation(accommodation, startDate, endDate, guestNumber, User))
            {
                MessageBox.Show("Succesfully saved");
                return;
            }
            MessageBox.Show("Saving was UNsuccesful");

        }

        private bool ValidateSelectedDates()
        {
            if (StartingDatePicker.Text.ToString().Equals(""))
            {
                MessageBox.Show("Please select a starting date");
                return false;
            }

            if (EndingDatePicker.Text.ToString().Equals(""))
            {
                MessageBox.Show("Please select an ending date");
                return false;
            }

            return true;
        }

        private bool IsViolatingAnyUIControl(DateTime startDate, DateTime endDate, Accommodation accommodation, int guestNumber=1)
        {
            if (!ValidateSelectedDates())
            {
                return true;
            }

            if (startDate > endDate)
            {
                MessageBox.Show("Selected starting date is after ending date, please select valid dates");
                return true;
            }

            if (accommodation == null)
            {
                MessageBox.Show("Please select an accommodation");
                return true;
            }

            if (guestNumber <= 0)
            {
                MessageBox.Show("Please enter a proper guest number");
                return true;

            }

            return false;
        }

        private void RefreshDataGrid(List<Accommodation> accommodations)
        {
            AccommodationsToShow = new ObservableCollection<Accommodation>();   
            AccommodationsGrid.ItemsSource = AccommodationsToShow;

            var sortedAccommodations = accommodations.OrderBy(t => t.Class);

            foreach (Accommodation a in sortedAccommodations)
            {
                AccommodationsToShow.Add(a);
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
                    if(CountryComboBox.SelectedIndex == 0)
                    {
                        MessageBox.Show("Please select a proper location");
                        return;
                    }

                    string city = ExtractCity(CountryComboBox.SelectedItem.ToString());
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
                    List<Accommodation> accommodations = AccommodationController.GetAll();
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
            List<Accommodation> accommodations = AccommodationController.GetBy(name);
            if (accommodations.Count == 0)
            {
                MessageBox.Show("There are currently no Accommodations with that name");
                return;
            }

            RefreshDataGrid(accommodations);
        }
        private void ApplyByType(AccommodationType accommodationType)
        {
            List<Accommodation> accommodations = AccommodationController.GetBy(accommodationType);
            if (accommodations.Count == 0)
            {
                MessageBox.Show("There are currently no Accommodations with that type");
                return;
            }

            RefreshDataGrid(accommodations);
        }
        private void ApplyByGuestNumber(int guestNumber)
        {
            List<Accommodation> accommodations = AccommodationController.GetByGuestNumber(guestNumber);
            if (accommodations.Count == 0)
            {
                MessageBox.Show("There are currently no Accommodations that can accept that many guests");
                return;
            }

            RefreshDataGrid(accommodations);
        }
        private void ApplyByReservationDays(int reservationDays)
        {
            List<Accommodation> accommodations = AccommodationController.GetByReservationDays(reservationDays);
            if (accommodations.Count == 0)
            {
                MessageBox.Show("There are currently no Accommodations that can be reservated that shortly");
                return;
            }

            RefreshDataGrid(accommodations);
        }
        private void ApplyByCity(string city)
        {
            List<Accommodation> accommodations = AccommodationController.GetByCity(city);
            if (accommodations.Count == 0)
            {
                MessageBox.Show("There are currently no Accommodations that are in selected location");
                return;
            }

            RefreshDataGrid(accommodations);

        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Guest1 guest1 = new(User);
            guest1.Show();

            Close();
        }

    }
}
