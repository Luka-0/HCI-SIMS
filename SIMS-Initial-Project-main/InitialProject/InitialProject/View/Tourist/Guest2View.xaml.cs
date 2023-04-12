using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.View.Tourist;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for Guest2View.xaml
    /// </summary>
    public partial class Guest2View : Window
    {
        private TourController tourController = new TourController();
        private LocationController LocationController = new();
        private VoucherController VoucherController = new();


        TourService TourService = new TourService(new TourRepository());
        TourReservationController _reservationController = new TourReservationController();

        public ObservableCollection<Tour> toursToShow { get; set; }

        public ObservableCollection<Voucher> vouchersToShow { get; set; }

        private UserController UserController = new UserController();

        public List<Tour> Tours
        {
            get;
            set;
        }

        public List<TourReservation> Reservations
        {
            get;
            set;
        }

        public Guest2View()
        {
            InitializeComponent();
            ShowAllTours();
            ShowAllVouchers();
            //this.ResizeMode = ResizeMode.NoResize;

        }

        public void ShowReservations()
        {
            Reservations = _reservationController.GetAll();
            foreach (TourReservation tour in Reservations)
            {
                MessageBox.Show(tour.ToString());
            }
        }

        public void ShowAllVouchers()
        {
            List<Voucher> allVouchers = VoucherController.GetAll();
            RefreshVoucherGrid(allVouchers);
        }

        private void RefreshVoucherGrid(List<Voucher> vouchers)
        {
            vouchersToShow = new ObservableCollection<Voucher>();
            ShowVoucher.ItemsSource = vouchersToShow;
            foreach (Voucher v in vouchers)
            {
                vouchersToShow.Add(v);
            }
        }
        private void RefreshDataGrid(List<Tour> tours)
        {
            toursToShow = new ObservableCollection<Tour>();
            TourShowGrid.ItemsSource = toursToShow;
            foreach (Tour t in tours)
            {
                toursToShow.Add(t);
            }
        }

        public void ShowAllTours()
        {
            this.DataContext = this;
            List<Tour> allTours = TourService.GetAll();
            RefreshDataGrid(allTours);
        }

        private void ShowByLocation_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ShowByLanguagee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string language = inputField.Text;
                Tours = tourController.GetByLanguage(language);
                RefreshDataGrid(Tours);
            }
            catch
            {
                MessageBox.Show("Greska");
            }
        }

        private void ShowByLenght_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string duration1 = inputField.Text;
                TimeSpan duration;
                TimeSpan.TryParse(inputField.Text, out duration);
                MessageBox.Show(duration.ToString());
                Tours = tourController.GetByDuration(duration);
                RefreshDataGrid(Tours);
            }
            catch
            {
                MessageBox.Show("Greska");
            }
        }

        private void ShowByGuestNumber_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int guestLimit = Int32.Parse(inputField.Text);

                List<Tour> tours = tourController.GetByGuestLimit(guestLimit);


                RefreshDataGrid(tours);
            }
            catch
            {
                MessageBox.Show("Greska");
            }
        }

        private void ReserveTourButton_Click(object sender, RoutedEventArgs e)
        {
         //  try
        //    {
                Tour tour = (Tour)TourShowGrid.SelectedItem;
                int guestNumber = int.Parse(SelectedGuestNumber.Text);

                TourReservationResponseDto response = _reservationController.Reserve(tour, guestNumber);

                if (response.IsFull && response.AvaliableSpace == 0)        //nemoguce rezervisati
                {
                    List<Tour> newTours = tourController.GetByLocation(tour.Location);
                    MessageBox.Show("Tura je popunjena. Pogledajte ture sa iste lokacije.");
                    FreeSpacesLabel.Content = "";
                    SelectedGuestNumber.Text = "";
                    RefreshDataGrid(newTours);
                }else
                    if(response.AvaliableSpace < guestNumber)
                {
                    MessageBox.Show("Nema mesta za uneti broj gostiju. Smanjite broj i pokusajte ponovo!");
                    FreeSpacesLabel.Content = "Broj slobodnih mesta: " + response.AvaliableSpace.ToString();
                }

                if(!response.IsFull)
                {
                    TourReservation newTourReservation = new TourReservation();
                    _reservationController.Save(newTourReservation, tour, UserController.GetBy("Perica"), guestNumber);
                    MessageBox.Show("Uspesno sacuvana rezervacija!");
                    FreeSpacesLabel.Content = "";
                    SelectedGuestNumber.Text = "";
                    ShowAllTours();
                }
         //  }
           // catch
           // {
           //     MessageBox.Show("Error");
           // }

        }

        private void CancelReservationButton_Click(object sender, RoutedEventArgs e)
        {
            ShowAllTours();
            FreeSpacesLabel.Content = "";
            SelectedGuestNumber.Text = "";
        }

        private void testDugme_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Tour tour = (Tour)TourShowGrid.SelectedItem;
                MessageBox.Show(_reservationController.CountTourReservations(tour).ToString());
            }
            catch
            {
                MessageBox.Show("ne smaraj me\n");
            }
        }

        private void ShowVoucher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Voucher tempVoucher = (Voucher)ShowVoucher.SelectedItem;
            //voucherSelLabel.Content = "Selected voucher: " + tempVoucher.ToString();
        }

        private void ShowKeyPoints_Click(object sender, RoutedEventArgs e)
        {
            KeyPointView keyPointView = new KeyPointView();
            keyPointView.Show();
        }

        private void Grade_Click(object sender, RoutedEventArgs e)
        {
            GradeTour gradeTour = new GradeTour();
            gradeTour.Show();
        }
    }
}
