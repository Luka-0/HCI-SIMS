using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModel
{
    public class AddPictureAndCommentViewModel:BindableBase
    {
        public AddPictureAndCommentViewModel()
        {
            UpdateFooterParametar("gradeTour");
            UpdateHeaderTitle("Dodaj sliku/komentar");
        }
    }
}
