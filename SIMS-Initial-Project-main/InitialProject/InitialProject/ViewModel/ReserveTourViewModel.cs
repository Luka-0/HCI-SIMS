using GalaSoft.MvvmLight.Messaging;
using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.ViewModel
{
    public class ReserveTourViewModel:BindableBase
    {
        private TourController tourController = new TourController();
        private VoucherController voucherController = new VoucherController();  
        private UserController userController = new UserController();
        private TourReservationController reservationController = new TourReservationController();

        private double _sliderValue = 0;
        private string _sliderLabelText = "Broj gostiju: 0";
        private string _selectedTourId;
        private string _selectedVoucherId;
        private string _dataTextField = "IZABRANA TURA I VAUCER\n\n";
        public MyICommand DecreaseSliderValue { get; set; }
        public MyICommand IncreaseSliderValue { get; set; }
        public MyICommand LoadDataCommand { get; set; }
        public MyICommand ReserveTourCommand { get; set; }
        public ReserveTourViewModel()
        {
            Mediator.Instance.Subscribe("TourIndexUpdated", OnTourIndexUpdated);
            Mediator.Instance.Subscribe("VoucherIndexUpdated", OnVoucherIndexUpdated);

            DecreaseSliderValue = new MyICommand(OnDecreaseSliderValue);
            IncreaseSliderValue = new MyICommand(OnIncreaseSliderValue);
            LoadDataCommand = new MyICommand(LoadData);
            ReserveTourCommand = new MyICommand(OnTourReserve);
        }

        public string DataTextField
        {
            get { return _dataTextField; }
            set
            {
                _dataTextField = value;
                OnPropertyChanged(nameof(DataTextField));
            }
        }

        public string SelectedTourId
        {
            get { return _selectedTourId; }
            set
            {
                _selectedTourId = value;
                OnPropertyChanged(nameof(_selectedTourId));
            }
        }

        public string SelectedVoucherId
        {
            get { return _selectedVoucherId; }
            set
            {
                _selectedVoucherId = value;
                OnPropertyChanged(nameof(SelectedVoucherId));
            }
        }

        public double SliderValue
        {
            get { return _sliderValue; }
            set
            {
                _sliderValue = value;
                _sliderLabelText = "Broj gostiju: " + _sliderValue.ToString();
                OnPropertyChanged(nameof(SliderValue));
                OnPropertyChanged(nameof(SliderLabelText));
            }
        }

        public string SliderLabelText
        {
            get { return _sliderLabelText; }
            set
            {
                _sliderLabelText = value;
                OnPropertyChanged(nameof(SliderLabelText));
            }
        }

        public void OnDecreaseSliderValue()
        {
            if(SliderValue - 1 > 0)
            {
                SliderValue--;
            }
        }

        public void OnIncreaseSliderValue()
        {
            SliderValue++;
        }

        private void OnTourIndexUpdated(object newId)
        {
            SelectedTourId = newId as string;
        }

        private void OnVoucherIndexUpdated(object newId)
        {
            SelectedVoucherId = newId as string;
        }

        public void LoadData()
        {
            if (_selectedTourId != null)
            {
                Tour tour = tourController.GetById(Int32.Parse(_selectedTourId));
                DataTextField = "Podaci o turi: " + tour.ToString() + "\n";
            }

            if(_selectedVoucherId != null)
            {
                Voucher voucher = voucherController.GetById(Int32.Parse(_selectedVoucherId));
                DataTextField += "Podaci o vauceru: " + voucher.ToString() + "\n";
            }
        }

        public void OnTourReserve()
        {
            TourReservation tourReservation = new TourReservation();
            Voucher voucher;
            if (_selectedVoucherId != null)
            {
                voucher = voucherController.GetById(Int32.Parse(_selectedVoucherId));
            }
            else
                voucher = voucherController.GetById(1);


            Tour tour = tourController.GetById(Int32.Parse(_selectedTourId));
            int guestNumber = (int)SliderValue;
            User guest = userController.GetBy(1);
            reservationController.Save(tourReservation, tour, guest, guestNumber, voucher);

            
        }


    }
}
