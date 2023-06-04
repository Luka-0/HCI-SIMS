using GalaSoft.MvvmLight.Messaging;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for ReserveTourView.xaml
    /// </summary>
    public partial class ReserveTourView : UserControl
    {
        public ReserveTourView()
        {
            InitializeComponent();
            BindableBase bindableBase = new BindableBase();
            bindableBase.UpdateFooterParametar("showSingleTour");
            bindableBase.UpdateHeaderTitle("Rezervacija ture");
        }
    }
}
