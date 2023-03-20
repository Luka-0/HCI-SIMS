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
using Microsoft.VisualBasic;


namespace InitialProject
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class CreateTourForm : Window
    {


        public User LoggedInUser { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public NewTourDto NewTourDto { get; set; } = new NewTourDto();
        public TourController tourController { get; set; }= new TourController();

        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreateTourForm(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;


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
            DateAndTime date1;
            String[] temp = SeparateForDate(dateString);
            String format = temp[1] + "-" + temp[0] + "-" + temp[2] + " " + timeString;

            //date = DateAndTime.DateValue(format);
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
            NewTourDto newTourDto = new NewTourDto(Name.Text, Country.Text, City.Text,
                Description.Text, Language.Text, GuestLimit.Text, TourKeyPoints.Text, StartDate.Text, StartTime.Text, Duration.Text, ImageURLs.Text);



            TourToControllerDto tourToControllerDto = new TourToControllerDto();
            tourToControllerDto.Name = newTourDto.Name;
            tourToControllerDto.Country = newTourDto.Country;
            tourToControllerDto.City = newTourDto.City;
            tourToControllerDto.Description = newTourDto.Description;
            tourToControllerDto.Language = newTourDto.Language;
            tourToControllerDto.TourKeyPointNames = Separate(newTourDto.TourKeyPointNames);
            tourToControllerDto.ImageURLs = Separate(newTourDto.ImageURLs);
            tourToControllerDto.GuestLimit = int.Parse(newTourDto.GuestLimit);
            tourToControllerDto.StartDateAndTime = SetDateAndTime(newTourDto.StartDate, newTourDto.StartTime);
            tourToControllerDto.Duration = SetDuration(newTourDto.Duration);
            tourToControllerDto.ImageURLs = Separate(newTourDto.ImageURLs);
            tourController.add(tourToControllerDto);
        }

        private void ViewAllTours(object sender, RoutedEventArgs e)
        {
             ShowTours showTours = new ShowTours();
             showTours.Show();
             Close(); //this.close();
        }
    }
}