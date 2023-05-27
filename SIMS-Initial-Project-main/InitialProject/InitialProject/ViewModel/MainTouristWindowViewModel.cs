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
        public MyICommand<string> NavCommand { get; set; }
        public string HeaderTitle { get; set; }

        private TouristHomeViewModel homeViewModel = new TouristHomeViewModel(); 
        private ShowAndSearchTourViewModel showAndSearchTour = new ShowAndSearchTourViewModel();
        private TourRequestViewModel requestViewModel = new TourRequestViewModel();
        private NavigationViewModel navigationViewModel = new NavigationViewModel();
        private ShowTourRequestViewModel showTourRequestViewModel = new ShowTourRequestViewModel();
        private StatisticsViewModel statisticsViewModel = new StatisticsViewModel();
        private GraphLanguageViewModel graphLanguageViewModel = new GraphLanguageViewModel();   
        private HelpViewModel helpViewModel = new HelpViewModel();

        private BindableBase currentViewModel;

        public MainTouristWindowViewModel()
        {
            CurrentViewModel = homeViewModel;
            NavCommand = new MyICommand<string>(OnNav);
            HeaderTitle = "NASLOV";
        }

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        public void SetNewViewModel(BindableBase newViewModel)
        {
            CurrentViewModel = newViewModel;
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
                case "showTourRequest":
                    CurrentViewModel = showTourRequestViewModel;
                    break;
                case "statistics":
                    CurrentViewModel = statisticsViewModel;
                    break;
                case "graphLanguageRequest":
                    CurrentViewModel = graphLanguageViewModel;
                    break;
                case "helpViewModel":
                    CurrentViewModel = helpViewModel;
                    break;
            }
        }
    }
}
