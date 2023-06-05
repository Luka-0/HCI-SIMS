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
            string filePath = "C:\\Users\\Luka stajic\\Downloads\\File.pdf";

            try
            {
                PdfWriter writter = PdfWriter.GetInstance(fileToGenerate, new FileStream(filePath, FileMode.Create));

                fileToGenerate.Open();

                iTextSharp.text.Paragraph paragraphTitle = new();
                Font fontTitle = FontFactory.GetFont(FontFactory.HELVETICA, 20, Font.BOLD, BaseColor.BLACK);
                Phrase phrazeTitle = new("Hello\n\n", fontTitle);
                paragraphTitle.Alignment = Element.ALIGN_CENTER; // Set alignment to center
                paragraphTitle.Add(phrazeTitle);
                fileToGenerate.Add(paragraphTitle);


                iTextSharp.text.Paragraph paragraph1 = new();
                Font fontParagraph1 = FontFactory.GetFont(FontFactory.COURIER, 16);
                Phrase phrazeParagraph1 = new("My name is Peter and I'm gay. My mom is a lesbian, thank you very much\n", fontParagraph1);
                paragraph1.Add(phrazeParagraph1);
                fileToGenerate.Add(paragraph1);

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
