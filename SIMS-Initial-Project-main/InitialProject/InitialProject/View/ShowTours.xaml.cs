using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for ShowTours.xaml
    /// </summary>
    /// 
    public partial class ShowTours : Window
    {
        public ObservableCollection<TourBasicInfoDto> BasicTours { get; set; }
        public TourController tourController { get; set; } = new TourController();
        private int ColNum{ get; set; } = 0;

        public ShowTours()
        {
            InitializeComponent();
            this.DataContext = this;
            BasicTours = new ObservableCollection<TourBasicInfoDto>();
            List<TourBasicInfoDto> basicTours= tourController.GetTodays();
            //List<TourBasicInfoDto> basicTours = tourController.getAllBasicInfo();
            foreach (TourBasicInfoDto tour in basicTours)
            {
                BasicTours.Add(tour);    
            }


        }
        private void generateColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "id")
            {
                e.Cancel = true;
            }
            ColNum++;
            if (ColNum == 7)
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void ViewSpecificTour(object sender, RoutedEventArgs e)
        {

        }
    }
}
