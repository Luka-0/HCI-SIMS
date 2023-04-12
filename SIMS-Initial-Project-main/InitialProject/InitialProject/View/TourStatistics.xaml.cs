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
        public TourStatisticsDto Statistics { get; set; } = new TourStatisticsDto();
        public TourGuestsDto TotalStatistics { get; set; } = new TourGuestsDto();
        public TourStatistics(int id, string tourName)
        {

            List<TourReview> reviews = TourReviewController.GetManyByTour(id);
            Statistics = new TourStatisticsDto();
            SetStatistics(reviews);
            Statistics.TourName = tourName;
            this.DataContext = this;
            InitializeComponent();

        }

        private void SetVoucherUsagePercentage(List<TourReservation> reservations)
        {
            int voucherCount = 0;
            int total = reservations.Count;
            if (total > 0)
            {
                foreach (TourReservation reservation in reservations)
                {
                    if (reservation.Voucher != null) voucherCount++;
                }
                Statistics.WithVouchers = (int)((double)voucherCount / (double)total * 100.0);
                Statistics.WithoutVouchers = 100 - Statistics.WithVouchers;

            }
        }
        
        private void SetStatistics(List<TourReview> reviews)
        {
            

            List<TourReservation> reservations = reviews.Select(t=>t.Reservation).ToList();

            List<User?> tourists = reviews.Select(r => r.Reservation.BookingGuest).ToList();

            foreach (var user in tourists)
            {
                if (user != null && user.Age < 18)
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
            SetVoucherUsagePercentage(reservations);
            
            
        }
    }
}
