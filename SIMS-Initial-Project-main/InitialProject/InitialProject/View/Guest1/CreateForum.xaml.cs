﻿using InitialProject.Controller;
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
    /// Interaction logic for CreateForum.xaml
    /// </summary>
    public partial class CreateForum : Window
    {
        private readonly ForumController ForumController = new();
        private readonly LocationController LocationController = new();

        private User User { get; set; }
        public CreateForum(User user)
        {
            DataContext = this;
            User = user;

            InitializeComponent();
            InitializeCountryCB();
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

        private void RemoveRedFromControls()
        {
            CommentTB.ClearValue(Border.BorderThicknessProperty);
            CommentTB.ClearValue(Border.BorderBrushProperty);

            CountryCB.ClearValue(Border.BorderThicknessProperty);
            CountryCB.ClearValue(Border.BorderBrushProperty);

            CityCB.ClearValue(Border.BorderThicknessProperty);
            CityCB.ClearValue(Border.BorderBrushProperty);
        }

        public void MakeControlRed<T>(T control) where T : Control
        {
            control.Focus();
            control.BorderBrush = new SolidColorBrush(Colors.Red);
            control.BorderThickness = new Thickness(2);
        }

        private void CreatePost_Click(object sender, RoutedEventArgs e)
        {
            if (IsViolatingAnyUIControl()) return;

            Forum forumToSave = new(LocationController.GetByCity(CityCB.SelectedItem.ToString()), CommentTB.Text, User);
            ForumController.Save(forumToSave);

            RemoveRedFromControls();
            MessageBox.Show("Successfull");
        }

        private bool IsViolatingAnyUIControl()
        {
            if(CountryCB.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a country");

                MakeControlRed(CountryCB);

                return true;
            }
            if(CityCB.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a city");

                MakeControlRed(CityCB);

                return true;
            }
            if (CommentTB.Text.Equals(""))
            {
                MessageBox.Show("Please write something in the comment section");

                MakeControlRed(CommentTB);

                return true;
            }

            RemoveRedFromControls();
            return false;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Guest1Menu guest1Menu = new(User);
            guest1Menu.Show();

            Close();
        }
    }
}
