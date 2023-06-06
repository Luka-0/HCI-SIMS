using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.View.Owner;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InitialProject.View.Guest1
{
    public partial class AccommodationReservate : System.Windows.Window
    {
        private readonly AccommodationReservationController AccommodationReservationController = new();
        private readonly AccommodationController AccommodationController = new();
        private readonly LocationController LocationController = new();
        private readonly UserController UserController = new UserController();

        public ObservableCollection<Accommodation> AccommodationsToShow { get; set; }
        private List<StartEndDateDto> DatesToChose { get; set; }
        private User User { get; set; }

        //kt3 - vlasnik - deo funkcionalnosti zakazivanja renoviranja
        private readonly RenovationController RenovationController = new RenovationController();
        
        public AccommodationReservate(User user)
        {
            InitializeComponent();
            InitializeFilterComboBox();
            InitializeAccommodationTypeComboBox();
            InitializeCountryComboBox();
            User = user;
            DataContext = this;

            List<Accommodation> allAccommodations = AccommodationController.GetAll();
            DatesToChose = new List<StartEndDateDto>();

            if (allAccommodations.Count == 0)
            {
                MessageBox.Show("There are currently no Accommodations to look at :(");
                Close();
            }

            RefreshDataGrid(allAccommodations);

            NameTextBox.IsEnabled = false;
            CountryComboBox.IsEnabled = false;
            CityComboBox.IsEnabled = false;
            AccommodationTypeComboBox.IsEnabled = false;
            GuestNumberTBFilter.IsEnabled = false;
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

        private void CheckSuperTitle()
        {
            if (User.SuperTitle)
            {
                if(User.BonusPoints > 0)
                {
                    UserController.UpdateBy(User.Id, true, User.BonusPoints - 1);
                    MessageBox.Show("One bonus point was spent which leaves you with " + (User.BonusPoints - 1).ToString());
                }
            }
        }

        private void RemoveRedFromControls()
        {
            StartingDatePicker.ClearValue(Border.BorderThicknessProperty);
            StartingDatePicker.ClearValue(Border.BorderBrushProperty);

            EndingDatePicker.ClearValue(Border.BorderThicknessProperty);
            EndingDatePicker.ClearValue(Border.BorderBrushProperty);

            ReservatingDaysTB.ClearValue(Border.BorderThicknessProperty);
            ReservatingDaysTB.ClearValue(Border.BorderBrushProperty);

            GuestNumberTB.ClearValue(Border.BorderThicknessProperty);
            GuestNumberTB.ClearValue(Border.BorderBrushProperty);

            CityComboBox.ClearValue(Border.BorderThicknessProperty);
            CityComboBox.ClearValue(Border.BorderBrushProperty);
        }

        public void MakeControlRed<T>(T control) where T : Control
        {
            control.Focus();
            control.BorderBrush = new SolidColorBrush(Colors.Red);
            control.BorderThickness = new Thickness(2);
        }

        private void GenerateDates_Click(object sender, RoutedEventArgs e)
        {
            int daysToStay;
            try
            {
                daysToStay = int.Parse(ReservatingDaysTB.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a number");

                MakeControlRed(ReservatingDaysTB);

                return;
            }

            Accommodation accommodation = (Accommodation)AccommodationsGrid.SelectedItem;

            //dodatak
            List<StartEndDateDto> availableDates = new List<StartEndDateDto>();
            List<Renovation> existingRenovations = new List<Renovation>();


            if (StartingDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select starting date first");

                MakeControlRed(StartingDatePicker);

                return;
            }
            if (EndingDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select ending date first");

                MakeControlRed(EndingDatePicker);

                return;
            }

            DateTime startDate = StartingDatePicker.SelectedDate.Value;
            DateTime endDate = EndingDatePicker.SelectedDate.Value;

            if (IsViolatingAnyUIControl(startDate, endDate, accommodation)) return;

            availableDates = AccommodationReservationController.GetAvailableDates(accommodation, startDate, endDate, daysToStay);

            DatesToChose.Clear();
            if (availableDates != null) {

                //KT3 - Vlasnik: dodatno ogranicenje da predlozeni datumi nisu datumi zakazanog renoviranja
                foreach (StartEndDateDto t in availableDates)
                {
                    existingRenovations = RenovationController.GetAllBetweenBy(accommodation, t.StartingDate, t.EndingDate);

                    if (existingRenovations.Count == 0)
                    {
                        DatesToChose.Add(t);
                    }
                }
            }

            if (DatesToChose == null || DatesToChose.Count == 0)
            {
                DatesToChose = AccommodationReservationController.FindOtherDates(endDate, accommodation, daysToStay, 0);
            }

            InitializeDatesComboBox();
            RemoveRedFromControls();
        }

        private void CreateReservation_Click(object sender, RoutedEventArgs e)
        {
            if (OfferedDatesCB.Items.Count == 0) return;

            int guestNumber, daysToStay;

            try
            {
                guestNumber = int.Parse(GuestNumberTB.Text);
                
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a number");

                MakeControlRed(GuestNumberTB);

                return;
            }

            try
            {
                daysToStay = int.Parse(ReservatingDaysTB.Text);

            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a number");

                MakeControlRed(ReservatingDaysTB);

                return;
            }

            Accommodation accommodation = (Accommodation)AccommodationsGrid.SelectedItem;

            DateTime startDate = DatesToChose[OfferedDatesCB.SelectedIndex].StartingDate;
            DateTime endDate = DatesToChose[OfferedDatesCB.SelectedIndex].EndingDate;

            if (IsViolatingAnyUIControl(startDate, endDate, accommodation, guestNumber)) return;

            if (daysToStay < accommodation.MinimumReservationDays)
            {
                MessageBox.Show("Entered days to stay are bellow the threshold for selected accommodation");

                MakeControlRed(ReservatingDaysTB);

                return;
            }

            if (AccommodationReservationController.CreateReservation(accommodation, startDate, endDate, guestNumber, User))
            {
                MessageBox.Show("Succesfully saved");

                CheckSuperTitle();

                OfferedDatesCB.Items.Clear();

                RemoveRedFromControls();

                return;
            }

            MessageBox.Show("Saving was UNsuccesful");

        }

        private bool ValidateSelectedDates()
        {
            if (StartingDatePicker.Text.ToString().Equals(""))
            {
                MessageBox.Show("Please select a starting date");

                MakeControlRed(StartingDatePicker);

                return false;
            }

            if (EndingDatePicker.Text.ToString().Equals(""))
            {
                MessageBox.Show("Please select an ending date");

                MakeControlRed(EndingDatePicker);

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

                MakeControlRed(StartingDatePicker);

                return true;
            }

            if (accommodation == null)
            {
                MessageBox.Show("Please select an accommodation");

                MakeControlRed(AccommodationsGrid);

                return true;
            }

            if (guestNumber <= 0)
            {
                MessageBox.Show("Please enter a proper guest number");

                MakeControlRed(GuestNumberTB);

                return true;

            }

            RemoveRedFromControls();
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

        private void DisableNotSelected(object sender, MouseEventArgs e)
        {
            switch (FilterComboBox.SelectedIndex)
            {
                case 1:
                    NameTextBox.IsEnabled = true;
                    CountryComboBox.IsEnabled = false;
                    CityComboBox.IsEnabled = false;
                    AccommodationTypeComboBox.IsEnabled = false;
                    GuestNumberTBFilter.IsEnabled = false;

                    break;
                case 2:
                    NameTextBox.IsEnabled = false;
                    CountryComboBox.IsEnabled = true;
                    CityComboBox.IsEnabled = true;
                    AccommodationTypeComboBox.IsEnabled = false;
                    GuestNumberTBFilter.IsEnabled = false;

                    break;
                case 3:
                    NameTextBox.IsEnabled = false;
                    CountryComboBox.IsEnabled = false;
                    CityComboBox.IsEnabled = false;
                    AccommodationTypeComboBox.IsEnabled = true;
                    GuestNumberTBFilter.IsEnabled = false;

                    break;
                case 4:
                    NameTextBox.IsEnabled = false;
                    CountryComboBox.IsEnabled = false;
                    CityComboBox.IsEnabled = false;
                    AccommodationTypeComboBox.IsEnabled = false;
                    GuestNumberTBFilter.IsEnabled = true;

                    break;
                case 5:
                    NameTextBox.IsEnabled = false;
                    CountryComboBox.IsEnabled = false;
                    CityComboBox.IsEnabled = false;
                    AccommodationTypeComboBox.IsEnabled = false;
                    GuestNumberTBFilter.IsEnabled = true;

                    break;
                case 6:
                    {
                        NameTextBox.IsEnabled = false;
                        CountryComboBox.IsEnabled = false;
                        CityComboBox.IsEnabled = false;
                        AccommodationTypeComboBox.IsEnabled = false;
                        GuestNumberTBFilter.IsEnabled = false;

                        break;
                    }
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

                        MakeControlRed(CityComboBox);

                        return;
                    }

                    string city = CountryComboBox.SelectedItem.ToString();
                    ApplyByCity(city);

                    break;
                case 3:
                    ApplyByType((AccommodationType)AccommodationTypeComboBox.SelectedIndex);

                    break;
                case 4:
                    try
                    {
                        ApplyByGuestNumber(int.Parse(GuestNumberTBFilter.Text));
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Please enter a number");

                        MakeControlRed(GuestNumberTBFilter);

                        break;
                    }

                    break;
                case 5:
                    try
                    {
                        ApplyByReservationDays(int.Parse(GuestNumberTBFilter.Text));
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Please enter a number");

                        MakeControlRed(GuestNumberTBFilter);

                        break;
                    }

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

            CityComboBox.ClearValue(Border.BorderThicknessProperty);
            CityComboBox.ClearValue(Border.BorderBrushProperty);
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
            Guest1Menu guest1Menu = new(User);
            guest1Menu.Show();

            Close();
        }

    }
}
