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
    /// <summary>
    /// Interaction logic for OwnerView.xaml
    /// </summary>
    public partial class Owner : Window
    {
        public string Username;
        public Owner(string username)
        {
            InitializeComponent();

            OperationsContainer.Content = new Notifications();

            Username = username;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OperationsContainer.Content = new AccommodationRegister(Username);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OperationsContainer.Content = new GuestReview();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OperationsContainer.Content = new Notifications();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            OperationsContainer.Content = new OwnerReviews(Username);
        }
    }
}
