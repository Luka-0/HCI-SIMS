using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModel
{
    public class GraphLanguageViewModel:BindableBase
    {
        public SeriesCollection DataSeries { get; set; }
        public List<string> Languages { get; set; }

        public GraphLanguageViewModel()
        {
            // Generate sample data
            var languageData = new Dictionary<string, int>
        {
            { "English", 25 },
            { "Spanish", 10 },
            { "French", 15 },
            { "German", 20 }
        };

            Languages = new List<string>(languageData.Keys);

            // Create a ColumnSeries and add the data points
            ColumnSeries columnSeries = new ColumnSeries
            {
                Title = "Tour Requests",
                Values = new ChartValues<int>(languageData.Values)
            };

            // Create a SeriesCollection and add the ColumnSeries
            DataSeries = new SeriesCollection
        {
            columnSeries
        };
        }
    }
}
