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
using InitialProject.Controller;
using InitialProject.Model;

namespace InitialProject.View.Guest1
{
    public partial class AnywhereWhenever : Window
    {
        private readonly AccommodationController AccommodationController = new();

        private User User { get; set; }

        public ObservableCollection<Accommodation> AccommodationsToShow { get; set; }

        public AnywhereWhenever(User user)
        {
            User = user;
            DataContext = this;

            InitializeComponent();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Guest1Menu guest1Menu = new(User);
            guest1Menu.Show();

            Close();
        }

        private void StartDemo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GenerateDates_Click(object sender, RoutedEventArgs e)
        {
            List<Accommodation> accommodations
        }

        private void Reservate_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
