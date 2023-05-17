using InitialProject.AuxiliaryClass;
using InitialProject.Controller;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Renovations : Page
    {
        public string Username;
        AccommodationController AccommodationController = new AccommodationController();
        RenovationController RenovationController = new RenovationController();

        public Renovations(string username)
        {
            Username = username;
            InitializeComponent(); 
            InitializeOwnersAccommoadtions();
        }

        public void InitializeOwnersAccommoadtions() {

            ownersAccommodations.ItemsSource = AccommodationController.GetAllBy(Username);
        }

        private void duration_days_TextChanged(object sender, TextChangedEventArgs e)
        {
            dateSuggestions.ItemsSource = new List<DateSuggestion>();

            if (startDate.SelectedDate.HasValue && endDate.SelectedDate.HasValue)
            {
                DateTime desiredStart = startDate.SelectedDate.Value;
                DateTime desiredEnd = endDate.SelectedDate.Value;

                if (!duration_days.Text.Equals(""))
                {
                   TimeSpan duration = new System.TimeSpan(Convert.ToInt32(duration_days.Text), 0, 0, 0);

                    Accommodation selectedAccommodation = ownersAccommodations.SelectedItem as Accommodation;

                    dateSuggestions.ItemsSource = AccommodationController.GetRenovationDateSuggestions(selectedAccommodation, desiredStart, desiredEnd, duration);
                }
            }
            else
            {

                MessageBox.Show("Niste izabrali datum!");
            }
        }

        private void scheduleRenovation_Click(object sender, RoutedEventArgs e) {

            DateSuggestion chosenDate = dateSuggestions.SelectedItem as DateSuggestion;

            Accommodation selectedAccommodation = ownersAccommodations.SelectedItem as Accommodation;
       

            TimeSpan duration = new System.TimeSpan(Convert.ToInt32(duration_days.Text), 0, 0, 0);

            if (chosenDate != null){

                RenovationController.Save(selectedAccommodation, chosenDate, description.Text, duration);
            }
            else
            {
                MessageBox.Show("Niste izabrali datum!");
            }

        }

        
    }
}
