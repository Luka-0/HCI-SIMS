using InitialProject.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModel
{
    public class NavigationViewModel:BindableBase
    {
        public NavigationViewModel() 
        {
            UpdateFooterParametar("home");
            UpdateHeaderTitle("Opcije");
        }

    }
}
