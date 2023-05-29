﻿using InitialProject.Commands;
using InitialProject.ViewModel;
using InitialProject.Windows;
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

namespace InitialProject.View.Tourist
{
    /// <summary>
    /// Interaction logic for TouristHomeView.xaml
    /// </summary>
    public partial class TouristHomeView : UserControl
    {
        public TouristHomeView()
        {
            InitializeComponent();
            this.DataContext = new TouristHomeViewModel();
        }

        private void Footer_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
