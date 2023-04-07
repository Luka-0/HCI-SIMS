using InitialProject.Controller;
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
    /// <summary>
    /// Interaction logic for OwnerView.xaml
    /// </summary>
    public partial class Owner : Window
    {
        private string Username;

        private AccommodationController AccommodationController = new AccommodationController();
        private UserController UserController = new UserController();

        public Owner(string username)
        {
            this.Username = username;

            InitializeComponent();
            Refresh(Username);

            OperationsContainer.Content = new Notifications(username);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Refresh(Username);
            OperationsContainer.Content = new AccommodationRegister(Username);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Refresh(Username);
            OperationsContainer.Content = new GuestReview(Username);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Refresh(Username);
            OperationsContainer.Content = new Notifications(Username);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Refresh(Username);
            OperationsContainer.Content = new OwnerReviews(Username);
        }

        private void ChangeAccommodationClassesBy(string ownerUsername) {

            User owner = UserController.GetBy(ownerUsername);

            if (owner.SuperTitle) {

                AccommodationController.UpdateBy(Username,"A");
                return;
            }
            AccommodationController.UpdateBy(Username, "B");
        }

        private void UpdateOwnerTitle(string username){

            OwnerReviews ownerReviews = new OwnerReviews(username);

            UserController.UpdateStatusBy(username, ownerReviews.SuperOwner);
        }

        private void Refresh(string ownerUsername) {

            //for refreshing data
            UpdateOwnerTitle(ownerUsername);
            ChangeAccommodationClassesBy(ownerUsername);
        }
    }
}
