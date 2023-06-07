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
    public class ComplexTourPreviewViewModel:BindableBase
    {
        ComplexTourRequestController complexTourRequestController = new ComplexTourRequestController();

        public ObservableCollection<ComplexTourRequest> _complexTourRequests { get; set; }
        public ComplexTourPreviewViewModel() 
        {
            LoadComplexRequests();
        }

        public ObservableCollection<ComplexTourRequest> ComplexTourRequests
        {
            get { return _complexTourRequests; }
            set
            {
                if (_complexTourRequests != value)
                {
                    _complexTourRequests = value;
                    OnPropertyChanged(nameof(ComplexTourRequests));
                }
            }
        }

        public void LoadComplexRequests()
        {
            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>();

            List<ComplexTourRequest> allRequests = complexTourRequestController.GetAll();
            ObservableCollection<ComplexTourRequest> tourRequests = new ObservableCollection<ComplexTourRequest>();
            foreach (ComplexTourRequest request in allRequests)
            {
                tourRequests.Add(request);
            }
            ComplexTourRequests = tourRequests;
        }

    }
}
