using InitialProject.Commands;
using InitialProject.ViewModel;
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
    /// Interaction logic for MainTouristWindow.xaml
    /// </summary>
    public partial class MainTouristWindow : Window
    {
        public MainTouristWindow()
        {
            InitializeComponent();
            this.DataContext = new MainTouristWindowViewModel();
        }

        
    }
}
