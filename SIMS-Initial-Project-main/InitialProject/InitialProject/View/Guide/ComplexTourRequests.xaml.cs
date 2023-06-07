using System;
using System.Collections.Generic;
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
using InitialProject.Enumeration;
using InitialProject.Model;
using Microsoft.VisualBasic.Logging;
using ScottPlot.MarkerShapes;

namespace InitialProject.View.Guide
{
    /// <summary>
    /// Interaction logic for ComplexTourRequests.xaml
    /// </summary>
    public partial class ComplexTourRequests : Window
    {
        public User LoggedInGuide { get; set; }
        public ComplexTourRequestController ComplexTourRequestController = new ComplexTourRequestController();
        public TourController TourController = new TourController();
        public TourRequestController TourRequestController = new TourRequestController();

        public List<ComplexTourRequest> Requests { get; set; }
        public List<string> RequestCities { get; set; } = new List<string>();
        public ComplexTourRequest _SelectedComplexTourRequest { get; set; }
        public ComplexTourRequests(User user)
        {
            LoggedInGuide = user;
            Requests = GetAvailableComplexTours(user);
            this.DataContext = this;

            InitializeComponent();
        }

        private List<ComplexTourRequest> GetAvailableComplexTours(User user)
        {
            List<ComplexTourRequest> complexTourRequests= ComplexTourRequestController.GetAllPending();
            List<ComplexTourRequest> availableTourRequests = new List<ComplexTourRequest>();
            availableTourRequests.AddRange(complexTourRequests);
            foreach (ComplexTourRequest complexTour in complexTourRequests)
            {
                if (AvailabilityCheck(complexTour))
                {
                    availableTourRequests.Remove(complexTour);
                }
            }

            return availableTourRequests;
        }

        private bool AvailabilityCheck(ComplexTourRequest complexTour)
        {
            List<int> guidesIds = getGuidesIds(complexTour);
            if(guidesIds.Contains(LoggedInGuide.Id))
            {
                return true;

            }
            return false;

        }

        private List<int> getGuidesIds(ComplexTourRequest complexTour)
        {
            List<int> ids = new List<int>();
            foreach (User guide in complexTour.Guides)
            {
                ids.Add(guide.Id);
            }
            return ids;
        }

        private void AddRequestNames(List<TourRequest> requests)
        {
            foreach (TourRequest request in requests)
            {
                RequestCities.Add(request.Location.City);
            }
        }

        

        private void DataGridRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _SelectedComplexTourRequest = (ComplexTourRequest)DataGridRequests.SelectedItem;
            AddRequestNames(_SelectedComplexTourRequest.Requests);


        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCity = (string)ComboBox1.SelectedItem;

            TourRequest request = GetTourByCityName(selectedCity);
            List<DateTime> occupiedDates = new List<DateTime>();
            if (request != null)
            {
                occupiedDates = TourController.GetOccupiedDays(LoggedInGuide.Id, request.LowerDateLimit, request.UpperDateLimit);
                occupiedDates.AddRange(TourRequestController.GetOccupiedDatesForComplexTour(_SelectedComplexTourRequest));
                
                DatePicker1.DisplayDateStart = request.LowerDateLimit;
                DatePicker1.DisplayDateEnd = request.UpperDateLimit;

                DatePicker1.BlackoutDates.Clear();
                DatePicker1.BlackoutDates.Add(
                    new CalendarDateRange(new DateTime(0001, 1, 1), request.LowerDateLimit.AddDays(-1)));
                DatePicker1.BlackoutDates.Add(
                    new CalendarDateRange(request.UpperDateLimit.AddDays(1), new DateTime(9999, 1, 1)));
                foreach (DateTime date in occupiedDates)
                {
                    DatePicker1.BlackoutDates.Add(new CalendarDateRange(date));
                }
            }

        }

        private TourRequest GetTourByCityName(string selectedCity)
        {
            
            return   _SelectedComplexTourRequest.Requests.FirstOrDefault(x => x.Location.City.Equals(selectedCity));
        }

        private void AcceptRequest(object sender, RoutedEventArgs e)
        {
            string selectedCity = (string)ComboBox1.SelectedItem;
            TourRequest request = GetTourByCityName(selectedCity);
            DateTime selectedDate = (DateTime)DatePicker1.SelectedDate;

            TourRequestController.Accept(request.Id, selectedDate);
            _SelectedComplexTourRequest.Guides.Add(LoggedInGuide);
            //  ComplexTourRequestController.SetGuide(_SelectedComplexTourRequest.Id, LoggedInGuide);

            ComplexTourRequestController.SetGuide(_SelectedComplexTourRequest.Id, LoggedInGuide);

            CreateTourForm createTourForm = new CreateTourForm(LoggedInGuide, request, selectedDate);
            this.Close();
            createTourForm.Show();
            

        }
    }
}
