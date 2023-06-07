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

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for Forums.xaml
    /// </summary>
    public partial class Forums : Page
    {
        CommentOwnerRepository CommentOwnerRepository = new CommentOwnerRepository();
        public Forums()
        {
            InitializeComponent();
            forums.ItemsSource = CommentOwnerRepository.GetOpenForums();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ForumOwnerVs forum = new ForumOwnerVs();
            forum = forums.SelectedItem as ForumOwnerVs;

            CommentOwnerVs comment = new CommentOwnerVs(forum, commentt.Text);

            this.CommentOwnerRepository.Save(comment);

            comments.ItemsSource = CommentOwnerRepository.GetAll();
        }
    }
}
