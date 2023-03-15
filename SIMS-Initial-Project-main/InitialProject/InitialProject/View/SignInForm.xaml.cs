using System;
using InitialProject.Forms;
using InitialProject.Model;
using InitialProject.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using InitialProject.Enumeration;
using InitialProject.View;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;

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
            _repository = new UserRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
           // User user = _repository.GetByUsername(Username);
            User user = UserRepository.GetUser(Username);

            if (user != null)
            {
                if(user.Password == txtPassword.Password)
                {
                    //CommentsOverview commentsOverview = new CommentsOverview(user);
                    //commentsOverview.Show();
                    //Close(); //this.close();

                    switch (user.Type)
                    {
                        case UserType.Guest1:
                            {
                                MessageBox.Show("Guest1: " + user.Username + " is  logged in.");
                                Close();

                                Guest1View guest1View = new ();
                                guest1View.Show();

                                break;
                            }
                        case UserType.Guest2:
                            {
                                MessageBox.Show("Guest2: " + user.Username + " is  logged in.");

                                Close();
                                break;
                            }
                        case UserType.Guide:
                            {
                                MessageBox.Show("Guide: " + user.Username + " is  logged in.");

                                Close();
                                break;
                            }
                        case UserType.Owner:
                            {
                                OwnerView ownerWindow = new OwnerView();

                                //this.Hide();

                                ownerWindow.Show();

                                MessageBox.Show("Owner: " + user.Username + " is  logged in.");

                                Close();
                                break;
                            }
                        default:
                            MessageBox.Show("Unexpected user type!");
                            break;
                    }

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
        

            //UserRepository.AddUserToDbl(user);
        }
    }
}
