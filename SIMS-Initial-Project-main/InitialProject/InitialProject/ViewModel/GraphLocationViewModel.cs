using InitialProject.Controller;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModel
{
    public class GraphLocationViewModel:BindableBase
    {
        TourRequestController _tourRequetController = new TourRequestController();

        public SeriesCollection DataSeries { get; set; }
        public List<string> Locations { get; set; }
        public List<string> XAxisLabels { get; set; }
        public List<int> YAxisLabels { get; set; }

        public GraphLocationViewModel()
        {
            UpdateHeaderTitle("Broj tura po lokaciji");
            UpdateFooterParametar("statistics");
            var locationData = _tourRequetController.GetRequestCountByLocation();

            Locations = new List<string>(locationData.Keys);

            // Create a ColumnSeries and add the data points
            ColumnSeries columnSeries = new ColumnSeries
            {
                Title = "Tour Requests",
                Values = new ChartValues<int>(locationData.Values)
            };

            // Create a SeriesCollection and add the ColumnSeries
            DataSeries = new SeriesCollection
            {
                columnSeries
            };

            XAxisLabels = Locations.ToList();

        }
    }
}
