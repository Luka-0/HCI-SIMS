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
using System.Windows.Threading;
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

        private int NumOfTicks = 0;

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
            RemoveRedFromControls();

            NumOfTicks = 0;

            DispatcherTimer dt = new();
            dt.Tick += new EventHandler(DtTicker);
            dt.Interval = new TimeSpan(0, 0, 1);
            dt.Start();

        }

        public void DtTicker(object sender, EventArgs e)
        {
            ++NumOfTicks;

            if (NumOfTicks > 30) return;

            switch (NumOfTicks)
            {
                case 1:
                    GuestNumberTB.Focus();
                    break;
                case 2:
                    GuestNumberTB.Text = "2";
                    break;
                case 3:
                    DaysToStayTB.Focus();
                    break;
                case 4:
                    DaysToStayTB.Text = "5";
                    break;
                case 6:
                    StartingDatePicker.Focus();
                    break;
                case 7:
                    StartingDatePicker.IsDropDownOpen = true;
                    break;
                case 8:
                    StartingDatePicker.SelectedDate = new DateTime(2023, 6, 10);
                    break;
                case 10:
                    StartingDatePicker.IsDropDownOpen = false;
                    break;
                case 11:
                    EndingDatePicker.Focus();
                    break;
                case 12:
                    EndingDatePicker.IsDropDownOpen = true;
                    break;
                case 13:
                    EndingDatePicker.SelectedDate = new DateTime(2023, 6, 25);
                    break;
                case 15:
                    EndingDatePicker.IsDropDownOpen = false;
                    break;
                case 17:
                    SAB.BorderBrush = new SolidColorBrush(Colors.Blue);
                    SAB.BorderThickness = new Thickness(2);
                    break;
                case 18:
                    ShowAccommodations_Click(null, null);

                    SAB.ClearValue(Border.BorderThicknessProperty);
                    SAB.ClearValue(Border.BorderBrushProperty);
                    break;
                case 20:
                    AccommodationsGrid.SelectedIndex = 4;
                    break;
                case 22:
                    GenerateDatesButton.BorderBrush = new SolidColorBrush(Colors.Blue);
                    GenerateDatesButton.BorderThickness = new Thickness(2);
                    break;
                case 23:
                    GenerateDates_Click(null, null);

                    GenerateDatesButton.ClearValue(Border.BorderThicknessProperty);
                    GenerateDatesButton.ClearValue(Border.BorderBrushProperty);
                    break;
                case 25:
                    OfferedDatesCB.IsDropDownOpen = true;
                    break;
                case 26:
                    OfferedDatesCB.SelectedIndex = 3;
                    break;
                case 28:
                    OfferedDatesCB.IsDropDownOpen = false;
                    break;
                case 29:
                    ReservateButton.BorderBrush = new SolidColorBrush(Colors.Blue);
                    ReservateButton.BorderThickness = new Thickness(2);
                    break;
                case 30:
                    MessageBox.Show("The demo has ended, all thats left to do is click on Reservate");

                    ReservateButton.ClearValue(Border.BorderThicknessProperty);
                    ReservateButton.ClearValue(Border.BorderBrushProperty);
                    break;
                default:
                    break;
            }
        }

        private void RemoveRedFromControls()
        {
            AccommodationsGrid.ClearValue(Border.BorderThicknessProperty);
            AccommodationsGrid.ClearValue(Border.BorderBrushProperty);

            GuestNumberTB.ClearValue(Border.BorderThicknessProperty);
            GuestNumberTB.ClearValue(Border.BorderBrushProperty);

            DaysToStayTB.ClearValue(Border.BorderThicknessProperty);
            DaysToStayTB.ClearValue(Border.BorderBrushProperty);

            StartingDatePicker.ClearValue(Border.BorderThicknessProperty);
            StartingDatePicker.ClearValue(Border.BorderBrushProperty);

            EndingDatePicker.ClearValue(Border.BorderThicknessProperty);
            EndingDatePicker.ClearValue(Border.BorderBrushProperty);
        }

        private void MakeControlRed<T>(T control) where T : Control
        {
            control.Focus();
            control.BorderBrush = new SolidColorBrush(Colors.Red);
            control.BorderThickness = new Thickness(2);
        }

        private void GenerateDates_Click(object? sender, RoutedEventArgs? e)
        {
            if (IsViolatingAnyUIControl()) return;
            if(AccommodationsGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an accommodations You wish to reservate");

                MakeControlRed(AccommodationsGrid);

                return;
            }

            Accommodation accommodation = (Accommodation)AccommodationsGrid.SelectedItem;

            int daysToStay;
            try
            {
                daysToStay = int.Parse(DaysToStayTB.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a number");
                MakeControlRed(DaysToStayTB);
                return;
            }

            if (StartingDatePicker.SelectedDate == null || EndingDatePicker.SelectedDate == null)
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
            RemoveRedFromControls();
        }

        private void GenerateDatesWithoutStartEnd(Accommodation accommodation, int daysToStay)
        {
            DatesToChoose = AccommodationReservationController.FindOtherDates(DateTime.Now.AddDays(1), accommodation, daysToStay, 5);

            InitializeDatesComboBox();
        }

        private void GenerateDatesWithStartEnd(Accommodation accommodation, DateTime startDate, DateTime endingDate, int daysToStay)
        {
            List<StartEndDateDto> availableDates = AccommodationReservationController.GetAvailableDates(accommodation, startDate, endingDate, daysToStay);
            List<Renovation> existingRenovations = new();

            DatesToChoose.Clear();
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

        private void Reservate_Click(object? sender, RoutedEventArgs? e)
        {
            DateTime startDate = DatesToChoose[OfferedDatesCB.SelectedIndex].StartingDate;
            DateTime endDate = DatesToChoose[OfferedDatesCB.SelectedIndex].EndingDate;

            if (AccommodationsGrid.SelectedItem == null)
            {
                MessageBox.Show("Please selecent an accommodation");

                MakeControlRed(AccommodationsGrid);

                return;
            }

            if (AccommodationReservationController.CreateReservation((Accommodation)AccommodationsGrid.SelectedItem, startDate, endDate, int.Parse(GuestNumberTB.Text), User))
            {
                MessageBox.Show("Succesfully saved");

                OfferedDatesCB.Items.Clear();
                RemoveRedFromControls();

                return;
            }

            MessageBox.Show("Saving was UNsuccesful");
            RemoveRedFromControls();
        }

        private bool IsViolatingAnyUIControl()
        {
            try
            {
                if (int.Parse(GuestNumberTB.Text) < 1)
                {
                    MessageBox.Show("Please enter a proper guest number");

                    MakeControlRed(GuestNumberTB);

                    return true;
                }
                if (int.Parse(DaysToStayTB.Text) < 1)
                {
                    MessageBox.Show("Please enter a proper number for staying days");

                    MakeControlRed(DaysToStayTB);

                    return true;
                }
                if (StartingDatePicker.SelectedDate != null && EndingDatePicker.SelectedDate != null && StartingDatePicker.SelectedDate.Value > EndingDatePicker.SelectedDate.Value)
                {
                    MessageBox.Show("The selected starting date is after ending date, please choose the correct dates");

                    MakeControlRed(StartingDatePicker);

                    return true;
                }
            }
            catch (FormatException)
            {
                return true;
            }

            RemoveRedFromControls();
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

        private void ShowAccommodations_Click(object? sender, RoutedEventArgs? e)
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
            RemoveRedFromControls();
        }
    }
}
