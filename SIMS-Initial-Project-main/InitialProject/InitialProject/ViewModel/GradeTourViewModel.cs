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
    public class GradeTourViewModel:BindableBase
    {
        LocationController locationController = new LocationController();
        private ObservableCollection<Tour> tours;

        private List<int> _comboBoxZnanje;
        private List<int> _comboBoxJezik;
        private List<int> _comboBoxZanimljivosti;

        private Tour _selectedRowData;

        public GradeTourViewModel() 
        {
            UpdateFooterParametar("home");
            UpdateHeaderTitle("Oceni turu");

            LoadTours();
            LoadComboBoxValues();
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

        public Tour SelectedRowData
        {
            get { return _selectedRowData; }
            set
            {
                if (_selectedRowData != value)
                {
                    _selectedRowData = value;
                    OnPropertyChanged(nameof(SelectedRowData));
                }
            }
        }

        public List<int> ComboBoxZnanje
        {
            get { return _comboBoxZnanje; }
            set
            {
                _comboBoxZnanje = value;
                OnPropertyChanged(nameof(ComboBoxZnanje));
            }
        }

        public List<int> ComboBoxJezik
        {
            get { return _comboBoxJezik; }
            set
            {
                _comboBoxJezik = value;
                OnPropertyChanged(nameof(ComboBoxJezik));
            }
        }
        public List<int> ComboBoxZanimljivosti
        {
            get { return _comboBoxZanimljivosti; }
            set
            {
                _comboBoxZanimljivosti = value;
                OnPropertyChanged(nameof(ComboBoxZanimljivosti));
            }
        }

        public void LoadComboBoxValues()
        {
            ComboBoxZanimljivosti = new List<int> { 1, 2, 3, 4, 5 };
            ComboBoxZnanje = new List<int> { 1, 2, 3, 4, 5 };
            ComboBoxJezik = new List<int> { 1, 2, 3, 4, 5 };
        }

        public void LoadTours()
        {
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

    }
}
