using InitialProject.Interface;
using InitialProject.Service;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;

namespace InitialProject.Controller
{
    public class ForumCommentController
    {
        private readonly ForumCommentService ForumCommentService = new(new ForumCommentRepository());

        public void Save(ForumComment forumComment)
        {
            ForumCommentService.Save(forumComment);
        }

        public List<ForumComment> GetByForum(int id)
        {
            return ForumCommentService.GetByForum(id);
        }

    }
}
