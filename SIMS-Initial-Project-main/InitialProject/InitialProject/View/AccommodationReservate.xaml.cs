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
        private ObservableCollection<AccommodationDto> accommodationsToShow { get; set; } 

        public AccommodationReservate()
        {
            InitializeComponent();
            this.DataContext = this;

            accommodationsToShow = new ObservableCollection<AccommodationDto>();

            List<Accommodation> allAccommodations = AccommodationRepository.GetAll();
            if (allAccommodations == null)
            {
                MessageBox.Show("There are currently no Accommodations to look at :(");
                this.Close();

                return;
            }

            foreach (Accommodation a in allAccommodations)
            {
                AccommodationDto tmp = new AccommodationDto(a.Title, a.GuestLimit, a.Type, a.Location);
                accommodationsToShow.Add(tmp);
            }

            AccommodationsGrid.ItemsSource = accommodationsToShow;
        }

        private void AccommodationsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
