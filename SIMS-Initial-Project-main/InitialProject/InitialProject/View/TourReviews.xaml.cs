using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InitialProject.Model;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourReviews.xaml
    /// </summary>
    public partial class TourReviews : Window
    {
        public User LoggedInGuide { get; set; }
        private int tourId { get; set; }
        public TourReviews(User user, int tourId)
        {
            InitializeComponent();
            this.DataContext = this;
            this.tourId = tourId;
            this.LoggedInGuide = user;
        }
    }
}
