using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using InitialProject.Controller;

namespace InitialProject.View.Guest1
{
    public partial class DeactiveForum : Window
    {
        private readonly ForumController ForumController = new();

        private User User { get; set; }
        private ObservableCollection<Forum> ForumsToShow { get; set; } = new();

        public DeactiveForum(User user)
        {
            User = user;
            DataContext = this;

            InitializeComponent();

            ShowDataGrig();

        }

        private void ShowDataGrig()
        {
            List<Forum> forums = ForumController.GetByUser(User.Id);
            ForumsGrid.ItemsSource = ForumsToShow;

            foreach(Forum f in forums)
            {
                ForumsToShow.Add(f);
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Guest1Menu guest1Menu = new(User);
            guest1Menu.Show();

            Close();
        }

        private void Deactivate_Click(object sender, RoutedEventArgs e)
        {
            Forum forumToDeactivate = (Forum)ForumsGrid.SelectedItem;
            if(forumToDeactivate == null)
            {
                MessageBox.Show("Please select a forum do deactivate");

                ForumsGrid.Focus();
                ForumsGrid.BorderBrush = new SolidColorBrush(Colors.Red);
                ForumsGrid.BorderThickness = new Thickness(2);

                return;
            }
            if(forumToDeactivate.IsClosed == true)
            {
                MessageBox.Show("That forum is already closed");
                return;
            }

            ForumsGrid.ClearValue(Border.BorderThicknessProperty);
            ForumsGrid.ClearValue(Border.BorderBrushProperty);

            ForumController.UpdateActivity(forumToDeactivate);
        }
    }
}
