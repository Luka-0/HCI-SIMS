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
using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Model;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourStatistics.xaml
    /// </summary>
    public partial class TourStatistics : Window
    {   
        public TourReviewController TourReviewController = new TourReviewController();
        public UserController UserController = new UserController();
        public TourStatisticsDto Statistics { get; set; }
        public TourStatistics(int id, string tourName)
        {
            InitializeComponent();

            List<TourReview> reviews = TourReviewController.GetByTour(id);
            Statistics = new TourStatisticsDto();
            SetStatistics(reviews);

            this.DataContext = this;
        }

        private void SetVoucherUsagePercentage(List<TourReview> reviews, List<User> tourists)
        {

        }
        
        private void SetStatistics(List<TourReview> reviews)
        {
            List<User> tourists = reviews.Select(r => r.Reservation.BookingGuest).ToList();

            foreach (User user in tourists)
            {
                if (user.Age < 18)
                {
                    Statistics.YouthCount++;
                }
                else
                {
                    if (user.Age < 50)
                    {
                        Statistics.MiddleAgedCount++;
                    }
                    else
                    {
                        Statistics.OldPeopleCount++;
                    }
                }
            }
            SetVoucherUsagePercentage(reviews, tourists);
            
            
        }
    }
}
