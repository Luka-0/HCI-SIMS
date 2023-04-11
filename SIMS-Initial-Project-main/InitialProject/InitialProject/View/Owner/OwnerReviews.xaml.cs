using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Enumeration;
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

        //recenzije koje vlasnik moze da vidi
        private List<AccommodationReviewDto> visibleOwnerReviews;

        //sve recenzije koje vlasnik ima
        private List<AccommodationReviewDto> ownerReviews;

        public OwnerReviews(string ownerUsername)
        {
           // this.SuperOwner = false;
            this.OwnerUsername = ownerUsername;
            this.visibleOwnerReviews = new List<AccommodationReviewDto>();
            this.ownerReviews = new List<AccommodationReviewDto>();

            InitializeComponent();
            InitializeReviewReports();
            InitializeOwnersTitle();
        }

        public void InitializeReviewReports()
        {
            this.visibleOwnerReviews = AccommodationReviewController.GetAllGradedBy(OwnerUsername);
            reviews.ItemsSource = visibleOwnerReviews;

            this.ownerReviews = AccommodationReviewController.GetAllBy(OwnerUsername);

        }

        //azuriranje titule vlasnika na frontu
        public void InitializeOwnersTitle() {

            double average = Math.Round(GetGradeSum() / GetGradeCount(), 2);
            
            // let limit 4, for running tests
            // real limit by specification: 50
            if (average >= 9.5 && GetGradeCount() >= 50)
            {
                TitlePlaceHolder.Text = "Super-Owner";
                AccommodationReviewController.DeclareOwner(OwnerUsername);
            }
            else { 
                TitlePlaceHolder.Text = "Owner";
                AccommodationReviewController.DeclareOwner(OwnerUsername);
            }

            AverageGradePlaceHolder.Text = average.ToString();
            GradeNumPlaceHolder.Text = GetGradeCount().ToString();
        }
        public double GetGradeSum() {

            double gradeSum = 0;

            foreach (var review in ownerReviews)
            {
                gradeSum += (review.Correctness + review.Tidiness);
            }
            return gradeSum;
        }

        public double GetGradeCount()
        {
            double gradeCount = 0;

            foreach (var review in ownerReviews)
            {
                gradeCount++;
            }
            return gradeCount;
        }
    }
}
