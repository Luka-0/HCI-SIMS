using InitialProject.Controller;
using InitialProject.Model;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.ViewModels
{
    public class ShowTourViewModel : ViewModelBase
    {
        private readonly TourController _tourController = new TourController();
        private readonly ObservableCollection<TourViewModel> _tours;

        public IEnumerable<TourViewModel> Tours => _tours;
        public ICommand ShowTour { get; }
        public ShowTourViewModel() 
        {
            _tours = new ObservableCollection<TourViewModel>();

            Tour tour = _tourController.GetById(1);
            _tours.Add(new TourViewModel(tour));
        }
    }
}
