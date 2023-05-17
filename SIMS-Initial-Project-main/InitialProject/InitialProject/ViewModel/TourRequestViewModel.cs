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
using System.Windows.Controls;

namespace InitialProject.ViewModel
{
    public class TourRequestViewModel : BindableBase
    {
        private LocationController locationControler = new LocationController();
        private TourController tourController = new TourController();
        private UserController userController = new UserController();   
        private TourRequestController tourRequestController = new TourRequestController();
        public ObservableCollection<string> StateComboBoxItems { get; set; }
        public ObservableCollection<string> CityComboBoxItems { get; set; }
        public ObservableCollection<string> LanguageComboBoxItems { get; set; }
        public MyICommand CreateTourCommand { get; set; }

        private string _selectedState;
        private string _selectedCity;
        private double _sliderValue = 0;
        private string _sliderLabelText = "Broj gostiju: 0";
        private DateTime _startDate;
        private DateTime _endDate;
        private string _selectedLanguage;

        public string SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
               // MessageBox.Show(value.ToString());
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value.Date;
                MessageBox.Show(value.ToString("dd-MM-yyyy"));
                OnPropertyChanged(nameof(EndDate));
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


        public string SelectedState
        {
            get { return _selectedState; }
            set
            {
                _selectedState = value;
                LoadCities();
                OnPropertyChanged(nameof(CityComboBoxItems));
            }
        }

        public string SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                OnPropertyChanged(nameof(CityComboBoxItems));
            }
        }

        public TourRequestViewModel()
        {
            LoadStates();
            LoadLanguages();
            CreateTourCommand = new MyICommand(CreateTour);
        }

        public void LoadStates()
        {
            List<Location> locations = locationControler.GetAll();
            ObservableCollection<string> states = new ObservableCollection<string>();
            foreach (Location location in locations)
            {
                states.Add(location.Country.ToString());
            }
            states = new ObservableCollection<string>(states.Distinct());
            StateComboBoxItems = states;
        }

        public void LoadCities()
        {
            List<Location> locations = locationControler.GetByCountry(_selectedState);
            ObservableCollection<string> cities = new ObservableCollection<string>();
            foreach (Location location in locations)
            {
                cities.Add(location.City.ToString());
            }
            CityComboBoxItems = cities;
        }

        public void LoadLanguages()
        {
            List<Tour> tours = tourController.GetAll();
            ObservableCollection<string> languages = new ObservableCollection<string>();
            foreach(Tour tour in tours)
            {
                languages.Add(tour.Language);
            }
            languages = new ObservableCollection<string>(languages.Distinct());
            LanguageComboBoxItems = languages;
        }

        public void CreateTour()
        {
            TourRequest request = new TourRequest();
            Location reqLocation = locationControler.GetBy(SelectedState, SelectedCity);

            request.Location = reqLocation;
            request.LowerDateLimit = StartDate;
            request.UpperDateLimit = EndDate;
            request.Description = "Desc";
            request.GuestNumber = (int)SliderValue;
            request.Language = SelectedLanguage;
            request.Tourist = userController.GetBy(1);

            tourRequestController.Save(request, userController.GetBy(1));
        }


    }
}
