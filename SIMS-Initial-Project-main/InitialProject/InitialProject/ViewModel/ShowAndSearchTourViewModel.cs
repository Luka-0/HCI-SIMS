using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using ScottPlot.Renderable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.ViewModel
{
    public class ShowAndSearchTourViewModel : BindableBase
    {
        private readonly TourController tourController = new TourController();
        private readonly LocationController locationController = new LocationController();
        private ObservableCollection<Tour> tours;    //for Binding

        public ObservableCollection<string> StateComboBoxItems { get; set; }
        public ObservableCollection<string> CityComboBoxItems { get; set; }
        public ObservableCollection<string> DurationComboBoxItems { get; set; }
        public ObservableCollection<string> LanguageComboBoxItems { get; set; }
        public ObservableCollection<string> GuestLimitComboBoxItems { get; set; }
        private string _selectedState;
        private string _selectedCity;
        private string _selectedDuration;
        private string _selectedLanguage;
        private string _selectedGuestLimit;

        private string _setCommandParametar;
        private Tour _selectedRowData;

        private string _navCommandParam = "statistics";

        public MyICommand SearchByLocationCommand { get; set; }
        public MyICommand SearchByDurationCommand { get; set; }
        public MyICommand SearchByLanguageCommand { get; set; }
        public MyICommand SearchByGuestLimitCommand { get; set; }

        public ShowAndSearchTourViewModel()
        {
            LoadData();

            SelectedState = "Izaberi drzavu";

            UpdateHeaderTitle("Pretraga tura");
            UpdateFooterParametar("home");

            SearchByLocationCommand = new MyICommand(SearchByLocation);
            SearchByDurationCommand = new MyICommand(SearchByDuration);
            SearchByLanguageCommand = new MyICommand(SearchByLanguage);
            SearchByGuestLimitCommand = new MyICommand(SearchByGuestLimit);
        }

        #region Properties
        public ObservableCollection<Tour> Tours
        {
            get { return tours; }
            set
            {
                if (tours != value)
                {
                    tours = value;
                    OnPropertyChanged(nameof(Tours));
                }
            }
        }

        public Tour SelectedRowData
        {
            get { return _selectedRowData; }
            set
            {
                if (_selectedRowData != value)
                {
                    _selectedRowData = value;
                    OnPropertyChanged(nameof(SelectedRowData));
                    UpdateSelectedTourIndex(_selectedRowData.Id.ToString());
                }
            }
        }

        public string SetCommandParameter
        {
            get { return _setCommandParametar; }
            set
            {
                if (_setCommandParametar != value)
                {
                    _setCommandParametar = value;
                    OnPropertyChanged(nameof(SetCommandParameter));
                }
            }
        }

        public string NavCommandParam
        {
            get { return _navCommandParam; }
            set
            {
                _navCommandParam = value;
                OnPropertyChanged(nameof(NavCommandParam));
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

        public string SelectedDuration
        {
            get { return _selectedDuration; }
            set
            {
                _selectedDuration = value;
                OnPropertyChanged(nameof(DurationComboBoxItems));
            }
        }

        public string SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged(nameof(LanguageComboBoxItems));
            }
        }

        public string SelectedGuestLimit
        {
            get { return _selectedGuestLimit; }
            set
            {
                _selectedGuestLimit = value;
                OnPropertyChanged(nameof(GuestLimitComboBoxItems));
            }
        }
        #endregion

        #region Data Methods
        public void LoadData()
        {
            LoadTours();
            LoadStates();
            LoadDurations();
            LoadLanguages();
            LoadGuestLimits();
        }

        public void LoadTours()
        {
            ObservableCollection<Tour> _tours = new ObservableCollection<Tour>();
            List<Tour> tempTours = tourController.GetAll();

            foreach (Tour tour in tempTours)
            {
                _tours.Add(tour);
            }
           
            Tours = _tours;
        }

        public void LoadStates()
        {
            List<Location> locations = locationController.GetAll();
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
            List<Location> locations = locationController.GetByCountry(_selectedState);
            ObservableCollection<string> cities = new ObservableCollection<string>();
            foreach (Location location in locations)
            {
                cities.Add(location.City.ToString());
            }
            CityComboBoxItems = cities;
        }

        public void LoadDurations()
        {
            ObservableCollection<string> durations = new ObservableCollection<string>();
            List<Tour> tempTours = tourController.GetAll();

            foreach(Tour tour in tempTours)
            {
                durations.Add(tour.Duration.ToString());
            }
            durations = new ObservableCollection<string>(durations.Distinct());
            DurationComboBoxItems = durations;
        }

        public void LoadLanguages()
        {
            ObservableCollection<string> languages = new ObservableCollection<string>();
            List<Tour> tempTours = tourController.GetAll();

            foreach (Tour tour in tempTours)
            {
                languages.Add(tour.Language.ToString());
            }
            languages = new ObservableCollection<string>(languages.Distinct());
            LanguageComboBoxItems = languages;
        }

        public void LoadGuestLimits()
        {
            ObservableCollection<string> guestLinits = new ObservableCollection<string>();
            List<Tour> tempTours = tourController.GetAll();

            foreach (Tour tour in tempTours)
            {
                guestLinits.Add(tour.GuestLimit.ToString());
            }
            guestLinits = new ObservableCollection<string>(guestLinits.Distinct());
            GuestLimitComboBoxItems = guestLinits;
        }
        #endregion


        #region Command Methonds

        public void SearchByLocation()
        {
            ObservableCollection<Tour> _tours = new ObservableCollection<Tour>();
            Location location = locationController.GetBy(_selectedState, _selectedCity);
            List<Tour> tours = tourController.GetByLocation(location);

            foreach(Tour tour in tours)
            {
                _tours.Add(tour);
            }
            Tours = _tours;
        }

        public void SearchByDuration()
        {
            ObservableCollection<Tour> _tours = new ObservableCollection<Tour>();
            List<Tour> tours = tourController.GetByDuration(TimeSpan.Parse(_selectedDuration));

            foreach (Tour tour in tours)
            {
                _tours.Add(tour);
            }
            Tours = _tours;
        }

        public void SearchByLanguage()
        {
            ObservableCollection<Tour> _tours = new ObservableCollection<Tour>();
            List<Tour> tours = tourController.GetByLanguage(_selectedLanguage);

            foreach (Tour tour in tours)
            {
                _tours.Add(tour);
            }
            Tours = _tours;
        }

        public void SearchByGuestLimit()
        {
            ObservableCollection<Tour> _tours = new ObservableCollection<Tour>();
            List<Tour> tours = tourController.GetByGuestLimit(Int32.Parse(_selectedGuestLimit));

            foreach (Tour tour in tours)
            {
                _tours.Add(tour);
            }
            Tours = _tours;
        }











        #endregion


        
    }
}
