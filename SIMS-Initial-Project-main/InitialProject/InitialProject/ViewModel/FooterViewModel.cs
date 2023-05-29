using InitialProject.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.ViewModel
{
    public class FooterViewModel:BindableBase
    {
        private string _parametar = "home";

        public FooterViewModel()
        {
            Mediator.Instance.Subscribe("ParametarUpdated", OnParametarUpdated);
        }

        public string Parametar
        {
            get { return _parametar; }
            set
            {
                _parametar = value;
                OnPropertyChanged(nameof(Parametar));
            }
        }

        private void OnParametarUpdated(object parametar)
        {
            Parametar = parametar as string;
        }
    }
}
