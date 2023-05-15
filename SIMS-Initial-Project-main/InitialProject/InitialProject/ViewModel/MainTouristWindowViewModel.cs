using InitialProject.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.ViewModel;

namespace InitialProject.ViewModel
{
    public class MainTouristWindowViewModel : BindableBase
    {
        public MyICommand<string> NavCommand { get; private set; }

        private TouristHomeViewModel homeViewModel = new TouristHomeViewModel(); 
        private ShowAndSearchTourViewModel showAndSearchTour = new ShowAndSearchTourViewModel();
        private TourRequestViewModel requestViewModel = new TourRequestViewModel();
        private NavigationViewModel navigationViewModel = new NavigationViewModel();
        

        private BindableBase currentViewModel;

        public MainTouristWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = homeViewModel;
        }

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "home":
                    CurrentViewModel = homeViewModel;
                    break;
                case "tourRequest":
                    CurrentViewModel = requestViewModel;
                    break;
                case "showAndSearchTour":
                    CurrentViewModel = showAndSearchTour;
                    break;
                case "navigation":
                    CurrentViewModel = navigationViewModel;
                    break;
            }
        }
    }
}
