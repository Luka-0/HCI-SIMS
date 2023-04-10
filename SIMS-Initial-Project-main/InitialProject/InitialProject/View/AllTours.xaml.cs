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
using System.Windows.Shapes;
using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Model;
using Microsoft.VisualBasic;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AllTours.xaml
    /// </summary>
    public partial class AllTours : Window
    {
        private User LoggedInGuide { get; set; }
        public ObservableCollection<TourBasicInfoDto> BasicTours { get; set; } = new ObservableCollection<TourBasicInfoDto>();


        public TourController TourController { get; set; } = new TourController();
        public VoucherController VoucherController { get; set; } = new VoucherController();

        public AllTours(User user)
        {
            LoggedInGuide = user;
            InitializeComponent();
            this.DataContext = this;
            GetUnfinishedTours(LoggedInGuide.Id);
        }

        private void GetUnfinishedTours(int guideId)
        {
            List<TourBasicInfoDto> tours = TourController.GetByStatus(guideId, TourStatus.Waiting);
            foreach (TourBasicInfoDto tour in tours)
            {
                BasicTours.Add(tour);
            }
        }

        private void CancelTour(object sender, RoutedEventArgs e)
        {
            TourBasicInfoDto tour = (TourBasicInfoDto)DataGridTours.SelectedItem;

            TimeSpan limit = new TimeSpan(0,48,0,0);
            TimeSpan difference = tour.StartDateAndTime - DateTime.Now;
            if (difference > limit)
            {
                TourController.Cancel(tour.id);
                
            }
        }
    }
}
