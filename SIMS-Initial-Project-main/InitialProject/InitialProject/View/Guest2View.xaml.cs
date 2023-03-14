using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for Guest2View.xaml
    /// </summary>
    public partial class Guest2View : Window
    {
        TourService TourService = new TourService();
        public Guest2View()
        {
            InitializeComponent();
        }

        private void mojButton_Click(object sender, RoutedEventArgs e)
        {
            List<Tour> Tours = TourService.GetAll();
            if(Tours.Count > 0)
            {
                MessageBox.Show("Nisam prazna");
            }
            else
            {
                MessageBox.Show("Prazna sam");
            }
            String s = "";

            DataTable dt = new DataTable();

            dt.Columns.Add("Column1", typeof(string));
            dt.Columns.Add("Column2", typeof(int));
            dt.Columns.Add("Column3", typeof(DateTime));

            dt.Rows.Add("Row1", 1, DateTime.Now);
            dt.Rows.Add("Row2", 2, DateTime.Now.AddDays(1));
            dt.Rows.Add("Row3", 3, DateTime.Now.AddDays(2));

            


            foreach (Tour t in Tours)
            {
                s += t.ToString();
                
            }
            MessageBox.Show(s);



        }
    }
}
