using InitialProject.Model;
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

namespace InitialProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for ViewRatings.xaml
    /// </summary>
    public partial class ViewRatings : Window
    {
        private readonly User User;

        public ViewRatings(User user)
        {
            InitializeComponent();

            User = user;
        }
    }
}
