using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModels
{
    public class TourViewModel : ViewModelBase
    {
        private readonly Tour _tour;

        public string Name => _tour.Name;
        public string Location => _tour.Location.ToString();
        public string Date => _tour.StartDateAndTime.Date.ToString("dd-mm-yyyy");


        public TourViewModel(Tour tour)
        {
            _tour = tour;
        }



    }
}
