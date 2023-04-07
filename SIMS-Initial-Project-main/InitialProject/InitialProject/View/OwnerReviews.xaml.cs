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

        public bool SuperOwner;

        private List<AccommodationReviewDto> accommodationReviews;

        public OwnerReviews(string ownerUsername)
        {
            this.SuperOwner = false;
            this.OwnerUsername = ownerUsername;
            this.accommodationReviews = new List<AccommodationReviewDto>();
            
            InitializeComponent();
            InitializeOwnerReviews();
            InitializeOwnersTitle();
        }

        public void InitializeOwnerReviews()
        {
            this.accommodationReviews = AccommodationReviewController.GetAllGradedBy(OwnerUsername);
            reviews.ItemsSource = accommodationReviews;
        }


        //azuriranje titule vlasnika
        public void InitializeOwnersTitle() {

            double average = GetGradeSum() / GetGradeCount();

            if (average >= 9.5 && GetGradeCount() >= 3)
            {
                TitlePlaceHolder.Text = "Super-Owner";
                this.SuperOwner = true;
            }
            else { 
                TitlePlaceHolder.Text = "Owner";
                this.SuperOwner = false;
            }

            AverageGradePlaceHolder.Text = average.ToString();
            GradeNumPlaceHolder.Text = GetGradeCount().ToString();
        }

        //smatrala sam da nema potrebe da se pravi klasa koja bi imala polja za izracunavanje
        //parametara titule, jer su u pitanju 2-3 polja, a napravila sam razdvojene funkcije
        //kako bi funkcija za inicijalizaciju titule vlasnika imala jednu ulogu
        public double GetGradeSum() {

            double gradeSum = 0;

            foreach (var review in accommodationReviews)
            {
                gradeSum += (review.Correctness + review.Tidiness);
            }
            return gradeSum;
        }

        public double GetGradeCount()
        {
            double gradeCount = 0;

            foreach (var review in accommodationReviews)
            {
                gradeCount++;
            }
            return gradeCount;
        }
    }
}
