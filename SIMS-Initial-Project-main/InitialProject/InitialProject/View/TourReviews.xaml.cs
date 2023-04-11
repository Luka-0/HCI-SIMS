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
using InitialProject.Model;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourReviews.xaml
    /// </summary>
    public partial class TourReviews : Window
    {
        public TourReviewController TourReviewController = new TourReviewController();
        public TourReservationController TourReservationController= new TourReservationController();
        public User LoggedInGuide { get; set; }
        private int tourId { get; set; }
        public ObservableCollection<BasicTourReviewDto> basicTourReviews { get; set; }= new ObservableCollection<BasicTourReviewDto>();
        private List<TourReview> reviews = new List<TourReview>();
        public TourReviews(User user, int tourId)
        {
            
            this.DataContext = this;
            this.tourId = tourId;
            this.LoggedInGuide = user;
            InitializeComponent();
            
            GetReviews(tourId);

        }

        public void GetReviews(int id)
        {
            reviews =  TourReviewController.GetManyByTour(id);

            foreach (TourReview review in reviews)
            {
                TourReservation reservation = TourReservationController.GetById(review.Reservation.Id);
                String keyPointName= TourReservationController.GetArrivalKeyPointName(reservation.Id);
                basicTourReviews.Add(new BasicTourReviewDto(reservation.BookingGuest.Username, review.Valid, review, keyPointName));
            }
        }

        private void InvalidateReview(object sender, RoutedEventArgs e)
        {
            
            CheckBox checkBox = (CheckBox)sender;
            BasicTourReviewDto basicReview= (BasicTourReviewDto)checkBox.DataContext;

            TourReviewController.Invalidate(basicReview.Review.Id);

        }

        private void Report(object sender, RoutedEventArgs e)
        {
            BasicTourReviewDto basicReview = (BasicTourReviewDto)dataGridBasicReviews.SelectedItem;
            TourReviewController.Invalidate(basicReview.Review.Id);

        }
        
    }
}
