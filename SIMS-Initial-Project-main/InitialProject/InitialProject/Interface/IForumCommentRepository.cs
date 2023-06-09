using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    interface IForumCommentRepository
    {
        public void Save(ForumComment forumComment);
        public List<ForumComment> GetByForum(int id);
        public List<ForumComment> GetAll();
        public void SaveComment(ForumComment comment);
    }
}
