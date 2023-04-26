﻿using InitialProject.Controller;
using InitialProject.Model;
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

namespace InitialProject.View.Guest1
{
    public partial class ViewRatings : Window
    {
        private readonly GuestReviewController GuestReviewController = new();
        private readonly AccommodationReviewController AccommodationReviewController = new();

        private readonly User User;

        public ObservableCollection<InitialProject.Model.GuestReview> ReviewsToShow { get; set; }

        public ViewRatings(User user)
        {
            InitializeComponent();
            InitializeReviewsDataGrid();

            User = user;

        }

        private void InitializeReviewsDataGrid()
        {
            List<InitialProject.Model.GuestReview> allGuestReviews = GuestReviewController.GetAll(User);
            List<AccommodationReview> allAccommodationReviews = AccommodationReviewController.GetBy(User);

            ReviewsToShow = new ObservableCollection<InitialProject.Model.GuestReview>();
            ReviewsGrid.ItemsSource = ReviewsToShow;

            MessageBox.Show(allGuestReviews.Count.ToString());

            foreach (InitialProject.Model.GuestReview g in allGuestReviews)
            {
                MessageBox.Show(g.Id.ToString());
                foreach (AccommodationReview a in allAccommodationReviews)
                {
                    MessageBox.Show(a.Id.ToString());
                    if (g.Reservation.Id == a.Reservation.Id)
                    {
                        ReviewsToShow.Add(g);
                        allAccommodationReviews.Remove(a);
                        break;
                    }
                }
            }
            //MessageBox.Show(ReviewsToShow.Count.ToString());
        }

    }
}
