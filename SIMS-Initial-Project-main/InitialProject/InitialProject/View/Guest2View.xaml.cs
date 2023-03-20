using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Interaction logic for Guest2View.xaml
    /// </summary>
    public partial class Guest2View : Window
    {
        TourController tourController = new TourController();   

        TourService TourService = new TourService();

        public List<GetTourDto> Tours
        {
            get;
            set;
        }

        public Guest2View()
        {
            InitializeComponent();
            ShowAllTours();


        }

        public void ShowAllTours()
        {
          //  this.DataContext = Tours;
            Tours = tourController.GetAll();
            TourShowGrid.ItemsSource = Tours;
        }

        private void mojButton_Click(object sender, RoutedEventArgs e)
        {
            
            if(Tours.Count > 0)
            {
                MessageBox.Show("Nisam prazna");
            }
            else
            {
                MessageBox.Show("Prazna sam");
            }
            String s = "";

           

            


            foreach (GetTourDto t in Tours)
            {
                s += t.ToString();
                
            }
            MessageBox.Show(s);

            

        }
    }
}
