using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;

namespace InitialProject.Dto
{
    public class ForumCommentDto
    {
        public string Text { get; set; }
        public Location? Location { get; set; }
        public User? User { get; set; }
        public Forum? Forum { get; set; }

        public char Special { get; set; }

        public ForumCommentDto() { }

        public ForumCommentDto(string text, Location? location, User? user, Forum? forum, char special)
        {
            Text = text;
            Location = location;
            User = user;
            Forum = forum;
            Special = special;
        }
    }
}
