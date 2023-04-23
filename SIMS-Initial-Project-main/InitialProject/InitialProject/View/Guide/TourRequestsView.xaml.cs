using InitialProject.Dto;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
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
using System.Globalization;
using Microsoft.EntityFrameworkCore.Update;
using System.Xml.Linq;

namespace InitialProject.View.Guide
{
    /// <summary>
    /// Interaction logic for TourRequestsView.xaml
    /// </summary>
    public partial class TourRequestsView : Window
    {
        private User LoggedInGuide { get; set; }

        public ObservableCollection<TourRequest> FilteredRequests { get; set; } =
            new ObservableCollection<TourRequest>();

        public TourRequestController TourRequestController { get; set; } = new TourRequestController();
        public TourController TourController { get; set; }= new TourController();
        public List<TourRequest> Requests { get; set; }
        public List<TourRequest> AllRequests { get; set; }

        public ObservableCollection<TourRequestYearlyCountDto> RequestYearlyCounts { get; set; }

        public DateTime LowerDate { get; set; }
        public DateTime UpperDate { get; set; }

        public TourRequestsView(User user)
        {
            this.DataContext = this;
            LoggedInGuide = user;

            Requests = TourRequestController.GetAllPending();
            AllRequests = TourRequestController.GetAll();
            InitializeComponent();
            RequestYearlyCounts = GetYearlyRequests(AllRequests);


            YearlyDataGrid.ItemsSource = RequestYearlyCounts;

        }

        /*private void Filter(List<TourRequest> requests)
        {
            List<TourRequest> result = requests;
            String country = Country.Text;
            String city = City.Text;
            String language = Language.Text;
            int guestNumber = int.Parse(GuestsNumber.Text);
            DateTime upperDate = ParseDate(UpperDateLimit.Text);
            DateTime lowerDate = ParseDate(LowerDateLimit.Text);


            if (country != String.Empty)
            {
                result.RemoveAll(tr => !tr.Location.Country.Equals(country));
            }

            if (city != String.Empty)
            {
                result.RemoveAll(tr => !tr.Location.City.Equals(country));
            }
            if (language != String.Empty)
            {
                result.RemoveAll(tr => !tr.Language.Equals(country));
            }
            if (guestNumber != null && guestNumber > 0 )
            {
                result.RemoveAll(tr => tr.GuestNumber < guestNumber);
            }
            if (upperDate != null)
            {
                result.RemoveAll(tr => tr.LowerDateLimit.Date.CompareTo(lowerDate.Date) < 0);
            }
            if (lowerDate != null)
            {
                result.RemoveAll(tr => tr.UpperDateLimit.Date.CompareTo(upperDate.Date) > 0);
            }

            FilteredRequests = new ObservableCollection<TourRequest>(result);
        }*/

        private DateTime ParseDate(string dateString)
        {

            String[] temp = SeparateForDate(dateString);
            String format = temp[1] + "-" + temp[0] + "-" + temp[2];

            //Myb put it in try catch 
            DateTime dateTime = DateTime.ParseExact(format, "d-M-yyyy", CultureInfo.InvariantCulture);


            return dateTime;

        }

        private String[] SeparateForDate(String names)
        {

            String[] delimiters = { ",", ";", ".", "/", };
            String[] result = names.Split(delimiters, StringSplitOptions.None);

            return result;
        }

        private void Filter(object sender, RoutedEventArgs e)
        {
            List<TourRequest> result = Requests;
            String country = Country.Text;
            String city = City.Text;
            String language = Language.Text;


            if (country != String.Empty)
            {
                result.RemoveAll(tr => !tr.Location.Country.Equals(country));
            }

            if (city != String.Empty)
            {
                result.RemoveAll(tr => !tr.Location.City.Equals(city));
            }

            if (language != String.Empty)
            {
                result.RemoveAll(tr => !tr.Language.Equals(language));
            }

            if (GuestsNumber.Text != String.Empty)
            {
                int guestNumber = int.Parse(GuestsNumber.Text);
                if (guestNumber > 0)
                {
                    result.RemoveAll(tr => tr.GuestNumber < guestNumber);
                }

            }

            if (LowerDateLimit.Text != String.Empty)
            {
                DateTime lowerDate = ParseDate(LowerDateLimit.Text);
                result.RemoveAll(tr => tr.LowerDateLimit.Date.CompareTo(lowerDate.Date) < 0);
            }

            if (UpperDateLimit.Text != String.Empty)
            {
                DateTime upperDate = ParseDate(UpperDateLimit.Text);
                result.RemoveAll(tr => tr.UpperDateLimit.Date.CompareTo(upperDate.Date) > 0);
            }


            FilteredRequests = new ObservableCollection<TourRequest>(result);
            DataGridRequests.ItemsSource = FilteredRequests;
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            Country.Text = String.Empty;
            City.Text = String.Empty;
            Language.Text = String.Empty;
            GuestsNumber.Text = String.Empty;
            LowerDateLimit.Text = String.Empty;
            UpperDateLimit.Text = String.Empty;

            Requests = TourRequestController.GetAllPending();
            FilteredRequests = new ObservableCollection<TourRequest>(TourRequestController.GetAllPending());
            DataGridRequests.ItemsSource = FilteredRequests;

        }

        public ObservableCollection<TourRequestYearlyCountDto> GetYearlyRequests(List<TourRequest> requests)
        {
            List<TourRequestYearlyCountDto> count = new List<TourRequestYearlyCountDto>();
            List<int> uniqueYears = new List<int>();
            foreach (TourRequest request in requests)
            {
                int year = request.LowerDateLimit.Date.Year;
                if (!uniqueYears.Contains(year))
                {
                    uniqueYears.Add(year);
                    count.Add(new TourRequestYearlyCountDto(year, request.LowerDateLimit));
                }
                else
                {
                    TourRequestYearlyCountDto dto = count.FirstOrDefault(t => t.Year == year);
                    dto.Count++;
                    dto.Dates.Add(request.LowerDateLimit.Date);
                }
            }

            return new ObservableCollection<TourRequestYearlyCountDto>(count);

        }


        private void ShowMonthly(object sender, RoutedEventArgs e)
        {
            TourRequestYearlyCountDto request = (TourRequestYearlyCountDto)YearlyDataGrid.SelectedItem;
            MonthlyRequests monthlyRequests = new MonthlyRequests(request);
            monthlyRequests.Show();
        }

        private void CreateNewTour(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterButton.Visibility = Visibility.Visible;
            if (ComboBox1.SelectedIndex == 0) // Option 1 is selected
            {
                LocationCountryLabel.Visibility = Visibility.Visible;
                InputLocationCountry.Visibility = Visibility.Visible;
                LocationCityLabel.Visibility = Visibility.Visible;
                InputLocationCity.Visibility = Visibility.Visible;

                LanguageLabel.Visibility = Visibility.Collapsed;
                InputLanguage.Visibility = Visibility.Collapsed;
                InputLanguage.Text = String.Empty;


                FilterByLocation(InputLocationCountry.Text.ToLower(), InputLocationCity.Text.ToLower());
            }
            else if (ComboBox1.SelectedIndex == 1)
            {
                LocationCountryLabel.Visibility = Visibility.Collapsed;
                InputLocationCountry.Visibility = Visibility.Collapsed;
                InputLocationCountry.Text = String.Empty;
                LocationCityLabel.Visibility = Visibility.Collapsed;
                InputLocationCity.Visibility = Visibility.Collapsed;
                InputLocationCity.Text = String.Empty;

                LanguageLabel.Visibility = Visibility.Visible;
                InputLanguage.Visibility = Visibility.Visible;

                FilterByLanguage(InputLanguage.Text.ToLower());

            }
        }

        private List<TourRequest> FilterByLanguage(string inputLanguage)
        {
            return AllRequests.Where(r => r.Language.ToLower().Equals(inputLanguage)).ToList();
        }

        private List<TourRequest> FilterByLocation(string inputCountry, string inputCity)
        {
            if (inputCountry != String.Empty && inputCity != String.Empty)
            {
                return AllRequests.Where(r =>
                        r.Location.Country.ToLower().Equals(inputCountry) &&
                        r.Location.City.ToLower().Equals(inputCity))
                    .ToList();
            }

            if (inputCountry != String.Empty && inputCity == String.Empty)
            {
                return AllRequests.Where(r => r.Location.Country.ToLower().Equals(inputCountry)).ToList();
            }

            if (inputCountry == String.Empty && inputCity != String.Empty)
            {
                return AllRequests.Where(r => r.Location.City.ToLower().Equals(inputCity)).ToList();
            }

            return AllRequests;
        }

        private void UpdateYearly(object sender, RoutedEventArgs e)
        {
            List<TourRequest> requests = new List<TourRequest>();

            if (!(InputLocationCountry.Text == String.Empty &&
                  InputLocationCity.Text == String.Empty & InputLanguage.Text == String.Empty))
            {
                if (InputLanguage.Text != String.Empty)
                {
                    requests = FilterByLanguage(InputLanguage.Text.ToLower());
                }
                else
                {
                    requests = FilterByLocation(InputLocationCountry.Text.ToLower(), InputLocationCity.Text.ToLower());
                }
            }
            else
            {
                requests = AllRequests;
            }

            ObservableCollection<TourRequestYearlyCountDto> filteredYearlyDtos = GetYearlyRequests(requests);
            YearlyDataGrid.ItemsSource = filteredYearlyDtos;


        }

        private void AcceptRequest(object sender, RoutedEventArgs e)
        {
            TourRequest selected = (TourRequest)DataGridRequests.SelectedItem;
            DateTime selectedDate = (DateTime)DatePicker1.SelectedDate;

            TourRequestController.Accept(selected.Id, selectedDate);


            Requests = TourRequestController.GetAllPending();
            FilteredRequests = new ObservableCollection<TourRequest>(Requests);
            DataGridRequests.ItemsSource = FilteredRequests;

            //TODO SLANJE OBAVESTENJA GOSTU2

        }

        private void DataGridRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TourRequest
                selected = (TourRequest)DataGridRequests
                    .SelectedItem; // Replace "MyObject" with the type of your object


            DatePicker1.BlackoutDates.Clear();
            List<DateTime> occupiedDates = new List<DateTime>();
            if (selected != null)
            {
                occupiedDates= TourController.GetOccupiedDays(LoggedInGuide.Id, selected.LowerDateLimit, selected.UpperDateLimit);
            }
            

            if (selected != null)
            {
                DatePicker1.DisplayDateStart = selected.LowerDateLimit;
                DatePicker1.DisplayDateEnd = selected.UpperDateLimit;

                DatePicker1.BlackoutDates.Add(
                    new CalendarDateRange(new DateTime(0001, 1, 1), selected.LowerDateLimit.AddDays(-1)));
                DatePicker1.BlackoutDates.Add(
                    new CalendarDateRange(selected.UpperDateLimit.AddDays(1), new DateTime(9999, 1, 1)));
                foreach (DateTime date in occupiedDates)
                {
                    DatePicker1.BlackoutDates.Add(new CalendarDateRange(date));
                }
                
            }
        }


    }
}
