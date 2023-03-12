using System;
using InitialProject.Forms;
using InitialProject.Model;
using InitialProject.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using InitialProject.Data;

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
            User user = UserRepository.getUser(Username);

            if (user != null)
            {
                if(user.Password == txtPassword.Password)
                {
                    CommentsOverview commentsOverview = new CommentsOverview(user);
                    commentsOverview.Show();
                    Close();
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

            switch (user.Type)
            {
                case UserType.Korisnik:MessageBox.Show("Ulogovan korisnik!");
                    break;
                case UserType.Vodic: MessageBox.Show("Ulogovan vodic!");
                    break;
                case UserType.Admin: MessageBox.Show("Ulogovan admin!");
                    break;
                default: MessageBox.Show("Kuc kuc ko je!");
                    break;
            }
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Password = txtPassword.Password;
            user.Username = Username;
        

            UserRepository.AddUserToDbl(user);
        }
    }
}
