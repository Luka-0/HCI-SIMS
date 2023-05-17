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
    class ShowTourRequestViewModel:BindableBase
    {
        private ObservableCollection<TourRequest> _tourRequests;
        private TourRequestController tourRequestController = new TourRequestController();

        public ObservableCollection<TourRequest> TourRequests
        {
            get { return _tourRequests; }
            set { SetProperty(ref _tourRequests, value); }
        }

        public ShowTourRequestViewModel()
        {
            LoadRequests();
        }

        public void LoadRequests()
        {
            TourRequests = new ObservableCollection<TourRequest>();

            List<TourRequest> allRequests = tourRequestController.GetAll();
            ObservableCollection<TourRequest> tourRequests = new ObservableCollection<TourRequest>();
            foreach(TourRequest request in allRequests)
            {
                tourRequests.Add(request);
            }
            TourRequests = tourRequests;
        }
    }
}
