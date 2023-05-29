using InitialProject.Commands;
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
    public class TouristHomeViewModel : BindableBase
    {
        private TourRequestController _tourRequestController = new TourRequestController();
        private ObservableCollection<string> _notificationListItems;
        private string _selectedNotification;

        public ObservableCollection<string> NotificationListItems
        {
            get { return _notificationListItems; }
            set
            {
                _notificationListItems = value;
                OnPropertyChanged(nameof(NotificationListItems));
            }
        }

        public string SelectedNotification
        {
            get { return _selectedNotification; }
            set
            {
                _selectedNotification = value;
                OnPropertyChanged(nameof(SelectedNotification));
            }
        }

        public TouristHomeViewModel()
        {
            LoadNewTourNotifications();
            UpdateFooterParametar("home");
        }

        

        /*
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
        }*/

        public void LoadNewTourNotifications()
        {
            List<TourRequest> requests = _tourRequestController.GetByState(Enumeration.TourRequestState.Accepted);
            ObservableCollection<string> newNotifications = new ObservableCollection<string>();

            foreach (TourRequest request in requests)
            {
                newNotifications.Add("Kreirana je nova tura: \n" + request.Location + " Od: " + request.LowerDateLimit.Date.ToString("dd-MM-yyyy") + ", do: " + request.UpperDateLimit.Date.ToString("dd-MM-yyyy"));
            }
            NotificationListItems = newNotifications;
            //foreach(String s in newNotifications)
            //{
             //   _notificationListItems.Add(s);
           // }

        }


    }
}
