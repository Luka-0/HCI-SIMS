using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace InitialProject.ViewModels
{
    public class ShowAndSearchTourViewModel : ViewModelBase
    {
        private readonly TourService _tourService = new TourService(new TourRepository());
        private readonly ObservableCollection<TourViewModel> _tours;

        public IEnumerable<TourViewModel> Tours => _tours;

        public ICommand ShowAllToursCommand { get; }
        public ICommand NextView { get; }

        public ShowAndSearchTourViewModel() 
        { 
            _tours = new ObservableCollection<TourViewModel>();

             List<Tour> allTour = _tourService.GetAll();
              foreach(Tour t in allTour)
              {
                 _tours.Add(new TourViewModel(t));
              }

        }








    }
}
