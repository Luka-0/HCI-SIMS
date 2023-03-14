using InitialProject.Contexts;
using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for CreateAccommodationView.xaml
    /// </summary>
    public partial class AccommodationRegister : Page
    {
        public AccommodationRegister()
        {
            InitializeComponent();

            InitializeLocationPicker();

            InitializeAccommodationType();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //preparing fields to be in right format
            int guestNumber = Int32.Parse(guestLimit.Text);
            AccommodationType type = Convert(typePicker.SelectedIndex);
            int minReservationDays = Int32.Parse(minDuration.Text);
            int cancellationDeadline = Int32.Parse(suspensionDays.Text);

            //calling method for finding the name of selected city
            //from owner's GUI fro Accommodation's location
            string selectedLocation = locationPicker.SelectedItem.ToString();
            string cityName = SeparateCity(selectedLocation);

            List<String> imageUrls = Copy(urls.Items);

            //Prepareing DTO for service, to be saved in the database
            NewAccommodationDto record = new NewAccommodationDto(title.Text, guestNumber, type, minReservationDays, cancellationDeadline, cityName, imageUrls);
            AccommodationServicecs.Save(record);
        }

        private List<string> Copy(ItemCollection urls)
        {
            List<String> imageUrls = new List<String>();

            foreach (var url in urls) {

                imageUrls.Add(url.ToString());
            }

            return imageUrls;
        }

        public void InitializeAccommodationType()
        {

            typePicker.Items.Add(AccommodationType.Apartment.ToString());
            typePicker.Items.Add(AccommodationType.House.ToString());
            typePicker.Items.Add(AccommodationType.Cottage.ToString());

            typePicker.SelectedIndex = 0;
        }

        public void InitializeLocationPicker()
        {

            List<Location> locations = LocationRepository.getAll();

            foreach (Location l in locations)
            {
                locationPicker.Items.Add(l.Country + "-" + l.City);
            }
            locationPicker.SelectedIndex = 0;

        }

        public AccommodationType Convert(int index)
        {

            AccommodationType type;
            if (index == 0)
            {
                type = AccommodationType.Apartment;
            }
            else if (index == 1)
            {
                type = AccommodationType.House;
            }
            else
            {
                type = AccommodationType.Cottage;
            }
            return type;
        }

        public string SeparateCity(string selectedLocation) {

            char[] delimiter = { '-' };

            string[] locationParts = selectedLocation.Split(delimiter);

            string cityName = locationParts[1];

            return cityName;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<Location> locations = LocationService.getAll();

            bool alreadyExists = false;

            foreach (Location l in locations)
            {
                if (city.Text.Equals(l.City)) {
                    alreadyExists = true;
                }
            }

            if (alreadyExists) {

                MessageBox.Show("Location already exists.\nTry to find it in the drop-down menu again.");
            }
            else {

             NewLocationDto location = new NewLocationDto(city.Text, country.Text);
             LocationService.Save(location);

             locationPicker.Items.Clear();
             InitializeLocationPicker();     
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            urls.Items.Add(url.Text);
        }
    }
}
