using InitialProject.Contexts;
using InitialProject.Enumeration;
using InitialProject.Model;
using InitialProject.Repository;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for CreateAccommodationView.xaml
    /// </summary>
    public partial class CreateAccommodationView : Page
    {
        public CreateAccommodationView()
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

            //creating new accommodation instance and saving it into the database
            Accommodation record = new Accommodation(title.Text, guestNumber, type, minReservationDays, cancellationDeadline);
            AccommodationRepository.Save(record);

            //finding the name of selected city from owner's GUI
            string selectedLocation = locationPicker.SelectedItem.ToString();
            string cityName = SeparateCity(selectedLocation);

            //MessageBox.Show(cityName); //logic test

            //Setting a foreign key - location id in accommodation table
            var db = new UserContext();
            var tempRecord = db.accommodation.Find(record.Id);
            tempRecord.Location = LocationRepository.getBy(cityName);

            db.SaveChanges();

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
    }
}
