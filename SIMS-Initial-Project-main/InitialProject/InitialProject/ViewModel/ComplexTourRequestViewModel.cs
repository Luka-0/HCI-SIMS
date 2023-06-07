using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Migrations;
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
    public class ComplexTourRequestViewModel:BindableBase
    {
        ComplexTourRequestController complexTourRequestController = new ComplexTourRequestController();
        private LocationController locationControler = new LocationController();
        private TourController tourController = new TourController();
        private UserController userController = new UserController();
        private TourRequestController tourRequestController = new TourRequestController();
        public ObservableCollection<string> StateComboBoxItems { get; set; }
        public ObservableCollection<string> CityComboBoxItems { get; set; }
        public ObservableCollection<string> LanguageComboBoxItems { get; set; }

        public List<TourRequest> tourRequests;


        private string _selectedState = "";
        private string _selectedCity = "";
        private double _sliderValue = 0;
        private string _sliderLabelText = "Broj gostiju: 0";
        private DateTime _startDate = DateTime.Now;
        private DateTime _endDate = DateTime.Now;
        private string _selectedLanguage = "";
        private string _description = "";

        public List<string> _commentsComboBox;
        private string _selectedItem = "";
        private bool _isRequestEnabled = false;
        private Visibility _labelVisibility = Visibility.Visible;

        public MyICommand AddNewTour { get; set; }
        public MyICommand AddRequest { get; set; }

        public MyICommand EndTourEnter { get; set; }


        public ComplexTourRequestViewModel()
        {
            LoadStates();
            LoadLanguages();
            LoadComments();
            AddNewTour = new MyICommand(OnAddNewTour);
            AddRequest = new MyICommand(OnAddRequest);
            EndTourEnter = new MyICommand(OnEndTourEnter);
            tourRequests = new List<TourRequest>();
        }

        #region Properties
        public Visibility LabelVisibility
        {
            get { return _labelVisibility; }
            set
            {
                if (_labelVisibility != value)
                {
                    _labelVisibility = value;
                    OnPropertyChanged(nameof(LabelVisibility));
                }
            }
        }
        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public bool IsRequestEnabled
        {
            get { return _isRequestEnabled; }
            set
            {
                _isRequestEnabled = value;
                OnPropertyChanged(nameof(IsRequestEnabled));
            }
        }

        public List<string> CommentsComboBox
        {
            get { return _commentsComboBox; }
            set
            {
                _commentsComboBox = value;
                OnPropertyChanged(nameof(CommentsComboBox));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
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
               // MessageBox.Show(value.ToString("dd-MM-yyyy"));
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
                ChechValidation();
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

        #endregion

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
            foreach (Tour tour in tours)
            {
                languages.Add(tour.Language);
            }
            languages = new ObservableCollection<string>(languages.Distinct());
            LanguageComboBoxItems = languages;
        }

        public void ResetFields()
        {
            SelectedCity = "";
            SelectedLanguage = "";
            SelectedState = "";
            SliderValue = 0;
            SliderLabelText = "Broj gostiju: 0";
            Description = "";
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            SelectedItem = "";
        }

        public void OnAddNewTour()
        {
            TourRequest request = new TourRequest();
            Location reqLocation = locationControler.GetBy(SelectedState, SelectedCity);

            request.Location = reqLocation;
            request.LowerDateLimit = StartDate;
            request.UpperDateLimit = EndDate;
            request.Description = SelectedItem;
            request.GuestNumber = (int)SliderValue;
            request.Language = SelectedLanguage;
            request.Tourist = userController.GetBy(1);

            tourRequests.Add(request);

            ResetFields();
        }

        public void LoadComments()
        {
            CommentsComboBox = new List<string>();
            CommentsComboBox.Add("Najbolja tura");
            CommentsComboBox.Add("Najgora tura");
            CommentsComboBox.Add("Solidna tura");
            CommentsComboBox.Add("Poveljne cene");
            CommentsComboBox.Add("Skupa mnogo");
            CommentsComboBox.Add("Nije lose");
        }

        public void OnAddRequest()
        {
            ChechValidation();

            User user = userController.GetBy(1);

            ComplexTourRequest complexRequest = new ComplexTourRequest();
            complexRequest.Name = "ComplexTour";

            ComplexTourRequest complexTourRequest = complexTourRequestController.Save(complexRequest);


            foreach(TourRequest request in tourRequests)
            {
                tourRequestController.Save(request, user);
            }

            foreach (TourRequest request in tourRequests)
            {
                tourRequestController.UpdateComplexTourRequest(request, complexTourRequest);
            }



            MessageBox.Show("Uspesno sacuvan zahtev za complexTour");
        }

        public void OnEndTourEnter()
        {
            IsRequestEnabled = true;
            LabelVisibility = Visibility.Hidden;
        }

        public void ChechValidation()
        {
            if (SelectedItem.Equals("") ||
                SelectedLanguage.Equals("") ||
                SelectedState.Equals("") ||
                SelectedCity.Equals("") ||
                SliderValue == 0
               )
            {
                IsRequestEnabled = false;
                LabelVisibility = Visibility.Visible;
            }
            else
            {
                IsRequestEnabled = true;
                LabelVisibility = Visibility.Hidden;
            }
        }
    }
}
