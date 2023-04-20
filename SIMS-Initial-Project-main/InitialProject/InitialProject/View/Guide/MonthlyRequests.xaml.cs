using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InitialProject.Dto;
using InitialProject.Migrations;

namespace InitialProject.View.Guide
{
    /// <summary>
    /// Interaction logic for MonthlyRequests.xaml
    /// </summary>
    public partial class MonthlyRequests : Window
    {

        public ObservableCollection<MonthlyRequestsDto>  Requests { get; set; }
        public MonthlyRequests(TourRequestYearlyCountDto yearlyCount)
        {
            this.DataContext = this;

            Requests = GetMonthlyRequests(yearlyCount);
            InitializeComponent();
        }

        public ObservableCollection<MonthlyRequestsDto> GetMonthlyRequests(TourRequestYearlyCountDto yearlyCount)
        {
            List<MonthlyRequestsDto> initialList = InitializeMonths();

            yearlyCount.Dates.ForEach(date => initialList[date.Month - 1].Count++);

            return new ObservableCollection<MonthlyRequestsDto>(initialList);
        }

        private List<MonthlyRequestsDto> InitializeMonths()
        {
            List<MonthlyRequestsDto> requests = new List<MonthlyRequestsDto>();
            
            MonthlyRequestsDto january = new MonthlyRequestsDto("January");
            MonthlyRequestsDto february = new MonthlyRequestsDto("February");
            MonthlyRequestsDto march = new MonthlyRequestsDto("March");
            MonthlyRequestsDto april = new MonthlyRequestsDto("April");
            MonthlyRequestsDto may= new MonthlyRequestsDto("May");
            MonthlyRequestsDto june= new MonthlyRequestsDto("June");
            MonthlyRequestsDto july= new MonthlyRequestsDto("July");
            MonthlyRequestsDto august= new MonthlyRequestsDto("August");
            MonthlyRequestsDto september = new MonthlyRequestsDto("September");
            MonthlyRequestsDto october= new MonthlyRequestsDto("October");
            MonthlyRequestsDto november= new MonthlyRequestsDto("November");
            MonthlyRequestsDto december = new MonthlyRequestsDto("December");
            MonthlyRequestsDto[] months = {january, february, march, april, may, june, july, august, september, october, november, december };
            requests.AddRange(months);

            return requests;
        }
    }
}
