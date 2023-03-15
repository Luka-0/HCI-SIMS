using System;
using InitialProject.Forms;
using InitialProject.Model;
using InitialProject.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using InitialProject.Dto;
using InitialProject.Enumeration;


namespace InitialProject
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class CreateTourForm : Window
    {



        public event PropertyChangedEventHandler PropertyChanged;
        public NewTourDto NewTourDto; 

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreateTourForm()
        {
            InitializeComponent();
            DataContext = this;



        }


        private void Create(object sender, RoutedEventArgs e)
        {
            NewTourDto tourDto = new NewTourDto(NewTourDto);
            //fja ka servisu de je logika
        }
    }
}