using InitialProject.Controller;
using InitialProject.Model;
using InitialProject.View.Owner;
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
    public partial class OwnerPage : Window
    {
        private string Username;

        public OwnerPage(string username)
        {
            this.Username = username;

            InitializeComponent();

            OperationsContainer.Content = new Notifications(username);

            //System.DateTime today = System.DateTime.Now;
            //System.TimeSpan duration = new System.TimeSpan(36, 0, 0, 0);
            //System.DateTime answer = today.Add(duration);
            //MessageBox.Show(answer.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OperationsContainer.Content = new AccommodationRegister(Username);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OperationsContainer.Content = new GuestReview(Username);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OperationsContainer.Content = new Notifications(Username);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {

            OperationsContainer.Content = new OwnerReviews(Username);
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            OperationsContainer.Content = new ReschedulingRequestsHandler(Username);
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
              OperationsContainer.Content = new Renovations(Username);
        }
    }
}
