using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModel
{
    public class TourRequestViewModel : BindableBase
    {
        private string headerTitle = "Moj title jebote!";
        public string HeaderTitle
        {
            get { return headerTitle; }
        }
    }
}
