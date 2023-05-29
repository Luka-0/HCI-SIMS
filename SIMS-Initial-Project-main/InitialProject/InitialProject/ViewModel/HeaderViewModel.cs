using InitialProject.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModel
{
    public class HeaderViewModel:BindableBase
    {
        private string _title = "Opcije";

        public HeaderViewModel()
        {
            Mediator.Instance.Subscribe("TitleUpdated", OnTitleUpdated);
        }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));

            }
        }

        private void OnTitleUpdated(object title)
        {
            Title = title as string;
        }


    }
}
