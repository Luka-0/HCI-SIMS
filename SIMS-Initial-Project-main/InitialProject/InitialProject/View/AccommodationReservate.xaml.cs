using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
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

namespace InitialProject.View
{
    public partial class AccommodationReservate : Window
    {
        public ObservableCollection<Accommodation> accommodationsToShow { get; set; } 		
        
        public AccommodationReservate()
        {
            InitializeComponent();
            DataContext = this;

            List<Accommodation> allAccommodations = AccommodationRepository.GetAll();
            if(allAccommodations == null)
            {
                MessageBox.Show("There are currently no Accommodations to look at :(");
                this.Close();
            }

            accommodationsToShow = new ObservableCollection<Accommodation>();
            AccommodationsGrid.ItemsSource = accommodationsToShow;
            foreach (Accommodation a in allAccommodations)
            {
                Accommodation tmp = new Accommodation(a.Title, a.GuestLimit, a.Type, 0, 0);
                tmp.Location = a.Location;
                accommodationsToShow.Add(tmp);
            }

        }


        private void AccommodationsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
