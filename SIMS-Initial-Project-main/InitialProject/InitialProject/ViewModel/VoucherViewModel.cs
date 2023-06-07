using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.ViewModel
{
    public class VoucherViewModel:BindableBase
    {
        private VoucherController voucherController = new VoucherController();
        private TourReservationController tourReservationController = new TourReservationController();
        private UserController userController = new UserController();

        private ObservableCollection<Voucher> _vouchers;

        private Voucher _selectedRowData;
        private Visibility buttonVisibility = Visibility.Collapsed;
        private string _selectedTourId;
        private bool _isButtonEnabled = false;
        public MyICommand CheckButtonVisibillityCommand { get; set; }

        public VoucherViewModel()
        {
            CheckButtonVisibillityCommand = new MyICommand(CheckButtonVisibility);
            Mediator.Instance.Subscribe("TourIndexUpdated", OnTourIndexUpdated);
            LoadData();
            CheckForExtraVoucher();

            UpdateHeaderTitle("Pregled svih vaucera");
            UpdateFooterParametar("home");

            CheckButtonVisibility();
        }

        public ObservableCollection<Voucher> Vouchers
        {
            get { return _vouchers; }
            set
            {
                if (_vouchers != value)
                {
                    _vouchers = value;
                    OnPropertyChanged(nameof(Vouchers));
                }
            }
        }

        public Voucher SelectedRowData
        {
            get { return _selectedRowData; }
            set
            {
                if (_selectedRowData != value)
                {
                    _selectedRowData = value;
                    OnPropertyChanged(nameof(SelectedRowData));
                    UpdateSelectedVoucherIndex(_selectedRowData.Id.ToString());
                    CheckButtonVisibility();
                    CheckButtonEnabled();
                }
            }
        }

        public string SelectedTourId
        {
            get { return _selectedTourId; }
            set
            {
                if (_selectedTourId != value)
                {
                    _selectedTourId = value;
                    OnPropertyChanged(nameof(SelectedTourId));
                }
            }
        }

        public Visibility ButtonVisibility
        {
            get { return buttonVisibility; }
            set
            {
                buttonVisibility = value;
                OnPropertyChanged(nameof(ButtonVisibility));
            }
        }

        public bool IsButtonEnabled
        {
            get { return _isButtonEnabled; }
            set
            {
                _isButtonEnabled = value;
                OnPropertyChanged(nameof(IsButtonEnabled));
            }
        }

        public void LoadData()
        {
            ObservableCollection<Voucher> vouchers = new ObservableCollection<Voucher>();
            List<Voucher> tempVouchers = voucherController.GetAll();

            foreach (Voucher voucher in tempVouchers)
            {
                vouchers.Add(voucher);
            }
            Vouchers = vouchers;
        }

        public void CheckButtonVisibility()
        {
            if(SelectedTourId != null)
            {
                ButtonVisibility = Visibility.Visible;
            }
        }

        public void CheckButtonEnabled()
        {
            if(_selectedRowData != null)
            {
                IsButtonEnabled = true;
            }
        }

        private void OnTourIndexUpdated(object newId)
        {
            SelectedTourId = newId as string;
        }

        public void CheckForExtraVoucher()
        {
            List<TourReservation> tourReservations = tourReservationController.GetAll();
            List<TourReservation> myReservations = new List<TourReservation>();
            User user = userController.GetBy(1);

            foreach(TourReservation tr in tourReservations)
            {
                if(tr.BookingGuest.Id ==  user.Id)
                {
                    myReservations.Add(tr);
                }
            }

            DateTime minDate = myReservations[0].Tour.StartDateAndTime;
            
            foreach(TourReservation tr in myReservations)
            {
                if(tr.Tour.StartDateAndTime < minDate)
                    minDate = tr.Tour.StartDateAndTime;
            }

            DateTime maxDate = minDate.AddYears(1);
          //  MessageBox.Show(maxDate.ToString());

            int countTours = 0;

            foreach (TourReservation tr in myReservations)
            {
                if (tr.Tour.StartDateAndTime >= minDate && tr.Tour.StartDateAndTime <= maxDate)
                    countTours++;
            }

          //  MessageBox.Show(countTours.ToString());

            if(countTours == 5)
            {
                Voucher voucher = new Voucher();
                voucher.User = user;
                voucher.ReceivedDate = DateTime.Now;
                voucher.ObtainedReason = Enumeration.VoucherObtainedReason.Earned;
                voucher.ExpirationDate = DateTime.Now.AddMonths(6);

                voucherController.Save(voucher, user);
                LoadData();
                // MessageBox.Show("Dodat");
            }

        }


    }
}
