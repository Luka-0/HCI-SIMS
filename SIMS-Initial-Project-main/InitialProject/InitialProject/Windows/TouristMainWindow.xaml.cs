using InitialProject.ViewModels;
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

namespace InitialProject.Windows
{
    /// <summary>
    /// Interaction logic for TouristMainWindow.xaml
    /// </summary>
    public partial class TouristMainWindow : Window
    {
        public TouristMainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

        private void ShowAndSearchTourView_Initialized(object sender, EventArgs e)
        {
            Application.Current.MainWindow = new TouristMainWindow()
            {
                DataContext = new MainViewModel()
            };
            //Application.Current.MainWindow.Show();
        }
    }
}
