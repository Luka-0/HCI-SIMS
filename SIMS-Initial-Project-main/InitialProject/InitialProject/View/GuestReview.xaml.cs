using InitialProject.Controller;
using InitialProject.Dto;
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
    /// Interaction logic for RateGuestsView.xaml
    /// </summary>
    public partial class GuestReview : Page
    {
        public GuestReview()
        {
            InitializeComponent();

            List<ExpiredReservationDto> records = new List<ExpiredReservationDto>();
            records = AccommodationReservationController.LoadExpiredReservations();
            ExpiredReservations.ItemsSource = records;
 
        }

     
    }
}
