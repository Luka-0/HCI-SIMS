using InitialProject.Commands;
using InitialProject.Controller;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace InitialProject.ViewModel
{
    public class ShowSingleTourRequestViewModel:BindableBase
    {
        TourRequestController tourRequestController = new TourRequestController();

        private string description;
        private string location;
        private string language;
        private string guestNumber;
        private DateTime startDateAndTime;
        private DateTime endtDateAndTime;
        private string state;
       
        private string _selectedRequestId = "1";

       

        public ShowSingleTourRequestViewModel() 
        {
            Mediator.Instance.Subscribe("TourRequestIndexUpdated", OnTourRequestIndexUpdated);
            LoadData();
        }

        #region Properties
        public string SelectedRequestId
        {
            get { return _selectedRequestId; }
            set
            {
                _selectedRequestId = value;
                OnPropertyChanged(nameof(SelectedRequestId));
                LoadData();
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public string Language
        {
            get { return language; }
            set
            {
                language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        public string GuestNumber
        {
            get { return guestNumber; }
            set
            {
                guestNumber = value;
                OnPropertyChanged(nameof(GuestNumber));
            }
        }

        public DateTime LowerDateLimit
        {
            get { return startDateAndTime; }
            set
            {
                startDateAndTime = value;
                OnPropertyChanged(nameof(LowerDateLimit));
            }
        }

        public DateTime UpperDateLimit
        {
            get { return endtDateAndTime; }
            set
            {
                endtDateAndTime = value;
                OnPropertyChanged(nameof(UpperDateLimit));
            }
        }

        public string State
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged(nameof(State));
            }
        }

        #endregion


        private void OnTourRequestIndexUpdated(object newId)
        {
            SelectedRequestId = newId as string;
        }

        private void LoadData()
        {
            TourRequest tourRequest = tourRequestController.GetById(Int32.Parse(SelectedRequestId));    

            Description = tourRequest.Description;
            Location = tourRequest.Location.ToString();
            Language = tourRequest.Language;
            GuestNumber = tourRequest.GuestNumber.ToString();
            LowerDateLimit = tourRequest.LowerDateLimit;
            UpperDateLimit = tourRequest.UpperDateLimit;
            State = tourRequest.State.ToString();
        }

    }
}
