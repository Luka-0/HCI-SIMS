using InitialProject.Controller;
using InitialProject.Model;
using InitialProject.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for TourShowView.xaml
    /// </summary>
    public partial class TourShowView : UserControl
    {
        public TourShowView()
        {
            InitializeComponent();
      //      ShowTour();
        }
      //  private readonly ObservableCollection<TourViewModel> Tours;
       // TourController _tourController = new TourController();

       // public void ShowTour()
       // {
       // //    Tour tura = _tourController.GetById(1);
         //   Tours.Add(new TourViewModel(tura));    
      //  }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("D:\\rozga1.jpg");
            bitmap.EndInit();


            slika.Height = 250;
            slika.Width = 300;
            slika.Source = bitmap;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("D:\\rozga.jpg");
            bitmap.EndInit();


            slika.Height = 250;
            slika.Width = 300;
            slika.Source = bitmap;
        }
    }
}
