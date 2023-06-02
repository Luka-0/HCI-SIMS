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

namespace InitialProject.View.Guest1
{
    public partial class AnywhereWhenever : Window
    {
        private readonly AccommodationController AccommodationController = new();
        private readonly AccommodationReservationController AccommodationReservationController = new();
        private readonly RenovationController RenovationController = new();

        private User User { get; set; }

        private List<StartEndDateDto> DatesToChoose { get; set; } = new();

        public ObservableCollection<Accommodation> AccommodationsToShow { get; set; }

        public AnywhereWhenever(User user)
        {
            User = user;
            DataContext = this;



            InitializeComponent();

            GenerateDatesButton.IsEnabled = false;
            ReservateButton.IsEnabled = false;
        }

        private void StartDemo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GenerateDates_Click(object sender, RoutedEventArgs e)
        {
            if (IsViolatingAnyUIControl()) return;
            if(AccommodationsGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an accommodations You wish to reservate");
                return;
            }

            Accommodation accommodation = (Accommodation)AccommodationsGrid.SelectedItem;
            int daysToStay = int.Parse(DaysToStayTB.Text);

            if (StartingDatePicker.SelectedDate == null && EndingDatePicker.SelectedDate == null)
            {
                GenerateDatesWithoutStartEnd(accommodation, daysToStay);
            }
            else
            {
                DateTime startDate = StartingDatePicker.SelectedDate.Value;
                DateTime endingDate = EndingDatePicker.SelectedDate.Value;
                GenerateDatesWithStartEnd(accommodation, startDate, endingDate, daysToStay);
            }

            ReservateButton.IsEnabled = true;
        }

        private void GenerateDatesWithoutStartEnd(Accommodation accommodation, int daysToStay)
        {
            DatesToChoose = AccommodationReservationController.FindOtherDates(DateTime.Now.AddDays(1), accommodation, daysToStay, 5);

            InitializeDatesComboBox();
        }

        private void GenerateDatesWithStartEnd(Accommodation accommodation, DateTime startDate, DateTime endingDate, int daysToStay)
        {
            List<StartEndDateDto> availableDates = AccommodationReservationController.GetAvailableDates(accommodation, startDate, endingDate, daysToStay);
            List<Renovation> existingRenovations = new List<Renovation>();


            if (availableDates != null)
            {
                foreach (StartEndDateDto t in availableDates)
                {
                    existingRenovations = RenovationController.GetAllBetweenBy(accommodation, t.StartingDate, t.EndingDate);

                    if (existingRenovations.Count == 0)
                    {
                        DatesToChoose.Add(t);
                    }
                }
            }

            if (DatesToChoose == null || DatesToChoose.Count == 0)
            {
                DatesToChoose = AccommodationReservationController.FindOtherDates(endingDate, accommodation, daysToStay, 0);
            }

            InitializeDatesComboBox();

            /*List<Accommodation> accommodations = AccommodationController.GetBy(int.Parse(DaysToStayTB.Text), int.Parse(GuestNumberTB.Text));

            if(accommodations == null || accommodations.Count == 0)
            {
                MessageBox.Show("There are currently no accommodations that match the entered guest number and staying days");
                return;
            }

            DatesToChoose =  AccommodationReservationController.GetAvailableDates(accommodations.ElementAt(0), startDate, endingDate, daysToStay);

            for (int i=1; i<accommodations.Count; ++i)
            {
                List<StartEndDateDto> tmp = AccommodationReservationController.GetAvailableDates(accommodations.ElementAt(i), startDate, endingDate, daysToStay);

                for(int j=0; j<tmp.Count; ++j)
                {
                    int k = 0;
                    StartEndDateDto tmpJ = tmp.ElementAt(j);
                    for (; k<DatesToChoose.Count; ++k)
                    {
                        if (tmpJ.StartingDate.Day == DatesToChoose.ElementAt(k).StartingDate.Day) break;
                    }

                    if(k == DatesToChoose.Count)
                    {
                        foreach (StartEndDateDto s in DatesToChoose)
                        {
                            if (s.StartingDate.Day == tmpJ.StartingDate.Day)
                            {
                                DatesToChoose.Remove(s);
                            }
                        }
                    }
                }
            }

            if (DatesToChoose.Count == 0) MessageBox.Show("Nema");
            */
        }

        private void InitializeDatesComboBox()
        {
            OfferedDatesCB.Items.Clear();

            foreach (StartEndDateDto t in DatesToChoose)
            {
                OfferedDatesCB.Items.Add(t.StartingDate.Date.ToString() + t.EndingDate.Date.ToString());
            }

            OfferedDatesCB.SelectedIndex = 0;
        }

        private void Reservate_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = DatesToChoose[OfferedDatesCB.SelectedIndex].StartingDate;
            DateTime endDate = DatesToChoose[OfferedDatesCB.SelectedIndex].EndingDate;

            if (AccommodationsGrid.SelectedItem == null)
            {
                MessageBox.Show("Please selecent an accommodation");
                return;
            }

            if (AccommodationReservationController.CreateReservation((Accommodation)AccommodationsGrid.SelectedItem, startDate, endDate, int.Parse(GuestNumberTB.Text), User))
            {
                MessageBox.Show("Succesfully saved");

                OfferedDatesCB.Items.Clear();
                return;
            }

            MessageBox.Show("Saving was UNsuccesful");
        }

        private bool IsViolatingAnyUIControl()
        {
            if (int.Parse(GuestNumberTB.Text) < 1)
            {
                MessageBox.Show("Please enter a proper guest number");
                return true;
            }
            if(int.Parse(DaysToStayTB.Text) < 1)
            {
                MessageBox.Show("Please enter a proper number for staying days");
                return true;
            }
            if(StartingDatePicker.SelectedDate != null && EndingDatePicker.SelectedDate != null && StartingDatePicker.SelectedDate.Value > EndingDatePicker.SelectedDate.Value )
            {
                MessageBox.Show("The selected starting date is after ending date, please choose the correct dates");
                return true;
            }

            return false;
        }

        private void RefreshDataGrid(List<Accommodation> accommodations)
        {
            AccommodationsToShow = new ObservableCollection<Accommodation>();
            AccommodationsGrid.ItemsSource = AccommodationsToShow;

            foreach (Accommodation a in accommodations)
            {
                AccommodationsToShow.Add(a);
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Guest1Menu guest1Menu = new(User);
            guest1Menu.Show();

            Close();
        }

        private void ShowAccommodations_Click(object sender, RoutedEventArgs e)
        {
            if (IsViolatingAnyUIControl()) return;

            List<Accommodation> accommodations = AccommodationController.GetBy(int.Parse(DaysToStayTB.Text), int.Parse(GuestNumberTB.Text));

            if (accommodations == null || accommodations.Count == 0)
            {
                MessageBox.Show("There are currently no accommodations that match the entered guest number and staying days");
                return;
            }

            RefreshDataGrid(accommodations);

            GenerateDatesButton.IsEnabled = true;
        }
    }
}
