using InitialProject.Controller;
using InitialProject.Dto;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for OwnerReviews.xaml
    /// </summary>
    public partial class OwnerReviews : Page
    {
        AccommodationReviewController AccommodationReviewController = new AccommodationReviewController();
        private string OwnerUsername;
        public OwnerReviews(string ownerUsername)
        {
            InitializeComponent();

            OwnerUsername = ownerUsername;

            List<AccommodationReviewDto> accommodationReviews = new List<AccommodationReviewDto>();

            accommodationReviews = AccommodationReviewController.GetAllGradedBy(ownerUsername);
            reviews.ItemsSource = accommodationReviews;
        }
    }
}
