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
 
            int guestNumber = Int32.Parse(guestLimit.Text);
            AccommodationType type = Convert(typePicker.SelectedIndex);
            int minReservationDays = Int32.Parse(minDuration.Text);
            int cancellationDeadline = Int32.Parse(suspensionDays.Text);

            Accommodation acc = new Accommodation(title.Text, guestNumber, type, minReservationDays, cancellationDeadline);

            AccommodationRepository.Save(acc);


            char[] delimiter = { '-' };

            string[] locationParts = locationPicker.SelectedItem.ToString().Split(delimiter);

            string cityName = locationParts[1];

            MessageBox.Show(cityName);

            var db = new UserContext();
            var newAcc = db.accommodation.Find(acc.Id);
            newAcc.Location = LocationRepository.getBy(cityName);

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
    }
}
