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

        private User User { get; set; }

        private List<StartEndDateDto> DatesToChoose { get; set; } = new();

        public ObservableCollection<Accommodation> AccommodationsToShow { get; set; } = new();

        public AnywhereWhenever(User user)
        {
            User = user;
            DataContext = this;

            InitializeComponent();
        }

        private void StartDemo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GenerateDates_Click(object sender, RoutedEventArgs e)
        {
            if (IsViolatingAnyUIControl()) return;

            DateTime startDate = StartingDatePicker.SelectedDate.Value;
            DateTime endingDate = EndingDatePicker.SelectedDate.Value;
            int daysToStay = int.Parse(DaysToStayTB.Text);

            if (StartingDatePicker.SelectedDate == null)
            {
                GenerateDatesWithoutStartEnd(startDate, endingDate, daysToStay);
            }
            else
            {
                GenerateDatesWithStartEnd(startDate, endingDate, daysToStay);
            }
        }

        private void GenerateDatesWithoutStartEnd(DateTime startDate, DateTime endingDate, int daysToStay)
        {
            List<Accommodation> accommodations = AccommodationController.GetBy(int.Parse(DaysToStayTB.Text), int.Parse(GuestNumberTB.Text));


        }

        private void GenerateDatesWithStartEnd(DateTime startDate, DateTime endingDate, int daysToStay)
        {
            List<Accommodation> accommodations = AccommodationController.GetBy(int.Parse(DaysToStayTB.Text), int.Parse(GuestNumberTB.Text));

            if(accommodations == null || accommodations.Count == 0)
            {
                MessageBox.Show("There are currently no accommodations that match the entered guest number and staying days");
                return;
            }

            DatesToChoose =  AccommodationReservationController.GetAvailableDates(accommodations.ElementAt(0), startDate, endingDate, daysToStay);

            for (int i=1; i<accommodations.Count; ++i)
            {
                List<StartEndDateDto> tmp = AccommodationReservationController.GetAvailableDates(accommodations.ElementAt(i), startDate, endingDate, daysToStay);

                for(int j=0; j<DatesToChoose.Count; ++j)
                {
                    if (!tmp.Contains(DatesToChoose.ElementAt(j)))
                    {
                        DatesToChoose.Remove(DatesToChoose.ElementAt(j));
                        
                    }
                }
            }

            if (DatesToChoose.Count == 0) MessageBox.Show("Nema");

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

        private void Reservate_Click(object sender, RoutedEventArgs e)
        {

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



        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Guest1Menu guest1Menu = new(User);
            guest1Menu.Show();

            Close();
        }
    }
}
