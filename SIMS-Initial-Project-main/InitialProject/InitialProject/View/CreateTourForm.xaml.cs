using System;
using System.Collections.Generic;
using InitialProject.Forms;
using InitialProject.Model;
using InitialProject.Repository;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.View;
using Microsoft.VisualBasic;


namespace InitialProject
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class CreateTourForm : Window
    {


        public User LoggedInGuide { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public TourController TourController{ get; set; }= new TourController();
        public TourKeyPointController TourKeyPointController{ get; set; } = new TourKeyPointController();



        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreateTourForm(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInGuide = user;


        }

        private List<String> Separate(String names)
        {

            String[] delimiters = { ",", ";", "." };
            List<String> separateNames = new List<String>();
            String[] result = names.Split(delimiters, StringSplitOptions.None);
            for (int i = 0; i < result.Length; i++)
            {
                separateNames.Add(result[i]);
            }
            return separateNames;
        }
        private String[] SeparateForDate(String names)
        {

            String[] delimiters = { ",", ";", ".", "/",  };
            String[] result = names.Split(delimiters, StringSplitOptions.None);
            
            return result;
        }
        private DateTime SetDateAndTime(String dateString, String timeString)
        {
            String[] temp = SeparateForDate(dateString);
            String format = temp[1] + "-" + temp[0] + "-" + temp[2] + " " + timeString;

            //Myb put it in try catch 
            DateTime dateTime = DateTime.ParseExact(format, "d-M-yyyy HH:mm:ss", CultureInfo.InvariantCulture);


            return dateTime;
        }

        private TimeSpan SetDuration(String timeInHours)
        {
            int hour = int.Parse(timeInHours);
            var startTime = new TimeOnly(hour, 00, 00);
            var start =  new TimeSpan(hour, 00, 00);
            return start;
        }

        private void Create(object sender, RoutedEventArgs e)
        {

            TourToControllerDto tourToControllerDto = new TourToControllerDto();
            tourToControllerDto.Name = Name.Text;
            tourToControllerDto.Country = Country.Text;
            tourToControllerDto.City = City.Text;
            tourToControllerDto.Description = Description.Text;
            tourToControllerDto.Language = Language.Text;
            tourToControllerDto.TourKeyPointNames = Separate(TourKeyPoints.Text);
            tourToControllerDto.ImageURLs = Separate(ImageURLs.Text);
            tourToControllerDto.GuestLimit = int.Parse(GuestLimit.Text);
            tourToControllerDto.StartDateAndTime = SetDateAndTime(StartDate.Text, StartTime.Text);
            tourToControllerDto.Duration = SetDuration(Duration.Text);
            tourToControllerDto.ImageURLs = Separate(ImageURLs.Text);
            tourToControllerDto.Guide = LoggedInGuide;

            Tour newTour = TourController.Create(tourToControllerDto);
            List<TourKeyPoint> newKeyPoints=  TourKeyPointController.Create(tourToControllerDto.TourKeyPointNames, newTour);
            TourController.UpdateTourProperties(newTour,tourToControllerDto, newKeyPoints);
        }

        private void ViewTodaysTours(object sender, RoutedEventArgs e)
        {
             ShowTours showTours = new ShowTours(LoggedInGuide);
             showTours.Show();
              
        }

        private void ShowTours(object sender, RoutedEventArgs e)
        {
            AllTours allTours = new AllTours(LoggedInGuide);
            allTours.Show();
        }
    }
}