using InitialProject.Contexts;
using InitialProject.Model;
using InitialProject.Repository;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for CreateAccommodationView.xaml
    /// </summary>
    public partial class CreateAccommodationView : Page
    {
        public CreateAccommodationView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Accommodation acc = new Accommodation();

            AccommodationRepository.Save(acc);

            var db = new UserContext();
            var newAcc = db.accommodation.Find(acc.Id);
            newAcc.Location = AccommodationRepository.getBy("Beograd");
            db.SaveChanges();

        }
    }
}
