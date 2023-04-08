using System;
using InitialProject.Forms;
using InitialProject.Model;
using InitialProject.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using InitialProject.Enumeration;
using InitialProject.View;
using InitialProject.Contexts;
using InitialProject.Controller;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserController UserController;
        private readonly AccommodationReviewController AccommodationReviewController;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            UserController = new UserController();
            AccommodationReviewController = new AccommodationReviewController();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = UserController.GetBy(Username);

            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    // CommentsOverview commentsOverview = new CommentsOverview(user);
                    // commentsOverview.Show();
                    // Close(); //this.close();

                    switch (user.Type)
                    {
                        case UserType.Guest1:
                            {
                                MessageBox.Show("Guest1: " + user.Username + " is  logged in.");

                                Guest1 guest1 = new(user);
                                guest1.Show();

                                this.Close();
                                break;
                            }
                        case UserType.Guest2:
                            {
                                MessageBox.Show("Guest2: " + user.Username + " is  logged in.");

                                Guest2View view = new Guest2View(); 
                                view.Show();
                                this.Close();
                                break;
                            }
                        case UserType.Guide:
                            {
                                CreateTourForm createTourForm = new CreateTourForm(user);
                                createTourForm.Show();
                                Close();
                                
                                break;
                            }
                        case UserType.Owner:
                            { 
                                Owner owner = new Owner(user.Username);
                               //this.Hide();
                                owner.Show();
                                MessageBox.Show("Owner: " + user.Username + " is  logged in.");
                                this.Close();
                                
                                break;
                            }
                        default:
                            MessageBox.Show("Unexpected user type!");
                            break;
                    }

                    //bez obzira na tip ulogovanog korisnika
                    //na osnovu poslednjih podataka iz tabele svih recenzija smestaja i vlasnika
                    //se azuriraju klase smestaja za svakog vlasnika, kao i njegova titula
                    //kako bi gosti koji pregledaju smestaje imali ispravne podatke o  smestajima
                    //tj da te informacije ne zavise od toga da li je vlasnik prethodno bio ulogovan
                    AccommodationReviewController.DeclareOwners();


                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Password = txtPassword.Password;
            user.Username = Username;

            UserController.Add(user);

            MessageBox.Show("Successfully added user");
            this.Close();
        }
    }
}
