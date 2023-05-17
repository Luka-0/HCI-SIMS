using InitialProject.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModel
{
    public class StatisticsViewModel:BindableBase
    {
        private TourRequestController tourRequestController = new TourRequestController();

        private string acceptedPercentage = "Prihvaceni zahtevi: ";
        private string unacceptedPercentage = "Odbijeni zahtevi: ";
        private string acceptedGuestNumber = "Br. ljudi u prihvacenim zahtevima:";

        public string AcceptedPercentage
        {
            get { return acceptedPercentage; }
            set
            {
                if (acceptedPercentage != value)
                {
                    acceptedPercentage = value;
                    OnPropertyChanged(nameof(AcceptedPercentage));
                }
            }
        }

        public string AcceptedGuestNumber
        {
            get { return acceptedGuestNumber; }
            set
            {
                if (acceptedGuestNumber != value)
                {
                    acceptedGuestNumber = value;
                    OnPropertyChanged(nameof(AcceptedGuestNumber));
                }
            }
        }

        public StatisticsViewModel()
        {
            LoadStatistics();
        }

        public string UnacceptedPercentage
        {
            get { return unacceptedPercentage; }
            set
            {
                if (unacceptedPercentage != value)
                {
                    unacceptedPercentage = value;
                    OnPropertyChanged(nameof(UnacceptedPercentage));
                }
            }
        }

        public void LoadStatistics()
        {
            string[] statistics = tourRequestController.GetStatistics();
            acceptedPercentage = "Prihvaceni zahtevi:        " + statistics[0] + "%";
            unacceptedPercentage = "Odbijeni zahtevi:        " + statistics[1] + "%";
            acceptedGuestNumber = "Br. ljudi u prihvacenim zahtevima:       " + statistics[2];
        }
    }
}
