using InitialProject.Model;
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
using System.Windows.Shapes;

namespace InitialProject.View
{
    public partial class Guest1 : Window
    {
        private User User { get; set; }

        public Guest1(User user)
        {
            InitializeComponent();
            User = user;
        }

        private void ShowAccommodationReservate(object sender, RoutedEventArgs e)
        {

            AccommodationReservate accommodationReservate = new(User);
            accommodationReservate.Show();

            this.Close();
        }
    }
}
