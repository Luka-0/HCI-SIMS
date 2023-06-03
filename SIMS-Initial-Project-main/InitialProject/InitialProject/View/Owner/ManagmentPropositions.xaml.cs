using InitialProject.Controller;
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

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for ManagmentPropositions.xaml
    /// </summary>
    public partial class ManagmentPropositions : Page
    {
        AccommodationController accommodation = new AccommodationController();
        public ManagmentPropositions()
        {
            InitializeComponent();
            InitializeRegistrationSuggestion();
            InitializeClosingSuggestion();
        }


        public void InitializeRegistrationSuggestion() {

            List<string> registrationSuggestions = new List<string>();
            List<String> cities = new List<string>();

            int i = 0;
            foreach (var location in this.accommodation.GetPopularLocations()) {

                if (!cities.Contains(location.City)) 
                {

                    //ako ima vise smestaja na jednoj popularnoj lokaciji, lokacija se ne dodaje ponovo
                    i++;
                    cities.Add(location.City);
                    registrationSuggestions.Add(i.ToString() + ") " + location.Country + "|" + location.City + ": seems to be very popoular location currently!\n>>>Suggesting new accommodation registration");
                }
            }

            registrationSuggestionsListBox.ItemsSource = registrationSuggestions;
        }

        public void InitializeClosingSuggestion()
        {

            List<string> closingSuggestions = new List<string>();

            int i = 0;
            foreach (var acc in this.accommodation.GetInferiorlarAccommodations())
            {                            
                    i++;
                closingSuggestions.Add(i.ToString() + ") Accommodation: " + acc.Title +" in " + acc.Location.City + ", has inferior occupancy.\n>>>Suggesting to close this accommodation.");
                
            }

            closingSuggestionsListBox.ItemsSource = closingSuggestions;
        }
    }
}
