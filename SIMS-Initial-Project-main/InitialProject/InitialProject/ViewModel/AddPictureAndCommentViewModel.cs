using InitialProject.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModel
{
    public class AddPictureAndCommentViewModel:BindableBase
    {
        public List<string> _commentsComboBox;
        private string _selectedItem;
        private string _message = "";

        public MyICommand AddPicture { get; set; }
        public AddPictureAndCommentViewModel()
        {
            UpdateFooterParametar("gradeTour");
            UpdateHeaderTitle("Dodaj sliku/komentar");

            AddPicture = new MyICommand(OnAddPicture);

            LoadComments();

        }

        public List<string> CommentsComboBox
        {
            get { return _commentsComboBox; }
            set
            {
                _commentsComboBox = value;
                OnPropertyChanged(nameof(CommentsComboBox));
            }
        }

        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public void LoadComments()
        {
            CommentsComboBox = new List<string>();
            CommentsComboBox.Add("Najbolja tura");
            CommentsComboBox.Add("Najgora tura");
            CommentsComboBox.Add("Solidna tura");
            CommentsComboBox.Add("Poveljne cene");
            CommentsComboBox.Add("Skupa mnogo");
            CommentsComboBox.Add("Nije lose");
        }

        public void OnAddPicture()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
            if(openFileDialog.ShowDialog() == true )
            {
                string selectedImagePath = openFileDialog.FileName;

                string destinationFolderPath = @"E:\\JobGitRepos\\HCI-SIMS\\SIMS-Initial-Project-main\\InitialProject\\InitialProject\\Resources\\Images";
                string destinationFilePath = System.IO.Path.Combine(destinationFolderPath, System.IO.Path.GetFileName(selectedImagePath));

                File.Copy(selectedImagePath, destinationFilePath, true);
                Message = "Slika uspesno dodata!";
            }
        }
    }
}
