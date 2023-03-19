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
        
        public AccommodationReservate()
        {
            InitializeComponent();
            InitializeFilterComboBox();
            InitializeAccommodationTypeComboBox();
            DataContext = this;

            List<Accommodation> allAccommodations = AccommodationRepository.GetAll();
            if(allAccommodations == null)
            {
                MessageBox.Show("There are currently no Accommodations to look at :(");
                this.Close();
            }

            RefreshDataGrid(allAccommodations);
        }

        private void InitializeFilterComboBox()
        {
            FilterComboBox.Items.Add("Name");                   //0
            FilterComboBox.Items.Add("Location");               //1
            FilterComboBox.Items.Add("Accommodation type");     //2
            FilterComboBox.Items.Add("Guest number");           //3
            FilterComboBox.Items.Add("Days for reservation");   //4
            FilterComboBox.Items.Add("Reset");                  //5

            FilterComboBox.SelectedIndex = 0;
        }

        private void InitializeAccommodationTypeComboBox()
        {
            AccommodationTypeComboBox.Items.Add("Apartment");   //0
            AccommodationTypeComboBox.Items.Add("House");       //1
            AccommodationTypeComboBox.Items.Add("Cottage");     //2

            AccommodationTypeComboBox.SelectedIndex = 0;

        }

        private void ReservateAccommodation(object sender, RoutedEventArgs e)
        {
            string startingDate = StartingDatePicker.Text.ToString();
            string endingDate = EndingDatePicker.Text.ToString();
            int guestNumber = int.Parse(GuestNumberTB.Text);

            if(!AccommodationReservationService.Reservate((Accommodation)AccommodationsGrid.SelectedItem, guestNumber, startingDate, endingDate))
            {
                MessageBox.Show("Reservation was unsuccessful");
            }
        }

        private void RefreshDataGrid(List<Accommodation> accommodations)
        {
            accommodationsToShow = new ObservableCollection<Accommodation>();   
            AccommodationsGrid.ItemsSource = accommodationsToShow;
            foreach (Accommodation a in accommodations)
            {
                Accommodation tmp = new Accommodation(a.Title, a.GuestLimit, a.Type, 0, 0, a.Location);
                accommodationsToShow.Add(tmp);
            }
        }

        private void ApplyFilter(object sender, RoutedEventArgs e)
        {
            if (FilterComboBox.SelectedIndex == 0)
            {
                ApplyByName(NameTextBox.Text.ToString());
            }
            else if(FilterComboBox.SelectedIndex == 2)
            {
                ApplyByType((AccommodationType)AccommodationTypeComboBox.SelectedIndex);
            }
            else if(FilterComboBox.SelectedIndex == 5)
            {
                List<Accommodation> accommodations = AccommodationRepository.GetAll();
                RefreshDataGrid(accommodations);
            }


        }

        private void ApplyByName(string name)
        {
            List<Accommodation> accommodations = AccommodationRepository.GetBy(name);
            if (accommodations == null)
            {
                MessageBox.Show("There are currently no Accommodations with that name");
                return;
            }

            RefreshDataGrid(accommodations);
        }

        private void ApplyByType(AccommodationType accommodationType)
        {
            List<Accommodation> accommodations = AccommodationRepository.GetBy(accommodationType);
            if (accommodations == null)
            {
                MessageBox.Show("There are currently no Accommodations with that name");
                return;
            }

            RefreshDataGrid(accommodations);
        }
    }
}
