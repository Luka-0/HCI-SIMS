using InitialProject.Controller;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
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

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for GradeTour.xaml
    /// </summary>
    public partial class GradeTour : Window
    {
        public GradeTour()
        {
            InitializeComponent();
            ShowAllTours();
        }

        private TourController tourController = new TourController();
        private LocationController LocationController = new();
        private VoucherController VoucherController = new();


        TourService TourService = new TourService(new TourRepository());
        TourReservationController _reservationController = new TourReservationController();
        TourReviewController tourReviewController = new TourReviewController();
        public ObservableCollection<TourReservation> toursToShow { get; set; }

        public ObservableCollection<Voucher> vouchersToShow { get; set; }

        private UserController UserController = new UserController();

        public List<Tour> Tours
        {
            get;
            set;
        }

        private void RefreshDataGrid(List<TourReservation> tours)
        {
            toursToShow = new ObservableCollection<TourReservation>();
            TourShowGrid.ItemsSource = toursToShow;
            foreach (TourReservation t in tours)
            {
                toursToShow.Add(t);
            }
        }

        public void ShowAllTours()
        {
            this.DataContext = this;
            List<TourReservation> allTours = _reservationController.GetAll();
            RefreshDataGrid(allTours);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TourReservation selected = (TourReservation)TourShowGrid.SelectedItem;

                TourReview review = new TourReview();
                //review.Reservation = (TourReservation)TourShowGrid.SelectedItem;
                review.GuideLanguage = Int32.Parse(jezik.Text);
                review.Comment = komentar.Text;
                review.AmusementScore = Int32.Parse(zanimljivost.Text);
                review.GuideKnowledge = Int32.Parse(znanje.Text);
                review.Images = "";
                tourReviewController.Save(review);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
