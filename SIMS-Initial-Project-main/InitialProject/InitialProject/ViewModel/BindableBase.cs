using InitialProject.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModel
{
    public class BindableBase : INotifyPropertyChanged
    {

        protected virtual void SetProperty<T>(ref T member, T val,
           [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void UpdateFooterParametar(string newParametar)
        {
            Mediator.Instance.Publish("ParametarUpdated", newParametar);
        }

        public void UpdateHeaderTitle(string newTitle)
        {
            Mediator.Instance.Publish("TitleUpdated", newTitle);
        }

        public void UpdateSelectedTourIndex(string newIndex)
        {
            Mediator.Instance.Publish("TourIndexUpdated", newIndex);
        }
    }
}
