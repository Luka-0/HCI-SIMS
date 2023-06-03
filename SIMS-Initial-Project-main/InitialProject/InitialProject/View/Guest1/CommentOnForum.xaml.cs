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
using InitialProject.Dto;
using InitialProject.Controller;
using System.Collections.ObjectModel;

namespace InitialProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for CommentOnForum.xaml
    /// </summary>
    public partial class CommentOnForum : Window
    {
        private readonly ForumController ForumController = new();
        private readonly ForumCommentController ForumCommentController = new();
        private readonly LocationController LocationController = new();

        public ObservableCollection<ForumCommentDto> CommentsToShow; 

        private User User { get; set; }


        public CommentOnForum(User user)
        {
            DataContext = this;
            User = user;

            InitializeComponent();
            InitializeCountryCB();

            GridL.IsEnabled = false;
        }

        private void InitializeCountryCB()
        {
            CountryCB.Items.Add("--Chose--");

            List<Location> countries = LocationController.GetAllDistinctByCountry();
            foreach (Location location in countries)
            {
                CountryCB.Items.Add(location.Country.ToString());
            }

            CountryCB.SelectedIndex = 0;
        }

        private void InitializeCityCB(object sender, MouseEventArgs e)
        {
            if (CountryCB.SelectedIndex == 0) return;

            CityCB.Items.Clear();

            List<Location> locations = LocationController.GetByCountry(CountryCB.SelectedItem.ToString());
            foreach (Location location in locations)
            {
                CityCB.Items.Add(location.City);
            }
        }

        private void RefreshDataGrid(List<ForumCommentDto> forumComments, Forum forum)
        {
            CommentsToShow = new ObservableCollection<ForumCommentDto>();
            ForumGrid.ItemsSource = CommentsToShow;

            ForumCommentDto newForum = new(forum.InitialComment, forum.Location, forum.User, forum);
            CommentsToShow.Add(newForum);

            foreach (ForumCommentDto a in forumComments)
            {
                CommentsToShow.Add(a);
            }

            GridL.IsEnabled = true;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Guest1Menu guest1Menu = new(User);
            guest1Menu.Show();

            Close();
        }

        private void AddComment_Click(object sender, RoutedEventArgs e)
        {
            ForumComment forumComment = new(CommentTB.Text, CommentsToShow.First().Forum, User);

            ForumCommentController.Save(forumComment);
            MessageBox.Show("Successfuly saved");

            ShowDG();
        }

        private void PreShowDG(object sender, MouseEventArgs e)
        {
            ShowDG();
        }

        private void ShowDG()
        {
            if (CityCB.SelectedIndex < 0) return;

            Forum forum = ForumController.GetByCity(CityCB.SelectedItem.ToString());
            if (forum == null) return;

            List<ForumComment> forumComments = ForumCommentController.GetByForum(forum.Id);

            List<ForumCommentDto> forumCommentDtos = new();
            foreach(ForumComment f in forumComments)
            {
                ForumCommentDto tmp = new(f.Text, null, f.User, f.Forum);
                forumCommentDtos.Add(tmp);
            }

            RefreshDataGrid(forumCommentDtos, forum);
        }


    }
}
