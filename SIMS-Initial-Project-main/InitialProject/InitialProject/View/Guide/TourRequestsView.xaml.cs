﻿using InitialProject.Dto;
using InitialProject.Model;
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
using InitialProject.Controller;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Update;

namespace InitialProject.View.Guide
{
    /// <summary>
    /// Interaction logic for TourRequestsView.xaml
    /// </summary>
    public partial class TourRequestsView : Window
    {
        private User LoggedInGuide { get; set; }
        public ObservableCollection<TourRequest> FilteredRequests{ get; set; } = new ObservableCollection<TourRequest>();
        public TourRequestController TourRequestController { get; set; } = new TourRequestController();
        public List<TourRequest> Requests { get; set; }

        public TourRequestsView( User user)
        {
            LoggedInGuide = user;

            Requests= TourRequestController.GetAllPending();
            
            
            this.DataContext = this;
            InitializeComponent();
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
    }
}
