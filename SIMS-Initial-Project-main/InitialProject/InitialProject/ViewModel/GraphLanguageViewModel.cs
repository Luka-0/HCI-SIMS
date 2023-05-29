﻿using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Controller;

namespace InitialProject.ViewModel
{
    public class GraphLanguageViewModel:BindableBase
    {
        TourRequestController _tourRequetController = new TourRequestController();

        public SeriesCollection DataSeries { get; set; }
        public List<string> Languages { get; set; }
        public List<string> XAxisLabels { get; set; }
        public List<int> YAxisLabels { get; set; }

        public GraphLanguageViewModel()
        {
            UpdateHeaderTitle("Broj tura po jeziku");
            UpdateFooterParametar("statistics");
            var languageData = _tourRequetController.GetRequestCountByLanguage();

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

            XAxisLabels = Languages.ToList();

        }



    }
}
