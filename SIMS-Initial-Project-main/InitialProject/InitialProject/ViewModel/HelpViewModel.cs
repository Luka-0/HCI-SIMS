using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModel
{
    public class HelpViewModel:BindableBase
    {
        public HelpViewModel() 
        {
            UpdateHeaderTitle("HELP");
            UpdateFooterParametar("home");
        }

        private string _navCommandParamReserve = "statistics";
        public string NavCommandParamReserve
        {
            get { return _navCommandParamReserve; }
            set
            {
                if (_navCommandParamReserve != value)
                {
                    _navCommandParamReserve = value;
                    OnPropertyChanged(nameof(NavCommandParamReserve));
                }
            }
        }
    }
}
