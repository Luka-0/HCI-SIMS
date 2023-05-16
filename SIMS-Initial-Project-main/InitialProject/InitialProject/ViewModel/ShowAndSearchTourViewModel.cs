using InitialProject.Controller;
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

namespace InitialProject.ViewModel
{
    public class ShowAndSearchTourViewModel : BindableBase
    {
        private readonly TourController tourController = new TourController();
        public  ObservableCollection<Tour> Tours {  get; set; }     //for Binding

       // public IEnumerable<Tour> Tours => _tours;

        public ICommand ShowAllToursCommand { get; }
        public ICommand NextView { get; }

        public ShowAndSearchTourViewModel()
        {
            LoadTours();
        }

        public void LoadTours()
        {
            ObservableCollection<Tour> _tours = new ObservableCollection<Tour>();
            List<Tour> tempTours = tourController.GetAll();

            foreach (Tour tour in tempTours)
            {
                _tours.Add(tour);
            }

            Tours = _tours;
        }







    }
}
