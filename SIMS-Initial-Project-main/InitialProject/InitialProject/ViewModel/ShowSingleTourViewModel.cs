using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace InitialProject.ViewModel
{
    public class ShowSingleTourViewModel:BindableBase
    {
        TourController tourController = new TourController();

        private string description;
        private string name;
        private string language;
        private DateTime startDateAndTime;
        private TimeSpan duration;
        private string location;
        private string imageSource = "D:\\rozga1.jpg";

        private string _selectedTourId = "5";

        public MyICommand PreviousImageCommand { get; set; }
        public MyICommand NextImageCommand { get; set; }

        public ShowSingleTourViewModel()
        {
            Mediator.Instance.Subscribe("TourIndexUpdated", OnTourIndexUpdated);
            PreviousImageCommand = new MyICommand(OnPreviousImage);
            NextImageCommand = new MyICommand(OnNextImage);

        }

        private void OnNextImage()
        {
            ImageSource = "D:\\rozga.jpg";
        }

        private void OnPreviousImage()
        {
            ImageSource = "D:\\rozga1.jpg";
        }


        #region Properties

        public string ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        public string SelectedTourId
        {
            get { return _selectedTourId; }
            set
            {
                _selectedTourId = value;
                OnPropertyChanged(nameof(SelectedTourId));
                LoadData();
            }
        }


        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Language
        {
            get { return language; }
            set
            {
                if (language != value)
                {
                    language = value;
                    OnPropertyChanged(nameof(Language));
                }
            }
        }

        public DateTime StartDateAndTime
        {
            get { return startDateAndTime; }
            set
            {
                if (startDateAndTime != value)
                {
                    startDateAndTime = value;
                    OnPropertyChanged(nameof(StartDateAndTime));
                }
            }
        }

        public TimeSpan Duration
        {
            get { return duration; }
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }

        public string Location
        {
            get { return location; }
            set
            {
                if (location != value)
                {
                    location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }

        #endregion

        private void OnTourIndexUpdated(object newId)
        {
            SelectedTourId = newId as string;
        }

        private void LoadData()
        {
            Tour tour = tourController.GetById(int.Parse(SelectedTourId));

            Name = tour.Name;
            Language = tour.Language;
            Description = tour.Description;
            Location = tour.Location.ToString();
            Duration = tour.Duration;
            StartDateAndTime = tour.StartDateAndTime;
        }



    }
}
