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

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for AllRenovations.xaml
    /// </summary>
    public partial class AllRenovations : Page
    {
        private string Username;
       
        private readonly RenovationController RenovationController = new RenovationController();

        public AllRenovations(string ownerUsername)
        {
            Username = ownerUsername;
            InitializeComponent();
            LoadRenovations();
        }

        public void LoadRenovations() {

            renovations.ItemsSource = RenovationController.GetAllBy(Username);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RenovationDto selectedRenovation = renovations.SelectedItem as RenovationDto;

            if (selectedRenovation == null){

                MessageBox.Show("Please select a renovation you want to cancel.");
            }

            if (RenovationController.Delete(selectedRenovation)){

                MessageBox.Show("Renovation is cancelled.");
            }
            else {

                MessageBox.Show("Less than 5 days left before this renovation starts.\n Unable to cancell renovation.\n");
            }

        }
    }
}
