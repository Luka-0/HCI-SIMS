using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for KeyPointView.xaml
    /// </summary>
    public partial class KeyPointView : Window
    {
        private TourKeyPointController tourKeyPointController = new TourKeyPointController();
        private TourKeyPointService tourKeyPointService = new TourKeyPointService(new TourKeyPointRepository());
        private UserController userController = new UserController();
        public ObservableCollection<TourKeyPoint> keyPointsToShow { get; set; }
        public KeyPointView()
        {
            InitializeComponent();
            ShowAllKeyPoints();
        }

        private void RefreshDataGrid(List<TourKeyPoint> keyPoints)
        {
            keyPointsToShow = new ObservableCollection<TourKeyPoint>();
            ShowKeyPoints.ItemsSource = keyPointsToShow;
            foreach (TourKeyPoint k in keyPoints)
            {
                keyPointsToShow.Add(k);
            }
        }

        public void ShowAllKeyPoints()
        {
            this.DataContext = this;
            User user = userController.GetBy("paja");
            List<TourAndKeyPointsDto> tourAndKeyPointsDtos = tourKeyPointService.GetByGuestAndActiveTour(user);
            List<TourKeyPoint> keyPoints = new List<TourKeyPoint>();
            
            foreach(TourAndKeyPointsDto t in tourAndKeyPointsDtos)
            {
                foreach (TourKeyPoint tk in t.keyPoints)
                {
                    keyPoints.Add(tk);
                }
            }
            if(keyPoints.Count > 0) { MessageBox.Show("IMA NESTO"); }
            RefreshDataGrid(keyPoints);
        }





    }


}
