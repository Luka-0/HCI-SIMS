using InitialProject.Controller;
using InitialProject.Model;
using Microsoft.Win32;
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
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using InitialProject.Enumeration;

namespace InitialProject.View.Guest1
{
    public partial class ViewRatings : Window
    {
        private readonly GuestReviewController GuestReviewController = new();
        private readonly AccommodationReviewController AccommodationReviewController = new();

        private readonly User User;

        public ObservableCollection<InitialProject.Model.GuestReview> ReviewsToShow { get; set; }

        public ViewRatings(User user)
        {
            InitializeComponent();
            InitializeReviewsDataGrid();

            User = user;

        }

        private void InitializeReviewsDataGrid()
        {
            List<InitialProject.Model.GuestReview> allGuestReviews = GuestReviewController.GetAll(User);
            List<AccommodationReview> allAccommodationReviews = AccommodationReviewController.GetBy(User);

            ReviewsToShow = new ObservableCollection<InitialProject.Model.GuestReview>();
            ReviewsGrid.ItemsSource = ReviewsToShow;

            foreach (InitialProject.Model.GuestReview g in allGuestReviews)
            {
                foreach (AccommodationReview a in allAccommodationReviews)
                {
                    if (g.Reservation.Id == a.Reservation.Id)
                    {
                        ReviewsToShow.Add(g);
                        allAccommodationReviews.Remove(a);
                        break;
                    }
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

            Guest1Menu guest1Menu = new(User);
            guest1Menu.Show();

            Close();
        }

        private void GeneratePdf_Click(object sender, RoutedEventArgs e)
        {
            Document fileToGenerate = new(PageSize.A4);
            string filePath = "C:\\Users\\Luka stajic\\Downloads\\Average ratings.pdf";

            try
            {
                PdfWriter writter = PdfWriter.GetInstance(fileToGenerate, new FileStream(filePath, FileMode.Create));

                fileToGenerate.Open();

                // Titled paragraph
                iTextSharp.text.Paragraph paragraphTitle = new();
                Font fontTitle = FontFactory.GetFont(FontFactory.HELVETICA, 20, Font.BOLD, BaseColor.BLACK);
                Phrase phrazeTitle = new("Hello " + User.Username + "\n\n\n", fontTitle);
                paragraphTitle.Alignment = Element.ALIGN_CENTER; // Set alignment to center
                paragraphTitle.Add(phrazeTitle);
                fileToGenerate.Add(paragraphTitle);

                // Intro paragraph
                iTextSharp.text.Paragraph paragraph1 = new();
                Font fontParagraph1 = FontFactory.GetFont(FontFactory.HELVETICA, 16);
                Phrase phraze1Paragraph1 = new("This is a pdf file which was generated when You pressed a button on the \"Viewing Ratings\" window in the HCI-SIMS application\n\n", fontParagraph1);
                Phrase phraze2Paragraph1 = new("Bellow You will see the average ratings You were given, by accommodation owners across all the categories\n\n\n", fontParagraph1);
                paragraph1.Add(phraze1Paragraph1);
                paragraph1.Add(phraze2Paragraph1);
                fileToGenerate.Add(paragraph1);

                // Average ratings paragraph
                iTextSharp.text.Paragraph paragraph2 = new();
                Font fontParagraph2 = FontFactory.GetFont(FontFactory.HELVETICA, 14);

                Phrase phraze1Paragraph2 = new("Marks across all reviews:\n\n", fontParagraph1);
                paragraph2.Add(phraze1Paragraph2);


                double tidiness = GuestReviewController.GetAverageTidinessByUser(User);
                Phrase phraze2Paragraph2 = new("-Average tidiness: " + tidiness.ToString() + "\n\n", fontParagraph2);
                paragraph2.Add(phraze2Paragraph2);

                double obedience = GuestReviewController.GetAverageObedienceByUser(User);
                Phrase phraze3Paragraph2 = new("-Average obedience: " + obedience.ToString() + "\n\n", fontParagraph2);
                paragraph2.Add(phraze3Paragraph2);

                Phrase phraze4Paragraph2 = new("Marks across types of accommodations:\n\n", fontParagraph1);
                paragraph2.Add(phraze4Paragraph2);

                
                double mark = GuestReviewController.GetAverageByAccommodationType(AccommodationType.Apartment, User);
                Phrase phraze5Paragraph2;
                if (mark == 0 || mark is double.NaN)
                {
                    phraze5Paragraph2 = new("-Average marks for apartments: You have no marks\n\n", fontParagraph2);
                }
                else
                {
                    mark /= 2;
                    phraze5Paragraph2 = new("-Average marks for apartments: " + mark.ToString() + "\n\n", fontParagraph2);
                }
                paragraph2.Add(phraze5Paragraph2);

                mark = GuestReviewController.GetAverageByAccommodationType(AccommodationType.Accommodation, User);
                Phrase phraze6Paragraph2;
                if (mark == 0 || mark is double.NaN)
                {
                    phraze6Paragraph2 = new("-Average marks for accommodations: You have no marks\n\n", fontParagraph2);
                }
                else
                {
                    mark /= 2;
                    phraze6Paragraph2 = new("-Average marks for accommodations: " + mark.ToString() + "\n\n", fontParagraph2);
                }
                paragraph2.Add(phraze6Paragraph2);

                mark = GuestReviewController.GetAverageByAccommodationType(AccommodationType.House, User);
                Phrase phraze7Paragraph2;
                if (mark == 0 || mark is double.NaN)
                {
                    phraze7Paragraph2 = new("-Average marks for houses: You have no marks\n\n", fontParagraph2);
                }
                else
                {
                    mark /= 2;
                    phraze7Paragraph2 = new("-Average marks for houses: " + mark.ToString() + "\n\n", fontParagraph2);
                }
                paragraph2.Add(phraze7Paragraph2);

                mark = GuestReviewController.GetAverageByAccommodationType(AccommodationType.Cottage, User);
                Phrase phraze8Paragraph2;
                if (mark == 0 || mark is double.NaN)
                {
                    phraze8Paragraph2 = new("-Average marks for cottages: You have no marks\n\n", fontParagraph2);
                }
                else
                {
                    mark /= 2;
                    phraze8Paragraph2 = new("-Average marks for cottages: " + mark.ToString() + "\n\n", fontParagraph2);
                }
                paragraph2.Add(phraze8Paragraph2);

                fileToGenerate.Add(paragraph2);

                fileToGenerate.Close();

                //System.Diagnostics.Process.Start(filePath);//   baca exception
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return;
            }

            System.Windows.MessageBox.Show("Successfuly created");
        }
    }
}
