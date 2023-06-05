using InitialProject.Controller;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModel
{
    public class TourTrackingViewModel:BindableBase
    {
        private readonly TourController tourController = new TourController();
        private readonly TourKeyPointController tourKeyPointController = new TourKeyPointController();
        private readonly UserController userController = new UserController();
        private readonly LocationController locationController = new LocationController();

        private ObservableCollection<Tour> tours;

        private string _name = "";
        private string _reached = "";
        private string _type = "";

        private Tour _selectedRowData;

        public TourTrackingViewModel() 
        {
            UpdateFooterParametar("home");
            UpdateHeaderTitle("Pracenje ture");

            LoadTours();
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
                    LoadKeyPoints();
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Reached
        {
            get { return _reached; }
            set
            {
                if (_reached != value)
                {
                    _reached = value;
                    OnPropertyChanged(nameof(Reached));
                }
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }

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


        public void LoadTours()
        {
            /*
            ObservableCollection<Tour> _tours = new ObservableCollection<Tour>();
            List<Tour> tempTours = tourController.GetAll();

            foreach (Tour tour in tempTours)
            {
                _tours.Add(tour);
            }

            Tours = _tours;
            */

            ObservableCollection<Tour> _tours = new ObservableCollection<Tour>();
            TimeSpan ts = new TimeSpan(0, 0, 0);
            Location location1 = locationController.GetBy(4);
            _tours.Add(new Tour("Domaca tura", "Najjaca bre", "Srpski", 15, DateTime.Now, ts, location1));
            location1 = locationController.GetBy(5);
            _tours.Add(new Tour("Italian tour", "Najjaca bre", "Srpski", 15, DateTime.Now, ts, location1));
            location1 = locationController.GetBy(6);
            _tours.Add(new Tour("Safari", "Najjaca bre", "Srpski", 15, DateTime.Now, ts, location1));


            Tours = _tours;
        }

        public void LoadKeyPoints()
        {
            if(SelectedRowData.Name.Equals("Domaca tura"))
            {
                Name = "Domaca tura";
                Reached = "Jeste";
                Type = "Start";
            }

            if (SelectedRowData.Name.Equals("Italian tour"))
            {
                Name = "Italian tour";
                Reached = "Jeste";
                Type = "Mid";
            }

            if (SelectedRowData.Name.Equals("Safari"))
            {
                Name = "Safari";
                Reached = "Jeste";
                Type = "End";
            }
        }

    }
}
