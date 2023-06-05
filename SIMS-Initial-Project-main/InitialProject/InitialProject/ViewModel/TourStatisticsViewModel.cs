using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModel
{
    public class TourStatisticsViewModel : BindableBase
    {
        public TourStatisticsViewModel()
        {
            UpdateHeaderTitle("Statistika o turama");
            UpdateFooterParametar("home");
        }
    }
}
