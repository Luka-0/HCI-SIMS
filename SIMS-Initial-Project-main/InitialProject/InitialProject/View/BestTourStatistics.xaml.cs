using System;
using System.Collections.Generic;
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
using InitialProject.Dto;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for BestTourStatistics.xaml
    /// </summary>
    public partial class BestTourStatistics : Window
    {
        public TourStatisticsDto BestTour { get; set; } = new TourStatisticsDto();
        public BestTourStatistics(TourGuestsDto bestTour)
        {
            BestTour = new TourStatisticsDto(bestTour);
            
            this.DataContext = this;
            InitializeComponent();
        }

        
    }
}
